using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PasswordHash
    {
        private static string GetRandomSalt() // Creates a random salt thats used in the hashed password
        {
            return BCrypt.Net.BCrypt.GenerateSalt();
        }
        public static string HashPassword(string password) // This method hashes the password and calls GetRandomSalt to add salt to the hashed password
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GetRandomSalt());
        }
        public static bool ValidatePassword(string password, string correctHash) // Matches a given password against the hashed password to see if they are the same
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
