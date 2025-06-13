using System.Linq;
using System.Web.Mvc;
using hotelmanagementsystem.Models;

namespace HotelManagementSystem.Controllers
{
    public class BlogController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Blog
        public ActionResult Index()
        {
            var blogs = db.Blogs.OrderByDescending(b => b.CreatedAt).ToList();
            return View(blogs); // Views/Blog/Index.cshtml
        }

        // GET: Blog/Details/5
        public ActionResult Details(int? id)
        {

            var blog = db.Blogs.Find(id);
            return View(blog); // Views/Blog/Details.cshtml
        }
    }
}