using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTBookShop.Models.ViewModel
{
	public class ChangePassVM
	{
		[Required]
		public string userID { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at max {1} characters long.")]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[^\w\s]).{6,}$", ErrorMessage = "Password must contain at least one number, one special character, one uppercase letter, and be at least 8 characters long.")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
    }
}
