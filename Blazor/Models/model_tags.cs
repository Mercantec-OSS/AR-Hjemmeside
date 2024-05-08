using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class model_tags
    {
		[Key]
		public int model_id { get; set; }

        public string? tag_id { get; set;}
    }
}
