using Budget_CoolBooks.Models;
using Budget_CoolBooks.ViewModels;
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
            var moderatorRole = new IdentityRole("Moderator");

            if (_roleManager.Roles.All(r => r.Name != adminRole.Name))
            {
                await _roleManager.CreateAsync(adminRole);
            }
            if (_roleManager.Roles.All(r => r.Name != moderatorRole.Name))
            {
                await _roleManager.CreateAsync(moderatorRole);
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

        public async Task SeedUsersAsync()
        {
            try
            {
                await TrySeedUsersAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }
        public async Task TrySeedUsersAsync()
        {
            if (await _userManager.FindByEmailAsync("sallad@mail.com") == null)
            {
                var user = new User { UserName = "sallad@mail.com", Email = "sallad@mail.com", Id = "00000000-0000-0000-0000-000000000001" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("aubergine@mail.com") == null)
            {
                var user = new User { UserName = "aubergine@mail.com", Email = "aubergine@mail.com", Id = "00000000-0000-0000-0000-000000000002" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("squash@mail.com") == null)
            {
                var user = new User { UserName = "squash@mail.com", Email = "squash@mail.com", Id = "00000000-0000-0000-0000-000000000003" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("gurka@mail.com") == null)
            {
                var user = new User { UserName = "gurka@mail.com", Email = "gurka@mail.com", Id = "00000000-0000-0000-0000-000000000004" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("tomat@mail.com") == null)
            {
                var user = new User { UserName = "tomat@mail.com", Email = "tomat@mail.com", Id = "00000000-0000-0000-0000-000000000005" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("majs@mail.com") == null)
            {
                var user = new User { UserName = "majs@mail.com", Email = "majs@mail.com", Id = "00000000-0000-0000-0000-000000000006" };
                await _userManager.CreateAsync(user, "User1!");
            }
            
            if (await _userManager.FindByEmailAsync("radish@mail.com") == null)
            {
                var user = new User { UserName = "radish@mail.com", Email = "radish@mail.com", Id = "00000000-0000-0000-0000-000000000007" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("pepper@mail.com") == null)
            {
                var user = new User { UserName = "pepper@mail.com", Email = "pepper@mail.com", Id = "00000000-0000-0000-0000-000000000008" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("bean@mail.com") == null)
            {
                var user = new User { UserName = "bean@mail.com", Email = "bean@mail.com", Id = "00000000-0000-0000-0000-000000000009" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("lentil@mail.com") == null)
            {
                var user = new User { UserName = "lentil@mail.com", Email = "lentil@mail.com", Id = "00000000-0000-0000-0000-000000000010" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("fish@mail.com") == null)
            {
                var user = new User { UserName = "fish@mail.com", Email = "fish@mail.com", Id = "00000000-0000-0000-0000-000000000011" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("meat@mail.com") == null)
            {
                var user = new User { UserName = "meat@mail.com", Email = "meat@mail.com", Id = "00000000-0000-0000-0000-000000000012" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("lettuce@mail.com") == null)
            {
                var user = new User { UserName = "lettuce@mail.com", Email = "lettuce@mail.com", Id = "00000000-0000-0000-0000-000000000013" };
                await _userManager.CreateAsync(user, "User1!");
            }

            if (await _userManager.FindByEmailAsync("oat@mail.com") == null)
            {
                var user = new User { UserName = "oat@mail.com", Email = "oat@mail.com", Id = "00000000-0000-0000-0000-000000000014" };
                await _userManager.CreateAsync(user, "User1!");
            }


        }
        
    }
}
