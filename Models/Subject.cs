using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Subject
    {
        [Key]
        public int subject_id { get; set; }

        public string? subject_name { get; set; }

        public int subject_description { get; set; }
    }
}
