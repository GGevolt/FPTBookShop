using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Models.ViewModel
{
    public class HomeVM
    {
        public List<Book> Books {  get; set; }
        public List<Category>? Categories { get; set; }
    }
}
