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








        //REVIEW FORUM!!!!
        public async Task<IActionResult> ReviewForum(int id)
        {
            //Get all books
            var booksResults = await _bookServices.GetAllBooksSorted();
            //Make Lists for rating services
            List<int> RatingPerGradesList = new List<int> { };
            List<int> AverageRatingsList = new List<int> { };
            foreach (var book in booksResults) 
            {
                var ratings = await _reviewServices.GetAllRatingsOfBook(id);
                var RatingPerGrades = RatingPerGrade(ratings);
                var AverageRatings = CalculateAverageRating(ratings);
                for (int i = 0; i < 1; i++)
                {
                    if (ratings == null)
                    {
                        throw new ArgumentException("Review is null");
                    }
                }
            }

            var CheckIfReviewed = await _reviewServices.GetReviewByBookID(id);
            if (CheckIfReviewed == null)
            {
                //Just get book info (no reviews) & a nis not reviewed varible
                var bookcardViewModel2 = new BookcardViewModel()
                {
                    Books = booksResults.ToList(),
                    IsNotReviewed = 1,
                };
                return View("/views/book/bookcard.cshtml", bookcardViewModel2);
            }
            //Get Review IDS
            var reviewIds = await _reviewServices.GetAllIdOfReviews(id);
            //ID OF COMMENTS LIST
            List<int> CommentIDs = new List<int> { };
            //GET IDS OF COMMENTS
            try
            {
                int r = 0;
                foreach (var reviewId in reviewIds)
                {
                    var commentIds = await _commentServices.GetAllIdOfComments(reviewIds[r]);
                    CommentIDs.AddRange(commentIds);
                    r++;
                }
            }
            catch (Exception ex) { }
            var AllFullReviews = await _reviewServices.GetFULLAllRatingsOfBook(id);
            //COMMENT & REPLIES LISTS
            List<Comment> CommentsList = new List<Comment> { };
            List<Reply> RepliesList = new List<Reply> { };
            int c = 0;
            //GET COMMENTS
            try
            {
                foreach (var Id in reviewIds)
                {
                    var Comments = await _commentServices.GetAllCommentsOfReview(reviewIds[c]);
                    CommentsList.AddRange(Comments);
                    c++;
                }
            }
            catch (Exception ex) { }
            c = 0;
            //GET REPLIES
            try
            {
                foreach (var Comment in CommentsList)
                {
                    var Replies = await _commentServices.GetAllReplysOfComments(CommentIDs[c]);
                    RepliesList.AddRange(Replies);
                    c++;
                }
            }
            catch (Exception ex) { }
            //ViewModel
            BookcardViewModel ratingsViewModel = new BookcardViewModel();
            //USER
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
            var bookcardViewModel = new BookcardViewModel()
            {

                AllFullReviews = AllFullReviews.ToList(),
                CommentsList = CommentsList.ToList(),
                RepliesList = RepliesList.ToList(),

                CurrentUserId = currentUserID,
            };
            return View("/views/book/bookcard.cshtml", bookcardViewModel);
        }
        private double CalculateAverageRating(ICollection<double> ratings)
        {
            double totalRating = 0;
            foreach (var rating in ratings)
            {
                totalRating = totalRating + rating;
            }
            double averageRating = totalRating / ratings.Count;
            averageRating = Math.Round(averageRating, 1);
            return averageRating;
        }

        private List<int> RatingPerGrade(ICollection<double> ratings)
        {
            int ones = 0; int twos = 0; int threes = 0; int fours = 0; int fives = 0;
            foreach (var rating in ratings)
            {
                if (rating == 1) { ones++; }
                else if (rating == 2) { twos++; }
                else if (rating == 3) { threes++; }
                else if (rating == 4) { fours++; }
                else if (rating == 5) { fives++; }
            }

            List<int> RatingPerGrades = new List<int>
            {
                fives, fours, threes, twos, ones
            };
            return RatingPerGrades;
        }
    }
}
