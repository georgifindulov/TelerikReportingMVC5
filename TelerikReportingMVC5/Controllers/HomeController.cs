using System;
using System.Web.Mvc;
using TelerikReportingMVC5.ViewModels;

namespace TelerikReportingMVC5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IndexViewModel viewModel = new IndexViewModel
            {
                JoinedSince = new DateTime(2005, 01, 01)
            };

            return View(viewModel);
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