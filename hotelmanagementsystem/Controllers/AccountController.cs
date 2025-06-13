using hotelmanagementsystem.Models;
using System.Linq;
using System.Web.Mvc;

namespace hotelmanagementsystem.Controllers
{
    public class AccountController : Controller
    {
        private AppDbContext db = new AppDbContext();

        public ActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User model)
        {
            if (ModelState.IsValid)
            {
                // Check if user already exists
                var existing = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existing != null)
                {
                    ViewBag.Error = "Email already registered.";
                    return View(model);
                }

                db.Users.Add(model);
                db.SaveChanges();
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public ActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user != null)
            {
                Session["User"] = user;

                var identity = new System.Security.Principal.GenericIdentity(user.Email);
                var principal = new System.Security.Principal.GenericPrincipal(identity, new[] { user.Role });
                System.Web.HttpContext.Current.User = principal;
                System.Threading.Thread.CurrentPrincipal = principal;

                if (user.Role == "Admin")
                    return RedirectToAction("Dashboard", "Admin");
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid credentials";
            return View();
        }




        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}