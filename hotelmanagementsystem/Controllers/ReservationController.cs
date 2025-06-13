using hotelmanagementsystem.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace hotelmanagementsystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            if (Session["User"] == null)
            {
                TempData["Error"] = "Please login to make a reservation.";
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // GET: Reservation/BookNow/5
        public ActionResult BookNow(int? roomId)
        {

            if (roomId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = Session["User"] as User;
            if (user == null)
            {
                TempData["Error"] = "Please login to book a room.";
                return RedirectToAction("Login", "Account");
            }

            var room = db.Rooms.Find(roomId);
            if (room == null)
            {
                return HttpNotFound("Room not found.");
            }

            var reservation = new Reservation
            {
                RoomId = room.Id,
                Email = user.Email,
                CreatedAt = DateTime.Now
            };

            ViewBag.Room = room;
            return View(reservation);
        }

        // POST: Reservation/BookNow
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookNow(Reservation reservation)
        {
            var user = Session["User"] as User;
            if (user == null)
            {
                TempData["Error"] = "Please login again.";
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                var room = db.Rooms.Find(reservation.RoomId);
                if (room == null)
                {
                    ModelState.AddModelError("", "Invalid Room selected.");
                    return View(reservation);
                }

                reservation.CreatedAt = DateTime.Now;
                db.Reservations.Add(reservation);
                db.SaveChanges();

                TempData["Success"] = "Your reservation has been placed!";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Room = db.Rooms.Find(reservation.RoomId);
            return View(reservation);
        }

        public ActionResult ViewBookings()
        {
            var reservations = db.Reservations.ToList();
            return PartialView("ViewBookings", reservations);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AllBookings()
        {
            var reservations = db.Reservations.OrderByDescending(r => r.CreatedAt).ToList();
            return View(reservations);
        }

        [HttpPost]
        public ActionResult Create(Reservation reservation)
        {
            if (Session["User"] == null)
            {
                TempData["Error"] = "Please login to make a reservation.";
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                db.Reservations.Add(reservation);
                db.SaveChanges();
                TempData["Success"] = "Reservation successful!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Please fill all fields correctly.";
            return View("Index", reservation);
        }
    }
}
