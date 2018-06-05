using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceProcess;
using ImageServiceWeb.Models;

namespace ImageServiceWeb.Controllers
{

    public class HomeController : Controller
    {
        ConfigModel configModel = new ConfigModel();

        public ActionResult Index()
        {
            if (ViewBag.ServiceRunning = (new ServiceController("ImageService").Status == ServiceControllerStatus.Running))
            {
                ViewBag.NumPhotos = configModel.GetNumPhotos(); 
            }

            return View();
        }

        public ActionResult Config()
        {
            ViewBag.Message = "Your config page.";

            return View(configModel);
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