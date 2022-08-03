using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tangy.Common;
using Tangy.Data;

namespace TangyWeb.Server.Service
{
    public class DbInitializer : IDbInitializer
    {
        readonly UserManager<IdentityUser> _userManager;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly AppDbContext _context;

        public DbInitializer(UserManager<IdentityUser> userManager, 
            RoleManager<IdentityRole> roleManager,
            AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
                if (!_roleManager.RoleExistsAsync(Constants.Role.Admin).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(Constants.Role.Admin)).GetAwaiter().GetResult();
                    _roleManager.CreateAsync(new IdentityRole(Constants.Role.Customer)).GetAwaiter().GetResult();
                }
                else
                    return;

                IdentityUser user = new()
                {
                    UserName = "b.susetyo@creditinfo.com",
                    Email = "b.susetyo@creditinfo.com",
                    EmailConfirmed = true,                    
                };

                _userManager.CreateAsync(user, "P@ssw0rd.1").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, Constants.Role.Admin).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
