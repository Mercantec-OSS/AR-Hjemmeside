using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FileName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? Picture { get; set; }
    }
}

public class LoginModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
    }
}
