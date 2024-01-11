using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Webshop.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [ForeignKey("CategoryID")]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
