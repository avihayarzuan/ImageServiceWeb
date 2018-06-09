using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceProcess;
using ImageServiceWeb.Models;
using System.Threading;
using ImageServiceWeb.Infrastructure;

namespace ImageServiceWeb.Controllers
{

    public class HomeController : Controller
    {
        static Object obj = new object();
        static ConfigModel configModel = new ConfigModel(obj);
        static LogModel logModel = new LogModel();
        static PhotoModel photoModel = new PhotoModel(configModel,obj);
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
            configModel.GetConfig();
            return View(configModel);
        }

        public ActionResult Photos()
        {
            ViewBag.Message = "Your photos page.";
            photoModel.GetPhotos();
            return View(photoModel);
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

        public ActionResult Remove(string dir)
        {
            configModel.RemoveHandler(dir);
            Monitor.Wait(obj);
            return View(configModel);
        }

        public ActionResult RemovePhoto(string photo)
        {
            photoModel.DeletePhoto(photo);
            Monitor.Wait(obj);
            photoModel.GetPhotos();
            return View(photoModel);
        }

        public ActionResult DeletePhotoConfirm(string photo)
        {
            ViewBag.PhotoNameOrigin = photo;
            ViewBag.PhotoNameSlashed = photo.Replace("\\", "\\\\");

            return View();
        }

        public ActionResult PhotosViewer(string path, string year, string month, string name)
        {
            ViewBag.path = path;
            ViewBag.pathOrigin = path.Replace("\\thumbnails", "");
            ViewBag.year = year;
            ViewBag.month = month;
            ViewBag.name = name;
            return View();
        }
    }
}