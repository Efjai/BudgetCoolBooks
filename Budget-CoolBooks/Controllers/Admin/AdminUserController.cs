using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Budget_CoolBooks.Controllers.Admin
{
    public class AdminUserController : Controller
    {
        private readonly UserServices _userServices;

        public AdminUserController(UserServices userServices)
        {
            _userServices = userServices;
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
        public async Task<IActionResult> Promote(string Id)
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
    }
}
