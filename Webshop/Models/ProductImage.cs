using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Webshop.Models
{
    public class ProductImage
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }

        public bool IsMainImage { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        public string FileName { get; set; }

        [NotMapped]
        public string ProductTitle { get; set; }


    }
}