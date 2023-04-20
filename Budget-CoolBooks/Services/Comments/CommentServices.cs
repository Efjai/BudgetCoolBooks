﻿using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Budget_CoolBooks.Services.Comments
{
    public class CommentServices
    {
        private readonly ApplicationDbContext _context;

        public CommentServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return _context.Comments.Where(c => c.Id == id).FirstOrDefault();
        }

        public async Task<bool> DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }


    }
}
