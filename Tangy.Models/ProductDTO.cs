using System.ComponentModel.DataAnnotations;

namespace Tangy.Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool ShopFavourites { get; set; }
        public bool CustomerFavourites { get; set; }
        public string Colour { get; set; }
        public string ImageUri { get; set; }
        [Range(1, int.MaxValue, ErrorMessage ="Please select a category")]
        public int CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
