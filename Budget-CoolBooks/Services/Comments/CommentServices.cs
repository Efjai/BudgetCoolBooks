using Budget_CoolBooks.Data;
using Budget_CoolBooks.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Budget_CoolBooks.Services.Comments
{
    public class CommentServices
    {
        private readonly ApplicationDbContext _context;

        public CommentServices(ApplicationDbContext context)
        {
            _context = context;
        }
        // COMMENTS 
        public async Task<Comment> GetCommentById(int id)
        {
            return _context.Comments.Where(c => c.Id == id).FirstOrDefault();
        }
        public async Task<bool> DeleteComment(Comment comment)
        {
            _context.Comments.Remove(comment);
            return Save();
        }

        // REPLIES
        public async Task<Reply> GetReplyById(int id)
        {
            return _context.Replys.Where(c => c.Id == id).FirstOrDefault();
        } 
        public async Task<bool> DeleteReply(Reply reply)
        {
            _context.Replys.Remove(reply);
            return Save();
        }
        public async Task<List<Reply>> GetAllReplysOfComments(int id)
        {
            return _context.Replys.Include(r => r.User).Include(r => r.Comment).Where(r => r.Comment.Id == id).ToList();
        }
        // OTHER
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
        public async Task<List<Comment>> GetAllCommentsOfReview(int id)
        {
            return _context.Comments.Include(r => r.User).Where(r => r.Review.Id == id).ToList();
        }
        public async Task<bool> CreateComment(Comment comment, string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }

            comment.User = user;


            _context.Comments.Add(comment);

            return Save();
        }
        public async Task<IList<int>> GetAllIdOfComments(int id)
        {
            return _context.Comments.Where(r => r.Review.Id == id).Select(r => r.Id).ToList();
        }
    }
}
