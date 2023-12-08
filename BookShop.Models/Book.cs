using System.ComponentModel.DataAnnotations;

namespace FPTBookShopWeb.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        [Range(1,1000)]
        public double Price { get; set; }
        public string? Book_Image { get; set; }
        public int Quantity { get; set; }
		public virtual ICollection<BookCategory>? BookCategories { get; set; }
	}

}
