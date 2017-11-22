using System.ComponentModel.DataAnnotations;

namespace FireSafetyStore.Web.Client.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}