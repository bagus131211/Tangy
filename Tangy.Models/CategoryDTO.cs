using System.ComponentModel.DataAnnotations;

namespace Tangy.Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter Name")]
        public string Name { get; set; }
    }
}
