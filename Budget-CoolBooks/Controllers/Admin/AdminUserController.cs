using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Genres;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        private readonly UserServices _userServices;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;

        public AdminUserController(UserServices userServices, UserManager<User> userManager)
        {
            _userServices = userServices;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userList = await _userServices.GetUsers();
            if (userList == null)
            {
                return NotFound();
            }

            var adminUserViewModel = new AdminUserViewModel()
            {
                Users = userList.ToList(),
            };

            return View("~/views/admin/user/index.cshtml", adminUserViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> PromoteToAdmin(string Id)
        {
            var user = await _userServices.GetUserById(Id);
            if (user == null)
            {
                return NotFound();
            }
            var adminUserViewModel = new AdminUserViewModel();


            if (!await _userServices.PromoteToAdmin(user))
            {
                adminUserViewModel.UpgradeResult = false;
            }
            else
            {
                adminUserViewModel.UpgradeResult = true;
            }

            return RedirectToAction("Index", adminUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PromoteToModerator(string Id)
        {
            var user = await _userServices.GetUserById(Id);
            if (user == null)
            {
                return NotFound();
            }

            var adminUserViewModel = new AdminUserViewModel();

            if (!await _userServices.PromoteToModerator(user))
            {
                adminUserViewModel.UpgradeResult = false;
            }

            else { adminUserViewModel.UpgradeResult = true; }

            return RedirectToAction("Index", adminUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string Id)
        {
            var user = await _userServices.GetUserById(Id);
            if (user == null)
            {
                return NotFound();
            }
            var adminUserViewModel = new AdminUserViewModel();


            if (!await _userServices.DemoteToMember(user))
            {
                adminUserViewModel.UpgradeResult = false;
            }
            else
            {
                adminUserViewModel.UpgradeResult = true;
            }

            return RedirectToAction("Index", adminUserViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _userServices.GetUserById(id);
            if (result == null)
            {
                return BadRequest(ModelState);
            }
            var adminUserViewModel = new AdminUserViewModel();

            if (!await _userServices.Delete(result))
            {
                adminUserViewModel.UpgradeResult = false;
            }
            else
            {
                adminUserViewModel.UpgradeResult = true;
            }
            return RedirectToAction("Index", adminUserViewModel);
        }

        // SORTING / FILTERING

        [HttpGet]
        public async Task<IActionResult> SortByFlags(IFormCollection form)
        {
            var usersByFlags = await _userServices.SortUsersByFlag();
            if (usersByFlags == null)
            {
                return BadRequest();
            }
            var adminUserViewModel = new AdminUserViewModel();
            adminUserViewModel.Users = usersByFlags.ToList();

            return View("~/views/admin/user/index.cshtml", adminUserViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SortByName()
        {
            var usersByName = await _userServices.SortUsersByName();
            if (usersByName == null)
            {
                return BadRequest();
            }
            var adminUserViewModel = new AdminUserViewModel();
            adminUserViewModel.Users = usersByName.ToList();

            return View("~/views/admin/user/index.cshtml", adminUserViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SortByRoles(int roleChoice)
        {
            var userList = await _userServices.GetUsers();
            if (userList == null)
            {
                return NotFound();
            }
            var adminUserViewModel = new AdminUserViewModel();

            var userManager = _userManager;

            List<User> SortedList = new List<User>();

            if (roleChoice == 1)
            {
                adminUserViewModel.Users = userList.ToList();
                return View("~/views/admin/user/index.cshtml", adminUserViewModel);
            }

            if (roleChoice == 2)
            {
                foreach (var user in userList)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles.Contains("Admin"))
                    {
                        SortedList.Add(user);
                    }
                }
            }

            if (roleChoice == 3)
            {
                foreach (var user in userList)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles.Contains("Moderator"))
                    {
                        SortedList.Add(user);
                    }
                }
            }

            if (roleChoice == 4)
            {
                foreach (var user in userList)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles == null || roles.Count == 0)
                    {
                        SortedList.Add(user);
                    }
                }
            }

            adminUserViewModel.Users = SortedList.ToList();
            return View("~/views/admin/user/index.cshtml", adminUserViewModel);
            }
        }
    }




