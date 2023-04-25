using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Comments;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers
{
    [Authorize]
        
    public class CommentController : Controller
    {
        private readonly ReviewServices _ReviewServices;
        private readonly BookServices _bookServices;
        private readonly UserServices _userServices;
        private readonly CommentServices _commentServices;

        public CommentController(ReviewServices reviewServices, BookServices bookServices, UserServices userServices,CommentServices commentServices)
        {
            _ReviewServices = reviewServices;
            _bookServices = bookServices;
            _userServices = userServices;
            _commentServices = commentServices;
        }

        // COMMENTS
        public async Task<IActionResult> Index(int reviewId, int bookId)
        {
            var review = await _ReviewServices.GetReviewByID(reviewId);
            if (review == null)
            {
                return NotFound();
            }

            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.Review = review;
            commentViewModel.BookId = bookId;
            return View(commentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string commentText, int reviewId, int bookId)
        {
            Comment comment = new Comment();
            comment.Text = commentText;
            comment.Created = DateTime.Now;

            comment.Review = await _ReviewServices.GetReviewByID(reviewId);
            if (comment.Review == null)
            {
                return NotFound();
            }

            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                return NotFound();
            }
            var user = await _userServices.GetUserById(currentUserID);
            if (user == null)
            {
                return NotFound();
            }
            comment.User = user;

            if (!await _commentServices.CreateComment(comment))
            {
                return BadRequest();
            }

            return RedirectToAction("BookDetails", "Book", new { id = bookId });
        }

        [HttpGet]
        public async Task<IActionResult> EditComment(int commentId, int bookId)
        {
            var comment = await _commentServices.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.Comment = comment;
            commentViewModel.BookId = bookId;
            return View("~/views/comment/edit.cshtml", commentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditComment(string editedText, int commentId, int bookId)
        {
            var comment = await _commentServices.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Text = editedText;
            if (!await _commentServices.UpdateComment(comment))
            {
                return BadRequest();
            }

            return RedirectToAction("BookDetails", "Book", new { id = bookId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteComment(int commentId, int bookId)
        {
            var comment = await _commentServices.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            if (!await _commentServices.DeleteComment(comment))
            {
                return BadRequest();
            }

            return RedirectToAction("BookDetails", "Book", new { id = bookId });
        }




        // REPLIES
        public async Task<IActionResult> ReplyIndex(int commentId, int bookId)
        {
            var comment = await _commentServices.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }

            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.Comment = comment;
            commentViewModel.BookId = bookId;
            return View("~/views/comment/replies/index.cshtml", commentViewModel);
        }
       
        public async Task<IActionResult> AddReply(string replyText, int commentId, int bookId)
        {
            Reply reply = new Reply();
            reply.Text = replyText;
            reply.Created = DateTime.Now;

            reply.Comment = await _commentServices.GetCommentById(commentId);
            if (reply.Comment == null)
            {
                return NotFound();
            }

            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                return NotFound();
            }
            var user = await _userServices.GetUserById(currentUserID);
            if (user == null)
            {
                return NotFound();
            }
            reply.User = user;

            if (!await _commentServices.CreateReply(reply))
            {
                return BadRequest();
            }

            return RedirectToAction("BookDetails", "Book", new { id = bookId });
        }

        [HttpGet]
        public async Task<IActionResult> EditReply(int replyId, int bookId)
        {
            var reply = await _commentServices.GetReplyById(replyId);
            if (reply == null)
            {
                return NotFound();
            }

            CommentViewModel commentViewModel = new CommentViewModel();
            commentViewModel.Reply = reply;
            commentViewModel.BookId = bookId;
            return View("~/views/comment/replies/edit.cshtml", commentViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditReply(string editedReply, int replyId, int bookId)
        {
            var reply = await _commentServices.GetReplyById(replyId);
            if (reply == null)
            {
                return NotFound();
            }
            reply.Text = editedReply;
            if (!await _commentServices.UpdateReply(reply))
            {
                return BadRequest();
            }

            return RedirectToAction("BookDetails", "Book", new { id = bookId });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReply(int replyId, int bookId)
        {
            var reply = await _commentServices.GetReplyById(replyId);
            if (reply == null)
            {
                return NotFound();
            }
            if (!await _commentServices.DeleteReply(reply))
            {
                return BadRequest();
            }

            return RedirectToAction("BookDetails", "Book", new { id = bookId });
        }
    }
}
