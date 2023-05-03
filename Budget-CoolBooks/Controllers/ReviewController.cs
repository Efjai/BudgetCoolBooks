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
            List<Book> books = (List<Book>)await _bookServices.GetAllBooksSorted();
            List<Review> allReviews = new List<Review>();
            List<Comment> allComments = new List<Comment>();
            List<Reply> allReplys = new List<Reply>();

            foreach (var book in books)
            {
                var temp = await _reviewServices.GetReviewByBookID(book.Id);

                if (temp != null)
                {
                    allReviews.Add(temp);
                }
            }

            foreach (var review in allReviews)
            {
                if (review != null)
                {
                    var temp = await _commentServices.GetAllCommentsOfReview(review.Id);
                    foreach (var comment in temp)
                    {
                        allComments.Add(comment);
                    }
                }
            }

            foreach(var comment in allComments)
            {
                var temp = await _commentServices.GetAllReplysOfComments(comment.Id);
                foreach (var reply in temp)
                {
                    allReplys.Add(reply);
                }
            }

            ClaimsPrincipal currentUser = this.User;
            var currentUserID = "";
            try
            {
                currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
            catch
            {
                currentUserID = "NoUserLoggedIn";
            }

            BookcardViewModel viewModel = new BookcardViewModel
            {
                Books = books,
                AllFullReviews = allReviews,
                CommentsList = allComments,
                RepliesList = allReplys,
                CurrentUserId = currentUserID,
            };

            return View(viewModel);
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
        public async Task<IActionResult> Delete(int reviewId, string test, int id)
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

            if (test != null)
            {
                return RedirectToAction("BookDetails", "Book", new { id = id });
            }
            return RedirectToAction("UserReviews", "User");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int reviewId, string test)
        {
            var reviewresult = (List<Review>)await _reviewServices.GetReviewListByID(reviewId);

            var bookResult = (List<Book>)await _bookServices.GetBookListByID(reviewresult[0].Book.Id);

            ReviewcardViewModel viewModel = new ReviewcardViewModel
            {
                Review = reviewresult,
                ReviewBook = bookResult,
                Test = test,
            };

            return View("~/views/review/Create.cshtml", viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string title, string text, double rating, int id, string test, int bookid)
        {
            

            Review updated = await _reviewServices.GetReviewByID(id);

            
            updated.Title = title;
            updated.Text = text;
            updated.Rating = rating;
            updated.Book = await _bookServices.GetBookById(bookid);


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

            if (test != null)
            {
                return RedirectToAction("BookDetails", "Book", new { id = updated.Book.Id });
            }
            else { return RedirectToAction("UserReviews", "User"); }
        }
    }
}
