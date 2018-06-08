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
        //ConfigModel configModel = new ConfigModel();
        LogModel logModel = new LogModel();
        bool serviceRunning = (new ServiceController("ImageService").Status == ServiceControllerStatus.Running);
        public ActionResult Index()
        {
            ViewBag.ServiceRunning = false;
            if (serviceRunning)
            {
                ViewBag.ServiceRunning = serviceRunning;
                ConfigModel configModel = new ConfigModel();
                ViewBag.NumPhotos = configModel.GetNumPhotos();
            }

            return View();
        }

        public ActionResult Config()
        {
            //bool a = new ServiceController("ImageService").Status == ServiceControllerStatus.Running;
            ViewBag.ServiceRunning = serviceRunning;
            //if (serviceRunning)
            //{
                ViewBag.Message = "Your config page.";
                return View(new ConfigModel());
            //}
            //else
            //{
            //    return View(new ConfigModel());
            //}
        }

        public ActionResult Photos()
        {
            ViewBag.Message = "Your photos page.";

            return View();
        }

        public ActionResult Logs()
        {
            ViewBag.ServiceRunning = serviceRunning;

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
            //ViewBag.Message = "dir";
            ConfigModel configModel = new ConfigModel();
            configModel.RemoveHandler(dir);
            return View(new ConfigModel());
        }
    }
}