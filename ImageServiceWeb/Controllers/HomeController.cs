using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceProcess;


namespace ImageServiceWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            bool isRunning = (new ServiceController("ImageService").Status == ServiceControllerStatus.Running);

            if (isRunning)
            {
                ViewBag.Message = "Image service is running";
            } 
            else
            {
                ViewBag.Message = "Image service is closed";
            }

            return View();
        }

        public ActionResult Config()
        {
            ViewBag.Message = "Your config page.";

            return View();
        }

        public ActionResult Photos()
        {
            ViewBag.Message = "Your photos page.";

            return View();
        }

        public ActionResult Logs()
        {
            ViewBag.Message = "Your logs page.";

            return View();
        }
    }
}