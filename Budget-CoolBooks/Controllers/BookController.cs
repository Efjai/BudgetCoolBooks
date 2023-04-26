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

namespace Budget_CoolBooks.Controllers
{
    public class BookController : Controller
    {
        private readonly BookServices _bookServices;
        private readonly AuthorServices _authorServices;
        private readonly ReviewServices _reviewServices;
        private readonly CommentServices _commentServices;
        private readonly UserServices _userServices;

        public BookController(BookServices bookServices, AuthorServices authorServices, ReviewServices reviewServices, CommentServices commentServices)
        {
            _bookServices = bookServices;
            _authorServices = authorServices;
            _reviewServices = reviewServices;
            _commentServices = commentServices;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _bookServices.GetAllBooksSorted();
            if (result == null)
            {
                return NotFound();
            }
            ViewBag.bookListSorted = result;
            return View(ViewBag.bookListSorted);
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
                var reviewIds = await _reviewServices.GetAllIdOfReviews(id);
                    var commentIds = await _commentServices.GetAllIdOfComments(id);

                    var AllFullReviews = await _reviewServices.GetFULLAllRatingsOfBook(id);

                    var RatingPerGrades = RatingPerGrade(ratings);
                    var AverageRatings = CalculateAverageRating(ratings);

                

                int c = 0;
                    List<Reply> GetAllReplysOfComments = new List<Reply> { };
                    List<Comment> GetAllCommentsOfReplys = new List<Comment> { };
                    foreach (var Id in reviewIds)
                    {
                        var GetAllCommentsOfRatings = await _commentServices.GetAllCommentsOfReview(reviewIds[c]);
                        GetAllCommentsOfReplys.AddRange(GetAllCommentsOfRatings);
                        try
                        {
                            
                            var GetAllReplysOfComment = await _commentServices.GetAllReplysOfComments(GetAllCommentsOfReplys[c].Id);
                            GetAllReplysOfComments.AddRange(GetAllReplysOfComment);
                        }
                        catch 
                        {
                            break;
                        }
                        c++;
                    }
                    ClaimsPrincipal currentUser = this.User;
                    var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                    if (currentUserID == null)
                    {
                        return NotFound();
                    }

                    BookcardViewModel ratingsViewModel = new BookcardViewModel();

                    var bookcardViewModel = new BookcardViewModel()
                    {
                        Books = bookResult.ToList(),

                        Authors = authorsResult.ToList(),

                        IsNotReviewed = 0,

                        RatingsByValue = RatingPerGrades,
                        AverageRating = AverageRatings,

                        AllFullReviews = AllFullReviews.ToList(),
                        CommentsToRatings = GetAllCommentsOfReplys.ToList(),
                        AllReplysOfComments = GetAllReplysOfComments.ToList(),
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