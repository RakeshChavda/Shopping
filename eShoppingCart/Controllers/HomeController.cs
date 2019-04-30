using eShoppingCart.Models.HomeModelIndex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string search, int? page)
        {
            HomeModelViewIndex model = new HomeModelViewIndex();
            return View(model.CreateModel(search, 4, page)); // Here passing page size 10/4/5
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