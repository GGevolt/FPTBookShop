using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Models
{
    public class ShoppingCart
    {
        [Key]
        public int Id { get; set; }
        public int BookID { get; set; }
        [ForeignKey("BookID")]
        [ValidateNever]
        public Book book { get; set; }
        [Range(1, 188, ErrorMessage = "188bet friend of people")]
        public int Count { get; set; }
        public string UserID { get; set; }
        [NotMapped]
        public double Price { get; set; }
    }
}
