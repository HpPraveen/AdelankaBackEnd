using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Adelanka.DAL.Models
{
    public class SystemUser
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Is Active")]
        [DefaultValue("true")]
        public bool IsActive { get; set; }
    }
}