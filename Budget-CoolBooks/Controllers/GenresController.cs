using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.AspNetCore.Authorization;
using System.Composition;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Budget_CoolBooks.Controllers
{
    [Authorize(Roles = "Admin")]
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
            return _context.Genres != null ?
                        View(await _context.Genres.Where(b => b.IsDeleted == false).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Genres'  is null.");
        }

        // GET: AddOrEdit
        public async Task<IActionResult> AddOrEdit(int? id)
        {
            if (id == null) return View(new Genre());
            else
            {
                var genre = await _context.Genres.FindAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }
                return View(genre);
            }
        }

        // POST: AddOrEdit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Id,Name,Description,Created")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    _context.Add(genre);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        _context.Update(genre);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!GenreExists(genre.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                return Json(new { isValid = true, html = RenderRazorViewToString(this, "_ViewAll", _context.Genres.Where(b => b.IsDeleted == false).ToList()) });
            }
            return Json(new { isValid = false, html = RenderRazorViewToString(this, "AddOrEdit", genre) });
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genre = await _context.Genres.FindAsync(id);
            genre.IsDeleted = true;
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return Json(new { html = RenderRazorViewToString(this, "_ViewAll", _context.Genres.Where(b => b.IsDeleted == false).ToList()) });
        }

        private bool GenreExists(int id)
        {
            return (_context.Genres?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static string RenderRazorViewToString(Controller controller, string viewName, object model = null)
        {
            controller.ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                IViewEngine viewEngine = controller.HttpContext.RequestServices.GetService(typeof(ICompositeViewEngine)) as ICompositeViewEngine;
                ViewEngineResult viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    controller.ControllerContext,
                    viewResult.View,
                    controller.ViewData,
                    controller.TempData,
                    sw,
                    new HtmlHelperOptions()
                );
                viewResult.View.RenderAsync(viewContext);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
