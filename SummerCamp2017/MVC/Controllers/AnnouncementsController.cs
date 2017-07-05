using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestClient;

namespace MVC.Controllers
{
    public class AnnouncementsController : Controller
    {


        public List<AnnouncementListModel> GetAnnouncements()
        {
            RestClient<AnnouncementListModel> rc = new RestClient<AnnouncementListModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            List<AnnouncementListModel> announcementsList = rc.Get();

            return announcementsList;
        }

        public List<Category> GetCategories()
        {
            RestClient<Category> rc = new RestClient<Category>();
            rc.WebServiceUrl = "http://localhost:61144/api/categories/";
            List<Category> categoriesList = rc.Get();

            return categoriesList;
        }

        public AnnouncementDetailsModel GetAnnouncementById(int id)
        {
            RestClient<AnnouncementDetailsModel> rc = new RestClient<AnnouncementDetailsModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            AnnouncementDetailsModel announcement = rc.GetById(id);

            return announcement;
        }


        public bool PostAnnouncement(AnnouncementCreateModel announcement)
        {
            RestClient<AnnouncementCreateModel> rc = new RestClient<AnnouncementCreateModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            bool response = rc.Post(announcement);

            return response;
        }

        public bool PutAnnouncement(int id, AnnouncementEditModel announcement)
        {
            RestClient<AnnouncementEditModel> rc = new RestClient<AnnouncementEditModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            bool response = rc.Put(id, announcement);

            return response;
        }

        public bool DeleteAnnouncement(int id)
        {
            RestClient<AnnouncementDetailsModel> rc = new RestClient<AnnouncementDetailsModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            bool response = rc.Delete(id);

            return response;
        }

        public bool CloseAnnouncement(int id)
        {

            RestClient<AnnouncementDetailsModel> rc = new RestClient<AnnouncementDetailsModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/Announcements/CloseAnnouncement/";
            bool response = rc.Close(id);

            return response;
        }

        public bool ExtendAnnouncement(int id)
        {

            RestClient<AnnouncementDetailsModel> rc = new RestClient<AnnouncementDetailsModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/Announcements/ExtendAnnouncement/";
            bool response = rc.Extend(id);

            return response;
        }

        // GET: Announcements
        public ActionResult Create()
        {
            ViewBag.Categories = GetCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Create(AnnouncementCreateModel announcement)
        {
            PostAnnouncement(announcement);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            AnnouncementDetailsModel announcement = GetAnnouncementById(id);

            return View(announcement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DeleteAnnouncement(id);

            return RedirectToAction("List");
        }

        public ActionResult Details(int id)
        {
            AnnouncementDetailsModel announcement = GetAnnouncementById(id);

            return View(announcement);
        }

        public ActionResult VerifyEmail(int id)
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult VerifyEmail(int id, EmailTextBox txtBox)
        {
            AnnouncementDetailsModel announcement = GetAnnouncementById(id);
            if (announcement.Email == txtBox.Email)
            {
                if (!(announcement.Closed))
                {
                    return RedirectToAction("Edit", new { id = announcement.AnnouncementId });
                }
                else
                {
                    ViewBag.Error = "This announcement is closed! Cannot edit!";
                }
            }
            else
            {
                ViewBag.Error = "Input data is incorrect!";
            }
            return View();
        }

        public ActionResult Close(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Close(int id, EmailTextBox txtBox)
        {
                AnnouncementDetailsModel announcement = GetAnnouncementById(id);
                if (announcement.Email == txtBox.Email)
                {
                    bool response = CloseAnnouncement(id);
                    if (response)
                    {
                        return RedirectToAction("List");
                    }
                    else
                    {
                        ViewBag.Error = "This announcement is already closed!";
                    }
                }

                else
                {
                    ViewBag.Error = "Input data is incorrect!";
                }

            return View();
        }


        public ActionResult Extend(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Extend(int id, EmailTextBox txtBox)
        {
            AnnouncementDetailsModel announcement = GetAnnouncementById(id);
            if (announcement.Email == txtBox.Email)
            {
                bool response = ExtendAnnouncement(id);
                if (response)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    ViewBag.Error = "This announcement is closed! Cannot extend!";
                }
            }

            else
            {
                ViewBag.Error = "Input data is incorrect!";
            }

            return View();

        }
        public ActionResult Edit(int id)
        {
            AnnouncementDetailsModel announcement = GetAnnouncementById(id);
            AnnouncementEditModel newAnnouncement = new AnnouncementEditModel();
            newAnnouncement.AnnouncementId = announcement.AnnouncementId;
            newAnnouncement.Phonenumber = announcement.Phonenumber;
            newAnnouncement.Email = announcement.Email;
            newAnnouncement.Title = announcement.Title;
            newAnnouncement.Description = announcement.Description;
            newAnnouncement.CategoryId = announcement.CategoryId;
            ViewBag.Categories = GetCategories();
            return View(newAnnouncement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnnouncementEditModel announcement)
        {
            PutAnnouncement(announcement.AnnouncementId, announcement);

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            List<AnnouncementListModel> announcementList = GetAnnouncements();

            return View(announcementList);
        }
    }
}