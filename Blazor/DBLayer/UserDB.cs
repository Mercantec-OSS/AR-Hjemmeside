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
                string token = await response.Content.ReadAsStringAsync();

                if (token != null)
                {
                    return token.ToString();
                }
            }
            catch  (Exception)
            {
                throw;
            }

            return "";
        }


    }
}
