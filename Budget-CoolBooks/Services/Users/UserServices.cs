﻿using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;

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
        public async Task<User> GetUserByName(string name)
        {
            return _context.Users.Where(u => u.UserName == name).FirstOrDefault();
        }
        public async Task<List<string>> GetUserRole(User user)
        {
            var result = await _userManager.GetRolesAsync(user);
            return result.ToList();
        }
        public async Task<bool> PromoteToAdmin(User user)
        {
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            return result.Succeeded ? Save() : false;
        }
        public async Task<bool> DemoteToMember(User user)
        {
            var result = await _userManager.RemoveFromRoleAsync(user, "Admin");
            return result.Succeeded ? Save() : false;
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<bool> Delete(User user)
        {
            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded ? Save() : false;
        }
    }
}
