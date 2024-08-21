using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class model_tags
    {
        [Key]
        public int model_id { get; set; }

        public string? tag_id { get; set; }
    }
}
