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
        static ConfigModel configModel = new ConfigModel();
        static LogModel logModel = new LogModel();
        static StudentsModel studentsModel = new StudentsModel();
        bool serviceRunning = (new ServiceController("ImageService").Status == ServiceControllerStatus.Running);

        public ActionResult Index()
        {
            ViewBag.ServiceRunning = serviceRunning;
            if (serviceRunning)
            {
                ViewBag.NumPhotos = configModel.GetNumPhotos();
            }
            studentsModel.getStudents();
            return View(studentsModel);
        }

        public ActionResult Config()
        {
            ViewBag.ServiceRunning = serviceRunning;
            ViewBag.Message = "Your config page.";
            configModel.Handlers.Clear();
            return View(configModel);
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
            logModel.GetLog();
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

        public ActionResult RemoveDir(string dir)
        {
            configModel.RemoveHandler(dir);
            return View(configModel);
        }
    }
}