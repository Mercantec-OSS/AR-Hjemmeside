using Blazor.Repository;
using Microsoft.AspNetCore.Components;
using Models.DTOModels;


namespace Blazor.Components.Pages
{
    public partial class Login
    {
        public UsersDTO userDTO = new();

        [Inject]
        private UserRepo _userRepo { get; set; }


        public async Task<bool> UserLogin()
        {
            return await _userRepo.UserLogin(userDTO);

            
        }
    }
}
