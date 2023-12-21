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
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Search {  get; set; }
        public string Sort { get; set; }
    }
}
