using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class Files
    {
		[Key]
		public int file_id { get; set; }

        public int model_id { get; set;}

        public int file_type { get; set; }

        public int file_path { get; set;}
    }
}
