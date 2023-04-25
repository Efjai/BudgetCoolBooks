using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Comments;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserServices _userServices;
        private readonly ReviewServices _reviewServices;
        private readonly CommentServices _commentServices;
        private readonly BookServices _bookServices;
        private readonly AuthorServices _authorServices;

        public UserController(UserServices userServices, ReviewServices reviewServices, CommentServices commentServices, BookServices bookServices, AuthorServices authorServices)
        {
            _userServices = userServices;
            _reviewServices = reviewServices;
            _commentServices = commentServices;
            _bookServices = bookServices;
            _authorServices = authorServices;
        }
        [HttpPost]
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

        [HttpPost]
        public async Task<IActionResult> LikeReview(int reviewId, int id)
        {
            var likeReview = await _reviewServices.GetSpecificReviewByID(reviewId);
            if (likeReview == null)
            {
                return NotFound();
            }
            likeReview.Like = likeReview.Like + 1;

            if (!await _userServices.LikeReviewById(likeReview))
            {
                return BadRequest();
            }
            return RedirectToAction("BookDetails", "Book", new { id = id });
        }
        [HttpPost]
        public async Task<IActionResult> DislikeReview(int reviewId, int id)
        {
            var dislikeReview = await _reviewServices.GetSpecificReviewByID(reviewId);
            if (dislikeReview == null)
            {
                return NotFound();
            }
            dislikeReview.Like = dislikeReview.Like + 1;

            if (!await _userServices.DislikeReviewById(dislikeReview))
            {
                return BadRequest();
            }
            return RedirectToAction("BookDetails", "Book", new { id = id });
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> UserReviews()
        {

            // Validate the id for user.
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Could not find user");
                return StatusCode(500, ModelState);
            }

            var reviewResult = await _reviewServices.GetReviewByUserId(currentUserID);
            if (reviewResult == null)
            {
                return NotFound();
            }

            var bookResult = await _bookServices.GetAllBooksSorted();
            if (bookResult == null)
            {
                return NotFound();
            }

            var authorResult = await _authorServices.GetAuthors();
            if (reviewResult == null)
            {
                return NotFound();
            }

            var viewmodel = new ReviewcardViewModel
            {
                Review = reviewResult,
                ReviewAuthor = (List<Author>)authorResult,
                ReviewBook = (List<Book>)bookResult,
            };



            return View("UserReviews", viewmodel);
        }

        public async Task<IActionResult> UserComments(int id)
        {
            // Validate the id for user.
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Could not find user");
                return StatusCode(500, ModelState);
            }

            var reviewResult = await _reviewServices.GetReviewByUserId(currentUserID);
            if (reviewResult == null)
            {
                return NotFound();
            }

            var commentResults = await _commentServices.GetCommentByUserId(currentUserID);

            var viewmodel = new ReviewcardViewModel
            {
                Review = reviewResult,
                ReviewComment = commentResults,
            };



            return View("UserComments", viewmodel);
        }

    }
}
