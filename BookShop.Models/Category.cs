using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace FPTBookShop.Models
{
	public class Category
	{
		[Key]
		public int ID { get; set; }
		[Required]
		[DisplayName("Category Name")]
		public string Name { get; set; }
		public string Description { get; set; }

		public virtual ICollection<BookCategory>? BookCategories { get; set; }
		public ApplicationUser ApplicationUser { get; set; }
		public int UserId { get; set; }
	}
}
