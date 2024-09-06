using Models;
using Models.DTOModels;

namespace Repository.IRepository
{
    public interface IUserRepo
    {
        Task<List<Users>> GetAllUsersAsync();
        Task<string> UserLoginAsync(UsersDTO DTOuser);

        Task<bool> CreateUserAsync(Users user);

        Task<bool> DeleteUserAsync(DeleteUserDTO DTODUser);
    }
}
