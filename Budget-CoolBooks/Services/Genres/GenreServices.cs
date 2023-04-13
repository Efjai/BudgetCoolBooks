using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Budget_CoolBooks.Services.Genres
{
    public class GenreServices
    {
        private readonly ApplicationDbContext _context;

        public GenreServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Genre>> GetGenres()
        {
            return _context.Genres.OrderBy(p => p.Id).ToList();
        }

        public async Task<bool> GenreExists(int id)
        {
            return await _context.Authors.AnyAsync(g => g.Id == id);
        }

        public async Task<bool> CreateGenre(Genre genre)
        {
            _context.Genres.Add(genre);
            return Save();
           
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}


