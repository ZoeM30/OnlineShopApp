using System.ComponentModel.DataAnnotations;

namespace OnlineShopApp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name ="Display Order")]
        [Range(1,100,ErrorMessage ="Display order value is between 1 and 100")]
        public int DisplayOrder { get; set; } 

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
