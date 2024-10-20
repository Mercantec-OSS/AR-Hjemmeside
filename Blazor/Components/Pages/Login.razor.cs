using Blazor.Repository;
using Microsoft.AspNetCore.Components;
using Models.DTOModels;
using Newtonsoft.Json;
using System.Text;


namespace Blazor.Components.Pages
{
    public partial class Login
    {
        public UsersDTO userDTO = new();

        [Inject]
        private UserRepo _userRepo { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }

        public async Task UserLogin()
        {
            var success = await _userRepo.UserLogin(userDTO);

            if (success == true)
            {
                navManager.NavigateTo("/");
            }

        }
    }
}
