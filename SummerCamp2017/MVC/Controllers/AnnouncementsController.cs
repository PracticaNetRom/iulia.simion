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


        public List<Announcement> GetAnnouncements()
        {
            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            List<Announcement> announcementsList = rc.Get();

            return announcementsList;
        }

        public Announcement GetAnnouncementById(int id)
        {
            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            Announcement announcement = rc.GetById(id);

            return announcement;
        }


        public bool PostAnnouncement(Announcement announcement)
        {
            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            bool response = rc.Post(announcement);

            return response;
        }

        public bool PutAnnouncement(int id, Announcement announcement)
        {
            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            bool response = rc.Put(id, announcement);

            return response;
        }

        public bool DeleteAnnouncement(int id)
        {
            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:61144/api/announcements/";
            bool response = rc.Delete(id);

            return response;
        }

        public bool CloseAnnouncement(int id)
        {

            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:61144/api/Announcements/CloseAnnouncement/";
            bool response = rc.Close(id);

            return response;
        }

        public bool ExtendAnnouncement(int id)
        {

            RestClient<Announcement> rc = new RestClient<Announcement>();
            rc.WebServiceUrl = "http://localhost:61144/api/Announcements/ExtendAnnouncement/";
            bool response = rc.Extend(id);

            return response;
        }

        // GET: Announcements
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Announcement announcement)
        {
            PostAnnouncement(announcement);

            return RedirectToAction("List");
        }

        public ActionResult Delete(int id)
        {
            Announcement announcement = GetAnnouncementById(id);

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
            Announcement announcement = GetAnnouncementById(id);

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
            Announcement announcement = GetAnnouncementById(id);
            if (announcement.Email == txtBox.Email)
            {
                return RedirectToAction("Edit", new { id = announcement.AnnouncementId });
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult Close(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Close(int id, EmailTextBox txtBox)
        {
            Announcement announcement = GetAnnouncementById(id);
            if (announcement.Email == txtBox.Email)
            {
                bool response = CloseAnnouncement(id);
                if (response)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            else
            {
                return HttpNotFound();
            }
            
        }


        public ActionResult Extend(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Extend(int id, EmailTextBox txtBox)
        {
            Announcement announcement = GetAnnouncementById(id);
            if (announcement.Email == txtBox.Email)
            {
                bool response = ExtendAnnouncement(id);
                if (response)
                {
                    return RedirectToAction("List");
                }
                else
                {
                    return HttpNotFound();
                }
            }

            else
            {
                return HttpNotFound();
            }

        }
        public ActionResult Edit(int id)
        {
            Announcement announcement = GetAnnouncementById(id);

            return View(announcement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Announcement announcement)
        {
            PutAnnouncement(announcement.AnnouncementId, announcement);

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            List<Announcement> announcementList = GetAnnouncements();

            return View(announcementList);
        }
    }
}