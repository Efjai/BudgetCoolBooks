using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Comments;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Budget_CoolBooks.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserServices _userServices;
        private readonly ReviewServices _reviewServices;
        private readonly CommentServices _commentServices;

        public UserController(UserServices userServices, ReviewServices reviewServices, CommentServices commentServices)
        {
            _userServices = userServices;
            _reviewServices = reviewServices;
            _commentServices = commentServices;
        }

        public async Task<IActionResult> FlagReview(int reviewId, int id)
        {
            var review = await _reviewServices.GetReviewByID(reviewId);
            if (review == null)
            {
                return NotFound();
            }
            review.Flag = review.Flag + 1;

            if (!await _userServices.FlagReviewById(review))
            {
                return BadRequest();
            }
            return RedirectToAction("BookDetails", "Book", new { id = id });
        }

        public async Task<IActionResult> FlagComment(int commentId, int id)
        {
            var comment = await _commentServices.GetCommentById(commentId);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Flag = comment.Flag + 1;

            if (!await _userServices.FlagCommentById(comment))
            {
                return BadRequest();
            }
            return RedirectToAction("BookDetails", "Book", new { id = id });
        }

        public async Task<IActionResult> FlagReply(int replyId, int id)
        {
            var reply = await _commentServices.GetReplyById(replyId);
            if (reply == null)
            {
                return NotFound();
            }
            reply.Flag = reply.Flag + 1;

            if (!await _userServices.FlagReplyById(reply))
            {
                return BadRequest();
            }
            return RedirectToAction("BookDetails", "Book", new { id = id });
        }
    }
}
