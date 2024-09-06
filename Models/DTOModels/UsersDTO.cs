using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOModels
{
    public class UsersDTO
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string? UID { get; set; }

        public string? Role { get; set; }
    }
}
