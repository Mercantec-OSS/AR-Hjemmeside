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


        public async Task<bool> UserLogin()
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(userDTO), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7013/api/user/login", content);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                    return false; 
            }

        }
    }
}
