using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XyzSystem.Models.Domain
{
    public class Product
    {
        [Key]
        public int ProductCode { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }
        public float UnitPrice { get; set; }
        

        public List<CategoryProduct>? CategoryProducts { get; set; }
    }
}
