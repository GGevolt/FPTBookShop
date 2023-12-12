using FPTBookShop.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        public int BookID { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [ForeignKey("BookID")]
        [InverseProperty("OrderDetails")]
        public virtual Book Book { get; set; }

        [ForeignKey("OrderID")]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }
    }

}
