using System.ComponentModel.DataAnnotations;

namespace FireSafetyStore.Web.Client.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}