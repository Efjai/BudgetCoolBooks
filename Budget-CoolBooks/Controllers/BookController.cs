using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Books;
using Budget_CoolBooks.Services.Authors;
using Budget_CoolBooks.Services.Reviews;
using Microsoft.AspNetCore.Mvc;
using Budget_CoolBooks.ViewModels;
using static System.Net.Mime.MediaTypeNames;

namespace Budget_CoolBooks.Controllers
{
    public class BookController : Controller
    {
        private readonly BookServices _bookServices;
        private readonly AuthorServices _authorServices;
        private readonly ReviewServices _reviewServices;

        public BookController(BookServices bookServices, AuthorServices authorServices, ReviewServices reviewServices)
        {
            _bookServices = bookServices;
            _authorServices = authorServices;
            _reviewServices = reviewServices;
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
        public async Task<IActionResult> BookDetails(int id)
        {
            var bookResult = await _bookServices.GetBookById(id);
            if (bookResult == null)
            {
                return NotFound();
            }
            var authorsResult = await _authorServices.GetAuthorsOfBook(id);
            if (authorsResult == null)
            {
                return NotFound();
            }
            var reviewResults = await _reviewServices.GetReviewByID(3);
            if (reviewResults == null)
            {
                return NotFound();
            }
            BookcardViewModel ratingsViewModel = new BookcardViewModel();

            var ratings = await _reviewServices.GetAllRatingsOfBook(3);
            if (ratings == null)
            {
                return NotFound();
            }

            var bookcardViewModel = new BookcardViewModel()
            {
                BookId = bookResult.Id,
                BookTitle = bookResult.Title,
                BookDescription = bookResult.Description,
                ImgPath = bookResult.Imagepath,

                Authors = authorsResult.ToList(),

                ReviewUser = reviewResults.User,
                ReviewCreated = reviewResults.Created,
                ReviewRating = reviewResults.Rating,
                ReviewTitle = reviewResults.Title,
                ReviewText = reviewResults.Text,
                Like = reviewResults.Like,
                Dislike = reviewResults.Dislike,
                Flag = reviewResults.Flag,
            };
            bookcardViewModel.RatingsByValue = RatingPerGrade(ratings);
            bookcardViewModel.AverageRating = CalculateAverageRating(ratings);

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

            List<int> nrOfRatingsByRatingInteger = new List<int>();
            nrOfRatingsByRatingInteger.Add(ones);
            nrOfRatingsByRatingInteger.Add(twos);
            nrOfRatingsByRatingInteger.Add(threes);
            nrOfRatingsByRatingInteger.Add(fours);
            nrOfRatingsByRatingInteger.Add(fives);

            return nrOfRatingsByRatingInteger;
        }
    }
}
