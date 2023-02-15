using System.ComponentModel.DataAnnotations;

namespace XyzSystem.Models.Domain
{
    public class Category
    {
       
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required")]
        public string? CategoryName { get; set; }

        public List<Product>? Products { get; set;}
    }
}
