using System.ComponentModel.DataAnnotations;

namespace FireSafetyStore.Web.Client.Models
{
    public class ImageUploadModel
    {
        [Key]
        public int ImageId
        {
            get;
            set;
        }

        [Required]
        public string ImagePath
        {
            get;
            set;
        }

        [Required]
        public byte[] ByteArray
        {
            get;
            set;
        }
    }
}