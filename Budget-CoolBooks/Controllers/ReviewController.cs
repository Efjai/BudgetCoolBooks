using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Comments;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace Budget_CoolBooks.Controllers
{
    [Authorize]
    public class ReviewController : Controller
    {
        private readonly ReviewServices _reviewServices;
        private readonly BookServices _bookServices;
        private readonly UserServices _userServices;
        private readonly AuthorServices _authorServices;
        private readonly CommentServices _commentServices;

        public ReviewController(ReviewServices reviewServices, BookServices bookServices, UserServices userServices, AuthorServices authorServices, CommentServices commentServices)
        {
            _reviewServices = reviewServices;
            _bookServices = bookServices;
            _userServices = userServices;
            _authorServices = authorServices;
            _commentServices = commentServices;
        }
       
        public async Task<IActionResult> Index(int id)
        {

            return View();
        }

        //ADD BOOK - GET
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            ReviewcardViewModel viewModel = new ReviewcardViewModel 
            {
                ReviewBook = (List<Book>)await _bookServices.GetBookListByID(id),
                ReviewAuthor = (List<Author>)await _authorServices.GetAuthorsOfBook(id),
            };
           

            //var book = await _bookServices.GetBookById(id);
            
            //viewModel.ReviewBook.Add(book);

            return View("~/views/review/Create.cshtml", viewModel);
        }

        // ADD BOOK - POST
        [HttpPost]
        public async Task<IActionResult> Create(string title, string text, double rating, int id)
        {
            // Get Book object to review !!FIX BOOKID FROM DETAILS PAGE!!
            var book = await _bookServices.GetBookById(id);
            if (book == null)
            {
                NotFound();
            }
            
            // Create a Review object and pass in user input.
            Review review = new Review()
            {
                Title = title,
                Text = text,
                Rating = rating,
                Created = DateTime.Now,
                IsDeleted = false,
                Like = 0,
                Dislike = 0,
                Flag = 0,
                
            };

            // Adds correct book to review.
            review.Book = book;
        

            // Validate the id for user.
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Could not find user");
                return StatusCode(500, ModelState);
            }


            if (!await _reviewServices.CreateReview(review, currentUserID))
            {
                return BadRequest();
            }

            return RedirectToAction("BookDetails", "Book", new { id = id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int reviewId)
        {
            var reviewToDelete = await _reviewServices.GetReviewByID(reviewId);
            if (reviewToDelete == null)
            {
                return NotFound();
            }

            //// CASCADE DELETE COMMENTS AND REPLIES WHEN REVIEWS ISDELETED?

            //var commentsToDelete = await _commentServices.GetAllCommentsOfReview(reviewId);
            //if (commentsToDelete == null) { return BadRequest(); }


            ///////// FUL LÖSNING FÖR ATT TA BORT REPLIES OCH COMMENTS KOPPLAT TILL REVIEW.
            //List<Reply> repliesToDelete = new List<Reply>();

            //foreach (var comment in commentsToDelete)
            //{
            //    repliesToDelete = await _commentServices.GetAllReplysOfComments(comment.Id);
            //    if (!repliesToDelete.Any()) { return BadRequest(); }
            //}

            //foreach (var reply in repliesToDelete)
            //{
            //    if (!await _commentServices.DeleteReply(reply))
            //    {
            //        return NotFound();
            //    }
            //}

            //foreach (var comment in commentsToDelete)
            //{
            //    if (!await _commentServices.DeleteComment(comment))
            //    {
            //        return NotFound();
            //    }
            //}
            ///////////////////////////


            if (!await _reviewServices.DeleteReview(reviewToDelete))
            {
                return NotFound();
            }
            
            return RedirectToAction("UserReviews", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int reviewId)
        {
            var reviewresult = (List<Review>)await _reviewServices.GetReviewListByID(reviewId);

            var bookResult = (List<Book>)await _bookServices.GetBookListByID(reviewresult[0].Book.Id);

            ReviewcardViewModel viewModel = new ReviewcardViewModel
            {
                Review = reviewresult,
                ReviewBook = bookResult,
            };

            return View("~/views/review/Create.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string title, string text, double rating, int id)
        {
            var test = id; 

            Review updated = await _reviewServices.GetReviewByID(id);

            
            updated.Title = title;
            updated.Text = text;
            updated.Rating = rating;


            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (currentUser == null)
            {
                ModelState.AddModelError("", "Could not find user");
                return StatusCode(500, ModelState);
            }


            if (!await _reviewServices.UpdateReview(updated, currentUserID))
            {
                return BadRequest();
            }

            return RedirectToAction("UserReviews","User");
        }
    }
}
