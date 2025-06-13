using hotelmanagementsystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hotelmanagementsystem.Controllers
{
    public class HomeController : Controller
    {

        private AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            var rooms = db.Rooms.ToList();
            return View(rooms); 
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                Contact contact = new Contact
                {
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    Message = model.Message
                };

                db.Contacts.Add(contact);
                db.SaveChanges();

                ViewBag.Error = "Thank you for contacting us!";
                ModelState.Clear();
            }
            else
            {
                ViewBag.Error = "Please fill all fields correctly.";
            }

            return View();
        }



    }
}
