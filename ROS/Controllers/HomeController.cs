using ROS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ROS.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(int? id)
        {
            var products = db.Recipes;
            var categories = from s in db.Recipes group s by s.Category;
            ViewBag.Categories = categories;
            if (id == null)
            {
                return View(products.ToList());
            }
            else
            {
                return View(products.Where(c => c.CategoryId == id).ToList());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}