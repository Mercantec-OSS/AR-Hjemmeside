using Models;
using Microsoft.EntityFrameworkCore;
using DatabaseLayer;
using Repository.IRepository;

namespace Repository.Repository
{
    public class UserRepo : IUserRepo
    {
        private readonly DBcontext _dbContext;

        public UserRepo(DBcontext context) 
        {
            _dbContext = context;
        }
        public async Task<string> UserLoginAsync(UsersDTO DTOuser)
        {
            var hashedPass = "";

            try
            {
                var userEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.UserName == DTOuser.UserName);

                if (userEntity == null)
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

            if (PasswordHash.ValidatePassword(DTOuser.Password, hashedPass) == true)
            {
                return "";
            }
            else
            {
                return null;
            }
        }
    }
}
