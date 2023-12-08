using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FPTBookShopWeb.Models.ViewModel
{
    public class BookVM
    {
        public Book Book { get; set; }
        public List<int>? SelectedCategories { get; set; }
        [ValidateNever]
        public List<Category> Categories { get; set; }
    }
}
