using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Budget_CoolBooks.Controllers
{
    public class ChartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static string _commentFilter = "Day"; 

        public ChartController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [HttpPost]
        public IActionResult Comment(string? filter)
        {
            if (filter != null)  _commentFilter = filter;

            switch (_commentFilter)
            {
                case "Day":
                    ViewData["commentLabels"] = _context.Comments.GroupBy(x => x.Created.Date)
                        .Select(grp => grp.Key.ToString("yy-MM-dd")).ToList();

                    ViewData["commentData"] = _context.Comments.GroupBy(x => x.Created.Date)
                        .Select(grp => grp.Count()).ToList();
                    break;

                case "Week":
                    ViewData["commentLabels"] = new List<string>(){ "Art1", "Biography", "Business", "Children's", "Christian" };
                    ViewData["commentData"] = new List<int>() { 4, 20, 18, 8, 50 };
                    break;

                case "Author":
                    break;

                case "Genre":
                    break;

                default:
                    break;
            }


            ViewData["commentFilter"] = _commentFilter;

            return View();
        }

    }
}
