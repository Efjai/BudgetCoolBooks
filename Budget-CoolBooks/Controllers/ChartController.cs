using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Collections.Generic;

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
            ViewData["commentFilter"] = _commentFilter;

            // Comment Filter Controller
            switch (_commentFilter)
            {
                case "Day":
                    ViewData["chartLabels"] = _context.Comments.GroupBy(x => x.Created.Date)
                        .Select(grp => grp.Key.ToString("yy-MM-dd")).ToList();

                    ViewData["chartData"] = _context.Comments.GroupBy(x => x.Created.Date)
                        .Select(grp => grp.Count()).ToList();
                    break;

                case "Week":
                    var weeks = _context.Comments.GroupBy(x => x.Created.Date)
                        .Select(grp => new { label = ISOWeek.GetWeekOfYear(grp.Key), data = grp.Count() }).ToList();

                    ViewData["chartLabels"] = weeks.GroupBy(x => x.label).Select(a => a.Key).ToList(); ;
                    ViewData["chartData"] = weeks.GroupBy(x => x.label).Select(a => a.Sum(b => b.data)).ToList(); 
                    break;

                case "Author":
                    ViewData["chartLabels"] = _context.CommentsAuthorsFromViews.GroupBy(a => a.Firstname)
                        .Select(grp => grp.Key).ToList();

                    ViewData["chartData"] = _context.CommentsAuthorsFromViews.GroupBy(a => a.Firstname)
                        .Select(grp => grp.Count()).ToList();
                    break;

                case "Genre":
                    ViewData["chartLabels"] = _context.CommentsGenresFromViews.GroupBy(g => g.Name)
                        .Select(grp => grp.Key).ToList();

                    ViewData["chartData"] = _context.CommentsGenresFromViews.GroupBy(g => g.Name)
                        .Select(grp => grp.Count()).ToList();
                    break;

            }


            return View();
        }

    }
}
