namespace Blazor.Models
{
    public class Category
    {
<<<<<<< Updated upstream:Blazor/Models/Categories.cs
        public int category_id { get; set; }
=======
		[Key]
		public int file_id { get; set; }
>>>>>>> Stashed changes:Blazor/Models/Category.cs

        public string? category_name { get; set; }

        public int subject_id { get; set;}
    }
}
