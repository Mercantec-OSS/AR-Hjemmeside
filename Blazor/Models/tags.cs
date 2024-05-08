using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class tags
    {
		[Key]
		public int id { get; set; }

        public string? tag_name { get; set; }
    }
}
