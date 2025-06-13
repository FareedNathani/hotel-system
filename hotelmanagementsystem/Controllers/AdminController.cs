using hotelmanagementsystem.Models;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace hotelmanagementsystem.Controllers
{
    public class AdminController : Controller
    {
        private AppDbContext db = new AppDbContext();

        protected string RenderPartialViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }


        public ActionResult Index()
        {
            var user = Session["User"] as User;
            if (user == null || user.Role != "Admin")
                return RedirectToAction("Login", "Account");

            return Content("Admin Controller is working");
        }

        public ActionResult Dashboard()
        {
            var user = Session["User"] as User;
            if (user == null || user.Role != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.TotalRooms = db.Rooms.Count();
            ViewBag.TotalBookings = db.Reservations.Count();
            ViewBag.TotalUsers = db.Users.Count();
            ViewBag.TotalMessages = db.Contacts.Count();

            return View(); // View should be Dashboard.cshtml
        }

        public ActionResult RoomsPartial()
        {
            var rooms = db.Rooms.ToList();
            return PartialView("_ManageRoomsPartial", rooms);
        }

        public ActionResult BookingsPartial()
        {
            var bookings = db.Reservations.OrderByDescending(r => r.CreatedAt).ToList(); // Fix: use Reservations
            return PartialView("_ViewBookingsPartial", bookings);
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult UsersPartial()
        {
            var users = db.Users.ToList();
            return PartialView("_ManageUsersPartial", users);
        }


        public ActionResult EditUserPartial(int id = 0)
        {
            var user = id == 0 ? new User() : db.Users.Find(id);
            return PartialView("EditUserPartial", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUser(User model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    db.Users.Add(model);
                }
                else
                {
                    var existing = db.Users.Find(model.Id);
                    if (existing != null)
                    {
                        existing.FullName = model.FullName;
                        existing.Email = model.Email;
                        existing.Role = model.Role;
                    }
                }

                db.SaveChanges();
                return Json(new { success = true });
            }

            // Validation failed: return form HTML
            var html = RenderPartialViewToString("_UserFormPartial", model);
            return Json(new { success = false, html });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteUser(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "User not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public ActionResult ContactsPartial()
        {
            try
            {
                var messages = db.Contacts.ToList();
                return PartialView("_ContactsPartial", messages); // ✅ Correct name
            }
            catch (Exception ex)
            {
                return Content("Error: " + ex.Message);
            }
        }


    }
}
