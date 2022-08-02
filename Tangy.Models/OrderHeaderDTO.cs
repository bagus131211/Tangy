using System.ComponentModel.DataAnnotations;

namespace Tangy.Models
{
    public class OrderHeaderDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Order Total")]
        public double Total { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required, Display(Name ="Shipping Date")]
        public DateTime ShippingDate { get; set; }
        [Required]
        public string Status { get; set; }
        public string? SessionId { get; set; }
        public string? PaymentIntentId { get; set; }
        
        [Required]
        public string Name { get; set; }
        [Required, Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Required, Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string City { get; set; }
        [Required, Display(Name = "Postal Codes")]
        public string PostalCode { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
