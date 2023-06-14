using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ToDoList.Models
{
    public class User_Model
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Display(Name = "FullName")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Address")]
        [MaxLength(100)]
        [Required]
        public string Address { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [Required]
        [StringLength(100, ErrorMessage = "minemum length is 4 ", MinimumLength = 4)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword")]
        [Required]
        [StringLength(100, ErrorMessage = "minemum length is 4 ", MinimumLength = 4)]
        [Compare("Password", ErrorMessage = "password and confirmation not match")]
        public string ConfirmPassword { get; set; }
        public bool IAgree { get; set; }
    }
}
