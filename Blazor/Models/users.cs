using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Blazor.Models
{
    public class Users
    {
		[Key]
		public int Id { get; set; }

        public string? Name { get; set; }


        public string? Email { get; set; }


        public string? Password { get; set; }

        public string? Role { get; set; }

        public string? Picture { get; set; }

    };
    
}
