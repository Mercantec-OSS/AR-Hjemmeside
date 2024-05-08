using System.ComponentModel.DataAnnotations;

namespace Blazor.Models
{
    public class Subject
    {
		[Key]
		public int subject_id { get; set; }

        public string? subject_name { get; set; }

        public int subject_description { get; set;}
    }
}
