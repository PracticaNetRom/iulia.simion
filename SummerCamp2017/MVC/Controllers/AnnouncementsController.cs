using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestClient;
using System.Net.Http;

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

        public HttpResponseMessage CloseAnnouncement(int id, string email)
        {

            RestClient<AnnouncementDetailsModel> rc = new RestClient<AnnouncementDetailsModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/Announcements/CloseAnnouncement/";
            HttpResponseMessage response = rc.Close(id,email);

            return response;
        }

        public HttpResponseMessage ExtendAnnouncement(int id, string email)
        {

            RestClient<AnnouncementDetailsModel> rc = new RestClient<AnnouncementDetailsModel>();
            rc.WebServiceUrl = "http://localhost:61144/api/Announcements/ExtendAnnouncement/";
            HttpResponseMessage response = rc.Extend(id,email);

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
                //AnnouncementDetailsModel announcement = GetAnnouncementById(id);
                //if (announcement.Email == txtBox.Email)
                //{
                HttpResponseMessage response = CloseAnnouncement(id, txtBox.Email);
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return RedirectToAction("List");
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    {
                        ViewBag.Error = "Input data is incorrect!";
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        ViewBag.Error = "This announcement is already closed!";
                    }
                //}

                

            return View();
        }


        public ActionResult Extend(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Extend(int id, EmailTextBox txtBox)
        {
            //ModelState.AddModelError(txtBox, "Enter email!");

            HttpResponseMessage response = ExtendAnnouncement(id, txtBox.Email);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                    return RedirectToAction("List");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                ViewBag.Error = "Input data is incorrect!";
                //ModelState.AddModelError( txtBox.Email, "Input data is incorrect!");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                    ViewBag.Error = "This announcement is closed! Cannot extend!";
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