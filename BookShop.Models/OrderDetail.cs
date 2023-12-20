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
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderID { get; set; }
        [ForeignKey("OrderHeaderID")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int ProductID { get; set; }
        [ForeignKey("ProductID")]
        [ValidateNever]
        public Book book { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

    }
}
