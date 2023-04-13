using Budget_CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Budget_CoolBooks.Data
{
    public class UserInitialisation
    {
        private readonly ILogger<UserInitialisation> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserInitialisation(ILogger<UserInitialisation> logger, ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitialiseAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Adminrole defined
            var adminRole = new IdentityRole("Admin");

            // If user wit
            if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await _roleManager.CreateAsync(adminRole);
            }


            // Add new hardcoded admins
            List<string> adminNames = new List<string>();
            adminNames.Add("tobias@mail.com");
            adminNames.Add("Hafiz@mail.com");
            adminNames.Add("fredrik@mail.com");
            adminNames.Add("robin@mail.com");
            adminNames.Add("david@mail.com");

            foreach (var admin in adminNames)
            {
                var administrator = new User { UserName = admin, Email = admin };

                if (_userManager.Users.All(u => u.UserName != administrator.UserName))
                {
                    await _userManager.CreateAsync(administrator, "Admin1!"); // Sets password for admin
                    if (!string.IsNullOrWhiteSpace(adminRole.Name))
                    {
                        await _userManager.AddToRolesAsync(administrator, new[] { adminRole.Name });
                    }
                }
            }
            
        }
    }
}
