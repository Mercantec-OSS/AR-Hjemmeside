using Models;

namespace Repository.IRepository
{
    public interface IUserRepo
    {
        Task<string> UserLoginAsync(UsersDTO DTOuser);
    }
}
