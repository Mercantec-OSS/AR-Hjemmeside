using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

public static class PasswordHelper
{
    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        // Split the hash into the salt and the hash parts.
        var parts = hashedPassword.Split('.', 2);
        if (parts.Length != 2)
        {
            throw new InvalidOperationException("The hashed password is not in the expected format.");
        }

        var salt = Convert.FromBase64String(parts[0]);
        var hash = Convert.FromBase64String(parts[1]);

        // Hash the provided password with the same salt
        var providedHash = KeyDerivation.Pbkdf2(
            password: providedPassword,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8);

        // Convert the provided hash to base64 for comparison
        var providedHashBase64 = Convert.ToBase64String(providedHash);

        // Compare the hashes
        return hash.SequenceEqual(providedHash);
    }
}
