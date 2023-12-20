using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Models.ViewModel
{
    public class ShoppingcartVM
    {
        public IEnumerable<ShoppingCart> ShoppingcartList { get; set; }
        public OrderHeader OrderHeader { get; set; }
    }
}
