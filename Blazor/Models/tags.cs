using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class Tags
    {
		[Key]
		public int id { get; set; }

        public string? tag_name { get; set; }
    }
}
