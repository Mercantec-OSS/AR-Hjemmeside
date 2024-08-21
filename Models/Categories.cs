using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Categories
    {
        [Key]
        public int category_id { get; set; }

        public string? category_name { get; set; }

        public int subject_id { get; set; }
    }
}
