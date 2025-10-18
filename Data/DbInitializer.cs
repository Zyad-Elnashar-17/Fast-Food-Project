using Fast_Food_Delievery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Fast_Food_Delievery.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _context;

        public DbInitializer(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                _roleManager.CreateAsync(new IdentityRole("Manager")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Admin")).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole("Customer")).GetAwaiter().GetResult();

                var AdminUser = new User()
                {
                    UserName = "Admin@gmail.com",
                    Email = "Admin@gmail.com",
                    FullName = "Admin",
                    City = "aaa",
                    Address = "zzz",
                    PostalCode = "20",
                };
                var result = _userManager.CreateAsync(AdminUser, "Admin@123").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(AdminUser, "Admin").GetAwaiter().GetResult();
                }
            } 
        }
    }
}
        