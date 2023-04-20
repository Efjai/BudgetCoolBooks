using Budget_CoolBooks.Services.Comments;
using Budget_CoolBooks.Services.Moderators;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Budget_CoolBooks.Controllers
{
    [Authorize(Roles = "Admin, Moderator")]
    public class ModeratorController : Controller
    {
        private readonly ModeratorServices _moderatorServices;
        private readonly ReviewServices _reviewServices;
        private readonly CommentServices _commentServices;

        public ModeratorController(ModeratorServices moderatorServices, ReviewServices reviewServices,
            CommentServices commentServices)
        {
            _moderatorServices = moderatorServices;
            _reviewServices = reviewServices;
            _commentServices = commentServices;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View("~/views/Moderator/index.cshtml");
        }


        // View and moderate REVIEWS
        [HttpGet]
        public async Task<IActionResult> IndexReviews()
        {
            // Gets all flagged reviews
            var reviews = await _moderatorServices.GetFlaggedReviews();

            var moderatorViewModel = new ModeratorViewModel();
            moderatorViewModel.Reviews = reviews.ToList();

            //Viewmodel for all data
            return View("~/views/Moderator/reviews/index.cshtml", moderatorViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AuditReviews(int Id)
        {
            // Gets all flagged reviews
            var review = await _reviewServices.GetReviewByID(Id);

            var moderatorViewModel = new ModeratorViewModel();
            moderatorViewModel.Review = review;

            //Viewmodel for all data
            return View("~/views/Moderator/reviews/audit.cshtml", moderatorViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> UnflagReview(int Id)
        {
            //Unflag review
            var review = await _reviewServices.GetReviewByID(Id);
            if(review == null)
            {
                return NotFound();
            }
            review.Flag = 0;
            if(!await _moderatorServices.UnflagReview(review))
            {
                return BadRequest();
            }

            return RedirectToAction("IndexReviews");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteReview(int Id)
        {
            //Unflag review
            var review = await _reviewServices.GetReviewByID(Id);
            if (review == null)
            {
                return NotFound();
            }

            if (!await _reviewServices.DeleteReview(review))
            {
                return BadRequest();
            }

            return RedirectToAction("IndexReviews");
        }


        // View and moderate COMMENTS
        [HttpGet]
        public async Task<IActionResult> IndexComments()
        {
            // Gets all flagged reviews
            var comments = await _moderatorServices.GetFlaggedComments();

            var moderatorViewModel = new ModeratorViewModel();
            moderatorViewModel.Comments = comments.ToList();

            //Viewmodel for all data
            return View("~/views/Moderator/Comments/index.cshtml", moderatorViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AuditComments(int Id)
        {
            // Gets all flagged reviews
            var comment = await _commentServices.GetCommentById(Id);

            var moderatorViewModel = new ModeratorViewModel();
            moderatorViewModel.Comment = comment;

            //Viewmodel for all data
            return View("~/views/Moderator/comments/audit.cshtml", moderatorViewModel);
        }


        [HttpGet]
        public async Task<IActionResult> UnflagComment(int Id)
        {
            //Unflag review
            var comment = await _commentServices.GetCommentById(Id);
            if (comment == null)
            {
                return NotFound();
            }
            comment.Flag = 0;
            if (!await _moderatorServices.UnflagComment(comment))
            {
                return BadRequest();
            }

            return RedirectToAction("IndexAudits");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteComment(int Id)
        {
            //Unflag review
            var comment = await _commentServices.GetCommentById(Id);
            if (comment == null)
            {
                return NotFound();
            }

            if (!await _commentServices.DeleteComment(comment))
            {
                return BadRequest();
            }

            return RedirectToAction("IndexAudits");
        }

    }
}
