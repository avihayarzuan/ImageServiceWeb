using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ServiceProcess;
using ImageServiceWeb.Models;
using System.Threading;
using ImageServiceWeb.Infrastructure;
using System.IO;

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

        /// <summary>
        /// Index the main view opened by the app
        /// </summary>
        /// <returns>View</returns>
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

        /// <summary>
        /// Configuration View
        /// </summary>
        /// <returns></returns>
        public ActionResult Config()
        {
            ViewBag.ServiceRunning = serviceRunning;
            ViewBag.Message = "Your config page.";
            configModel.GetConfig();
            return View(configModel);
        }

        /// <summary>
        /// Photose View
        /// </summary>
        /// <returns></returns>
        public ActionResult Photos()
        {
            ViewBag.Message = "Your photos page.";
            photoModel.GetPhotos();
            return View(photoModel);
        }

        /// <summary>
        /// Logs View
        /// </summary>
        /// <returns></returns>
        public ActionResult Logs()
        {
            ViewBag.ServiceRunning = serviceRunning;

            ViewBag.Message = "Your logs page.";
            logModel.GetLog();
            return View(logModel);
        }

        /// <summary>
        /// Confirmation view of deletion directory
        /// </summary>
        /// <param name="dir">The directory path</param>
        /// <returns></returns>
        public ActionResult DeleteConfirm(string dir)
        {
            if (dir != null)
            {
                ViewBag.MessageOrigin = dir;
                ViewBag.MessageSlashed = dir.Replace("\\", "\\\\");
            }
            return View();
        }

        /// <summary>
        /// Removes the specified Directory handler.
        /// </summary>
        /// <param name="dir">The dir.</param>
        /// <returns></returns>
        public ActionResult Remove(string dir)
        {
            configModel.RemoveHandler(dir);
            Monitor.Wait(obj);
            return View(configModel);
        }

        /// <summary>
        /// Removes the photo.
        /// </summary>
        /// <param name="photo">The photo path</param>
        /// <returns></returns>
        public ActionResult RemovePhoto(string photo)
        {
            photoModel.DeletePhoto(photo);
            Monitor.Wait(obj);
            photoModel.GetPhotos();
            return View(photoModel);
        }

        /// <summary>
        /// Confirm window to delete specified photo
        /// </summary>
        /// <param name="photo">The photo path</param>
        /// <returns></returns>
        public ActionResult DeletePhotoConfirm(string photo)
        {
            ViewBag.PhotoNameOrigin = photo;
            ViewBag.PhotoName = Path.GetFileNameWithoutExtension(photo);
            ViewBag.PhotoNameSlashed = photo.Replace("\\", "\\\\");

            return View();
        }

        /// <summary>
        /// View specified photo
        /// </summary>
        /// <param name="path">The path of the photo</param>
        /// <param name="year">The year of the photo</param>
        /// <param name="month">The month of the photo</param>
        /// <param name="name">The name of the photo</param>
        /// <returns></returns>
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