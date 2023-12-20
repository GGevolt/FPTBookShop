using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Models.ViewModel
{
	public class OrderVM
	{
		public List<OrderHeader> OrderHeaderes { get; set; }
		public IEnumerable<OrderDetail> OrderDetailes { get; set;}
	}
}
