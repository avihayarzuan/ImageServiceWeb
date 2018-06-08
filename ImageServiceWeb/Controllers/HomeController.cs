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
        ConfigModel configModel;
        LogModel logModel = new LogModel();

        public ActionResult Index()
        {
            if (ViewBag.ServiceRunning = (new ServiceController("ImageService").Status == ServiceControllerStatus.Running))
            {
                configModel = new ConfigModel();
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

            return View(logModel);
        }

        public ActionResult DeleteConfirm(string dir)
        {
            if (dir != null)
            {
                ViewBag.MessageOrigin = dir;
                ViewBag.MessageSlashed = dir.Replace("\\", "\\\\");
            }
            return View();
        }

        public ActionResult Remove(string dir)
        {
            ConfigModel configModel = new ConfigModel();
            configModel.RemoveHandler(dir);
            return View(new ConfigModel());
        }
    }
}