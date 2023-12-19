using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.UtcNow;
        [Required]
        public string UserID { get; set; }
        [Required]
        public int OrderStatusID { get; set; }
        public bool IsDelete { get; set; } = false;
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
