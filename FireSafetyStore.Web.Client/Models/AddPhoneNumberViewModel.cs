using System.ComponentModel.DataAnnotations;

namespace FireSafetyStore.Web.Client.Models
{
    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

}