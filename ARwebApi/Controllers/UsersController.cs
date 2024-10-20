using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTOModels;
using Repository.IRepository;

namespace ARwebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepo _userRepo;
        public UsersController(IUserRepo userRepo) 
        { 
            _userRepo = userRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> GetAllUsersAsync()
        {
            var userList = await _userRepo.GetAllUsersAsync();

            if (userList != null)
            {
                return userList;
            }

            return NotFound("DB is empty");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> UserLoginAsync(UsersDTO DTOuser)
        {
            var result = await _userRepo.UserLoginAsync(DTOuser);

            if (DTOuser.UserName == null || DTOuser.Password == null)
            {
                return StatusCode(400);
            }

            if (result != null)
            {
                return Ok(result);
            }
            
            return StatusCode(400);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<bool>> CreateUserAsync(Users user)
        {
            if (user.UserName == null  || user.Email == null || user.FirstName == null || user.LastName == null || user.Password == null)
            {
                return StatusCode(400, "Missing Username, Password, Email, Firstname or Lastname");
            }

            if (await _userRepo.CreateUserAsync(user) == true)
            {
                return StatusCode(200, "User was created");
            }

            return StatusCode(400, "Username or Email allready exists");
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteUserAsync(DeleteUserDTO DUserDTO)
        {
            var result = await _userRepo.DeleteUserAsync(DUserDTO);

            if (result == false)
            {
                return BadRequest("User wasn't deleted");
            }

            return Ok("User deleted");
        }
    }
}
