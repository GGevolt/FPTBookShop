using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace FPTBookShop.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Full_Name { get; set; }
        public string Address {  get; set; }
    }
}
