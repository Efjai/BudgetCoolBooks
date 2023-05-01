using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.Comments;
using Microsoft.AspNetCore.Mvc;
using Budget_CoolBooks.ViewModels;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using Budget_CoolBooks.Services.UserServices;
using System.Security.Claims;
using Budget_CoolBooks.Services.Genres;
using Budget_CoolBooks.Services.Search;

namespace Budget_CoolBooks.Controllers
{
    public class BookController : Controller
    {
        private readonly BookServices _bookServices;
        private readonly AuthorServices _authorServices;
        private readonly ReviewServices _reviewServices;
        private readonly CommentServices _commentServices;
        private readonly UserServices _userServices;
        private readonly GenreServices _genreServices;

        public BookController(BookServices bookServices, AuthorServices authorServices, ReviewServices reviewServices, CommentServices commentServices, GenreServices genreServices)
        {
            _bookServices = bookServices;
            _authorServices = authorServices;
            _reviewServices = reviewServices;
            _commentServices = commentServices;
            _genreServices = genreServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var result = await _bookServices.GetAllBooksSorted();
            //if (result == null)
            //{
            //    return NotFound();
            //}
            //ViewBag.bookListSorted = result;
            //return View(ViewBag.bookListSorted);
            
            var result = (List<Book>)await _bookServices.GetAllBooksSorted();

            
            // TEST RATING -- WORKING, NOT PRETTY BUT WORKING.
            Dictionary<string, int> ratingPerBook = new Dictionary<string, int>();


            foreach (var book in result)
            {
                int rating = _reviewServices.GetAverageRating(book.Id);
                ratingPerBook.Add(book.Title, rating);
            }
            //

            var searchViewModel = new SearchViewModel()
            {
                Books = result.ToList(),
                SearchActive = true,
                SearchAuthors = (List<Author>)await _authorServices.GetAuthors(),
                SearchGenres = (List<Genre>)await _genreServices.GetGenres(),
                RatingPerBook = ratingPerBook,

            };

            return View("/views/home/search.cshtml", searchViewModel);
        }
        [HttpPost]
        [AcceptVerbs("GET", "POST")]
        public async Task<IActionResult> BookDetails(int id)
        {
            //Get book by bookid
            var bookResult = await _bookServices.GetBookListByID(id);
            if (bookResult == null)
            {
                return NotFound();
            }
            //Get author/s by bookid
            var authorsResult = await _authorServices.GetAuthorsOfBook(id);
            if (authorsResult == null)
            {
                return NotFound();
            }
            var ratings = await _reviewServices.GetAllRatingsOfBook(id);

            for (int i = 0; i < 1; i++)
            {
                if (ratings == null)
                {
                    throw new ArgumentException("Review is null");
                }
            }


            var CheckIfReviewed = await _reviewServices.GetReviewByBookID(id);
            if (CheckIfReviewed == null)
            {
                //Just get book info (no reviews) & a nis not reviewed varible
                var bookcardViewModel2 = new BookcardViewModel()
                {
                    Books = bookResult.ToList(),

                    Authors = authorsResult.ToList(),

                    IsNotReviewed = 1,
                };
                return View("/views/book/bookcard.cshtml", bookcardViewModel2);
            }
            //Get Review IDS
            var reviewIds = await _reviewServices.GetAllIdOfReviews(id);

            List<int> CommentIDs = new List<int> { };
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
            catch
            {

            }

            var AllFullReviews = await _reviewServices.GetFULLAllRatingsOfBook(id);

            var RatingPerGrades = RatingPerGrade(ratings);
            var AverageRatings = CalculateAverageRating(ratings);

            List<Comment> CommentsList = new List<Comment> { };
            List<Reply> RepliesList = new List<Reply> { };

            int c = 0;
            try
            {
                foreach (var Id in reviewIds)
                {
                    var Comments = await _commentServices.GetAllCommentsOfReview(reviewIds[c]);
                    CommentsList.AddRange(Comments);
                    c++;
                }
            }
            catch (Exception ex)
            {

            }
            c = 0;
            try
            {
                foreach (var Comment in CommentsList)
                {
                    var Replies = await _commentServices.GetAllReplysOfComments(CommentIDs[c]);
                    RepliesList.AddRange(Replies);
                    c++;
                }
            }
            catch
            {

            }

            BookcardViewModel ratingsViewModel = new BookcardViewModel();

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
                Books = bookResult.ToList(),

                Authors = authorsResult.ToList(),

                IsNotReviewed = 0,

                RatingsByValue = RatingPerGrades,
                AverageRating = AverageRatings,

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