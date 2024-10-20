using Blazor.DBLayer;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Models;
using Models.DTOModels;
using NuGet.Common;

namespace Blazor.Repository
{
    public class UserRepo
    {
        private const string LoginToken = "eow";

        private readonly ProtectedLocalStorage _storageService;
        private readonly UserDB _userDB;

        public UserRepo(ProtectedLocalStorage storageService)
        {
            _storageService = storageService;
            _userDB = new UserDB();
        }

        public async Task<bool> UserLogin(UsersDTO userDTO)
        {
            var token = await _userDB.UserLogin(userDTO);

            if (token == null || token == string.Empty)
            {
                return false;
            }

            await _storageService.SetAsync(LoginToken, token);

            return true;
        }

        public async Task<bool> Logout()
        {
            await _storageService.DeleteAsync(LoginToken);

            return true;
        }

        public async Task<bool> CreateUser(Users user)
        {
            var result = await _userDB.CreateUser(user);

            if (result == true)
            {
                return true;
            }

            return false;
        }

    }
}
