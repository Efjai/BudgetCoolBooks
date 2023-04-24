using Budget_CoolBooks.Models;
using Humanizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Budget_CoolBooks.Data;

namespace Budget_CoolBooks.Services.UserServices
{
    public class UserServices
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserServices(ApplicationDbContext context, UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ICollection<User>> GetUsers()
        {
            return _context.Users.OrderBy(u => u.Id).ToList();
        }

        public async Task<User> GetUserById(string id)
        {
            return _context.Users.Where(u => u.Id == id).FirstOrDefault();
        }


        // ROLE - FUNCTIONS
        public async Task<bool> PromoteToAdmin(User user)
        {
            if (await _userManager.IsInRoleAsync(user, "Moderator"))
            {
               var removeModRole = await _userManager.RemoveFromRoleAsync(user, "Moderator");
            }

            var result = await _userManager.AddToRoleAsync(user, "Admin");
            return result.Succeeded ? Save() : false;
        }
        public async Task<bool> PromoteToModerator(User user)
        {
            var result = await _userManager.AddToRoleAsync(user, "Moderator");
            return result.Succeeded ? Save() : false;
        }
        public async Task<bool> DemoteToMember(User user)
        {
            if (await _userManager.IsInRoleAsync(user, "Moderator"))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, "Moderator");
                return result.Succeeded ? Save() : false;
            }

            if (await _userManager.IsInRoleAsync(user, "Admin"))
            {
                var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
                return result.Succeeded ? Save() : false;
            }

            return false;
        }

        public async Task<bool> Delete(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Save() : false;
        }


        // FLAGGING - FUNCTIONS
        public async Task<bool> FlagReviewById(Review review)
        {
            _context.Reviews.Update(review);
            return Save();
        }
        public async Task<bool> FlagCommentById(Comment comment)
        {
            _context.Comments.Update(comment);
            return Save();
        }
        public async Task<bool> FlagReplyById(Reply reply)
        {
            _context.Replys.Update(reply);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
