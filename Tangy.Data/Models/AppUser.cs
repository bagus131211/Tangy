using Microsoft.AspNetCore.Identity;

namespace Tangy.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
