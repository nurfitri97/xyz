using Microsoft.AspNetCore.Mvc.Rendering;
using XyzSystem.Models.Domain;

namespace XyzSystem.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; } = new();
        public IEnumerable<SelectListItem>? AllCategories { get; set; }

        private List<int>? _selectedCategories;
        public List<int>? SelectedCategories
        {
            get
            {
                if (_selectedCategories == null)
                {
                    _selectedCategories = Product?.Categories?.Select(m => m.CategoryId).ToList();
                }
                return _selectedCategories;
            }
            set { _selectedCategories = value; }
        }
    }
}
