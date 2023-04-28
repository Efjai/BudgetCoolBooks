using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.UserServices;
using Microsoft.AspNetCore.Identity;

namespace Budget_CoolBooks.ViewModels
{
    public class AdminUserViewModel
    {
        public List<User> Users { get; set; }
        public bool UpgradeResult { get; set; }

        public User User { get; set; }
        public List<Review> Reviews { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Reply> Replies { get; set; }
    }
}
