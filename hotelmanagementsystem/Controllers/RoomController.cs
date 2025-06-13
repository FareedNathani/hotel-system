using hotelmanagementsystem.Models;
using System.Linq;
using System.Web.Mvc;

namespace hotelmanagementsystem.Controllers
{
    public class RoomController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // Loads full room list view (optional)
        public ActionResult Index()
        {
            var rooms = db.Rooms.ToList();
            return View(rooms);
        }

        public ActionResult Rooms()
        {
            var rooms = db.Rooms.ToList();
            return View(rooms);
        }

        // ✅ Loads partial view for admin dashboard
        public ActionResult RoomsPartial()
        {
            var rooms = db.Rooms.ToList();
            return PartialView("~/Views/Admin/_ManageRoomsPartial.cshtml", rooms);
        }

        // ✅ Add Room from Dashboard (AJAX)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateRoomFromDashboard(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
            }

            var rooms = db.Rooms.ToList();
            return PartialView("~/Views/Admin/_ManageRoomsPartial.cshtml", rooms);
        }

        // ✅ Load edit form partial
        public ActionResult EditRoomPartial(int id)
        {
            var room = db.Rooms.Find(id);
            if (room == null)
                return HttpNotFound();

            return PartialView("~/Views/Admin/_EditRoomFormPartial.cshtml", room);
        }

        // ✅ Save edited room from dashboard
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditRoomFromDashboard(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }

            var rooms = db.Rooms.ToList();
            return PartialView("~/Views/Admin/_ManageRoomsPartial.cshtml", rooms);
        }

        // ✅ Delete room from dashboard
        [HttpPost]
        public ActionResult DeleteRoomFromDashboard(int id)
        {
            var room = db.Rooms.Find(id);
            if (room != null)
            {
                db.Rooms.Remove(room);
                db.SaveChanges();
            }

            var rooms = db.Rooms.ToList();
            return PartialView("~/Views/Admin/_ManageRoomsPartial.cshtml", rooms);
        }

        // ✅ (Optional) Standard Create View
        public ActionResult Create()
        {
            var user = Session["User"] as User;
            if (user == null || user.Role != "Admin")
                return RedirectToAction("Login", "Account");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // ✅ (Optional) Full-page edit
        public ActionResult Edit(int id)
        {
            var room = db.Rooms.Find(id);
            if (room == null)
                return HttpNotFound();

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(room);
        }

        // ✅ (Optional) Full-page delete
        public ActionResult Delete(int id)
        {
            var room = db.Rooms.Find(id);
            if (room == null)
                return HttpNotFound();

            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var room = db.Rooms.Find(id);
            if (room != null)
            {
                db.Rooms.Remove(room);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
