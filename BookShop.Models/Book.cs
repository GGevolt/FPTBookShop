﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FPTBookShop.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        [Range(1, 10000)]
        public double Price { get; set; }
		[ValidateNever]
		public string? Book_Image { get; set; }
		[Range(1,10000)]
		public int Quantity { get; set; }
		[ValidateNever]
		public virtual ICollection<BookCategory>? BookCategories { get; set; }
    }
}
