using MVC.Models;
using RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ReviewsController : Controller
    {
        public List<Review> GetReviews()
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:61144/api/reviews/";
            List<Review> reviewsList = new List<Review>();
            reviewsList = rc.Get();

            return reviewsList;
        }

        public Review GetReviewById(int id)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:61144/api/reviews/";
            Review review = new Review();
            review = rc.GetById(id);

            return review;
        }

        public List<Review> GetReviewsPerAnnouncement(int id)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:61144/api/reviews/reviewsperannouncement/";
            List<Review> reviewsList = new List<Review>();
            reviewsList = rc.GetByAnnouncement(id);

            return reviewsList;
        }

        public bool PostReview(Review review)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:61144/api/reviews/";
            bool response = rc.Post(review);

            return response;
        }

        public bool PutReview(int id, Review review)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:61144/api/reviews/";
            bool response = rc.Put(id, review);

            return response;
        }

        public bool DeleteReview(int id)
        {
            RestClient<Review> rc = new RestClient<Review>();
            rc.WebServiceUrl = "http://localhost:61144/api/reviews/";
            bool response = rc.Delete(id);

            return response;
        }

        // GET: Announcements
        public ActionResult Create(int idAnn)
        {
            ViewBag.RatingRange = new int[] {1,2,3,4,5};
            return View();
        }

        [HttpPost]
        public ActionResult Create(int idAnn, Review review)
        {
            review.AnnouncementId = idAnn;
            PostReview(review);
            
            return RedirectToAction("List", new { id=idAnn });
        }

        public ActionResult Delete(int idAnn, int id)
        {
            DeleteReview(id);
            return RedirectToAction("List", new { id=idAnn });
        }

        public ActionResult Details(int id)
        {
            Review review = GetReviewById(id);

            return View(review);
        }

        public ActionResult Edit(int id)
        {
            Review review = GetReviewById(id);

            return View(review);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int idAnn, Review review)
        {
            review.AnnouncementId = idAnn;
            PutReview(review.ReviewId, review);
            
            return RedirectToAction("List", new { id=idAnn });
        }

        public ActionResult List(int id)
        {
            ReviewsPerAnnouncement revPerAnn = new ReviewsPerAnnouncement();
            revPerAnn.CurrentAnnouncementId = id;
            List<Review> reviewsList = GetReviewsPerAnnouncement(id);
            revPerAnn.Reviews = reviewsList;

            if (reviewsList.Count == 0)
            {
                revPerAnn.AverageRating = 0;
            }
            else
            {
                revPerAnn.AverageRating = 0;
                foreach (var review in reviewsList)
                {
                    revPerAnn.AverageRating += Convert.ToDecimal(review.Rating);
                }
                revPerAnn.AverageRating /= reviewsList.Count();
            }

            return View(revPerAnn);
        }
    }
}