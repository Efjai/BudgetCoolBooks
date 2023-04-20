using Budget_CoolBooks.Models;
using Budget_CoolBooks.Services.Reviews;
using Budget_CoolBooks.Services.UserServices;
using Budget_CoolBooks.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Budget_CoolBooks.Controllers.Admin
{
    [Authorize(Roles = "Admin, Moderator")]
    public class AdminReviewController : Controller
    {
        private readonly ReviewServices _reviewServices;
        private readonly UserServices _userServices;

        //public AdminReviewController(ReviewServices reviewServices, UserServices userServices)
        //{
        //    _reviewServices = reviewServices;
        //    _userServices = userServices;
        //}


        //[HttpGet]
        //public async Task<IActionResult> Index()
        //{
        //    // Gets all flagged reviews
        //    var reviews = await _reviewServices.GetFlaggedReviews();

        //    var adminReviewsViewModel = new AdminModerationViewModel();
        //    adminReviewsViewModel.Reviews = reviews.ToList();

        //    // Gets all users associated to reviews

        //    //Viewmodel for all data
        //    return View("~/views/admin/review/index.cshtml", adminReviewsViewModel);

        //}


        //// View and moderate REVIEWS
        //[HttpGet]
        //public async Task<IActionResult> AuditReview()
        //{
        //    // Gets all flagged reviews
        //    var reviews = await _reviewServices.GetFlaggedReviews();

        //    var adminReviewsViewModel = new AdminModerationViewModel();
        //    adminReviewsViewModel.Reviews = reviews.ToList();

        //    //Viewmodel for all data
        //    return View("~/views/admin/review/index.cshtml", adminReviewsViewModel);
        //}

        ////        [HttpPost]
        ////        public async Task<IActionResult> AuditReview(string userName)
        ////        {

        ////        }


        //// View and moderate COMMENTS
        //[HttpGet]
        //public async Task<IActionResult> AuditComment()
        //{
        //    // Gets all flagged reviews
        //    var reviews = await _reviewServices.GetFlaggedComments();

        //    var adminModerationViewModel = new AdminModerationViewModel();
        //    adminReviewsViewModel.Reviews = reviews.ToList();

        //    //Viewmodel for all data
        //    return View("~/views/admin/review/index.cshtml", adminModerationViewModel);
        //}

        ////        [HttpPost]
        ////        public async Task<IActionResult> AuditComment()
        ////        {

        ////        }




        //// DELETE reviews
        //[HttpPost]
        //public async Task<IActionResult> DeleteReview(int reviewId)
        //{
        //    var reviewToDelete = await _reviewServices.GetReviewByID(reviewId);
        //    if (reviewToDelete == null)
        //    {
        //        return NotFound();
        //    }
        //    if (!await _reviewServices.DeleteReview(reviewToDelete))
        //    {
        //        return NotFound();
        //    }
        //    return View("AdminReviews");
        //}
    }
}

