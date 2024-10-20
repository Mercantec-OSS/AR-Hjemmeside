using Blazor.Repository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Blazor.Components.Pages
{
    public partial class CreateUser
    {
        private Users newUser = new();
        
        [Inject]
        private UserRepo _userRepo { get; set; }
        [Inject]
        NavigationManager navManager { get; set; }

        private async Task CreateNewUser()
        {
            var result = await _userRepo.CreateUser(newUser);

            if (result == true)
            {
                navManager.NavigateTo("/");
            }
        }
    }

}

