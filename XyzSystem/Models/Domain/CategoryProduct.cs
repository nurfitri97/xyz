using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XyzSystem.Models.Domain
{
    public class CategoryProduct
    {
        [ForeignKey("Category")]
      
        public int CategoryId { get; set; }
        [ForeignKey("Product")]
        
        public int ProductCode { get; set; }

        public Product Product { get; set; }

        public Category Category { get; set; }


    }
}
