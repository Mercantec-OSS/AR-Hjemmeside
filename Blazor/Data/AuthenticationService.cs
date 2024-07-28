using ARClassLibrary; // Assuming this is where your AppDbContext and User model are defined
using Blazor.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AuthenticationService
{
    private readonly AppDbContext _context;

    public AuthenticationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> AuthenticateUser(string userName, string password)
    {
        var user = await _context.Users
                                 .FirstOrDefaultAsync(u => u.UserName == userName);

        if (user == null)
        {
            return false;
        }

        // Assuming you have a method to verify the password hash
        // This is a placeholder. Implement password verification based on your hashing strategy
        return PasswordHelper.VerifyPassword(user.Password, password);
    }
}

