using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Tags
    {
        [Key]
        public int id { get; set; }

        public string? tag_name { get; set; }
    }
}
