using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class Categories
    {
		[Key]
		public int category_id { get; set; }

        public string? category_name { get; set; }

        public int subject_id { get; set;}
    }
}
