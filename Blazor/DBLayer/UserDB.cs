using Models;
using Models.DTOModels;
using NuGet.Protocol;

namespace Blazor.DBLayer
{
    public class UserDB
    {
        private readonly string _apiRoot;

        private readonly HttpClient _httpClient;

        public UserDB()
        {
            _httpClient = new HttpClient();
            _apiRoot = "https://localhost:7013/api";
        }

        //Login
        public async Task<string> UserLogin(UsersDTO userDTO)
        {
            var uri = $"{_apiRoot}/user/login";

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(uri, userDTO);

                if (response.IsSuccessStatusCode)
                {
                    string token = await response.Content.ReadAsStringAsync();
                    if (token != null)
                    {
                        return token.ToString();
                    }

                    return null;
                }
            }
            catch
            {
                return null;
            }

            return "";
        }

        public async Task<bool> CreateUser(Users user)
        {
            var uri = $"{_apiRoot}/user/create";

            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync(uri, user);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                return false;
            }
            catch 
            {
                return false;
            }
        }


    }
}
