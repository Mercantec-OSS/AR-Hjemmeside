using Models;
using Microsoft.EntityFrameworkCore;
using DatabaseLayer;
using Repository.IRepository;
using Models.DTOModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Repository.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly IConfiguration _configuration;
        private readonly DBcontext _dbContext;

        public UserRepo(DBcontext context, IConfiguration configuration)
        {
            _configuration = configuration;
            _dbContext = context;
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            if (_dbContext.Users == null)
            {
                return null;
            }

            return await _dbContext.Users.ToListAsync();
        }

        public async Task<string> UserLoginAsync(UsersDTO userDTO)
        {
            var hashedPass = "";

            try
            {
                 var userEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == userDTO.UserName);

                if (userEntity == null)
                {
                    return null;
                }

                userDTO.Role = userEntity.Role;
                userDTO.UID = userEntity.Id.ToString();

                if (userDTO.Role == null || userDTO.UID == null)
                {
                    return null;
                }

                var validPass = userEntity.Password;

                if (validPass == null)
                {
                    return null;
                }

                hashedPass = validPass;
            }
            catch
            {
                return null;
            }

            if (PasswordHash.ValidatePassword(userDTO.Password, hashedPass) == true)
            {
                return GenerateToken(userDTO);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> CreateUserAsync(Users user)
        {
            if (_dbContext.Users.Any(x => x.UserName == user.UserName) || _dbContext.Users.Any(x => x.Email == user.Email))
            {
                return false;
            }

            try
            {
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserAsync(DeleteUserDTO DTODUser)
        {
            if (_dbContext == null)
            {
                return false;
            }

            var userEntity = await _dbContext.Users.FindAsync(DTODUser.UserName);

            if (userEntity == null)
            {
                return false;
            }

            try
            {
                _dbContext.Users.Remove(userEntity);
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                return false;
            }

            return true;
        }

        private string GenerateToken(UsersDTO userDTO)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userDTO.UserName),
                new Claim(ClaimTypes.NameIdentifier, userDTO.UID),
                new Claim(ClaimTypes.Role, userDTO.Role)
            };

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
