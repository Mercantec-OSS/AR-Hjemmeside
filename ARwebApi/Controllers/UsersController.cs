        using Microsoft.AspNetCore.Mvc;
using Models;
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

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> UserLoginAsync(UsersDTO DTOuser)
        {
            if (DTOuser.UserName == null || DTOuser.Password == null)
            {
                return StatusCode(400);
            }

            if (_userRepo.UserLoginAsync(DTOuser) != null)
            {
                var eow = await _userRepo.UserLoginAsync(DTOuser);
                return eow;
            }

            return StatusCode(400);
        }
    }
}
