using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers
{
    [Authorize]
        
    public class CommentController : Controller
    {
            private readonly ReviewServices _ReviewServices;
            private readonly BookServices _bookServices;
            private readonly UserServices _userServices;
        public CommentController(ReviewServices reviewServices, BookServices bookServices, UserServices userServices)
        {
            _ReviewServices = reviewServices;
            _bookServices = bookServices;
            _userServices = userServices;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
