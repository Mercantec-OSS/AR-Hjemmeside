using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Files
    {
        [Key]
        public int file_id { get; set; }

        public int model_id { get; set; }

        public int file_type { get; set; }

        public int file_path { get; set; }
    }
}
