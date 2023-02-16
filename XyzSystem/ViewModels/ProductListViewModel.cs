using Microsoft.AspNetCore.Mvc.Rendering;
using XyzSystem.Models.Domain;

namespace XyzSystem.ViewModels
{
    public class ProductListViewModel
    {
        public List<Product> products { get; set; } = new List<Product>();
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem>? AllCategories { get; set; }
    }
}
