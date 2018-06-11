using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using ImageServiceWeb.Communication;
using ImageServiceWeb.Infrastructure;

namespace ImageServiceWeb.Models
{
    public class PhotoModel
    {
        public string outputDir;
        private Object obj;
        private List<PhotoInfo> photoList;
        public List<PhotoInfo> PhotoList
        {
            get { return photoList; }
            set { }
        }

        /// <summary>
        /// Our PhotoModel constructor
        /// </summary>
        /// <param name="configModel">Homes config model so we can get the output folder</param>
        /// <param name="obj">Object for locking</param>
        public PhotoModel(ConfigModel configModel,Object obj)
        {
            outputDir = configModel.Output;
            photoList = new List<PhotoInfo>();
            this.obj = obj;
        }

        /// <summary>
        /// Update the photoList according to our output folder
        /// </summary>
        public void GetPhotos()
        {
            // Clearing our list each refresh
            photoList.Clear();
            // Making sure we only read image files
            string[] extensions = { ".jpg", ".png", ".gif", ".bmp" };
            // Searching our thumbnail folder
            string thumbnailDir = outputDir + "\\thumbnails";
            string[] fileList = Directory.GetFiles(thumbnailDir, "*.*", SearchOption.AllDirectories);
            // Going over all our files
            foreach (string path in fileList)
            {
                // If the file is an image
                if (extensions.Contains(Path.GetExtension(path.ToLower())))
                {
                    // We'll extract the information from it and add it to the list
                    string rPath = "\\Images" + path.Substring(outputDir.Length);
                    string name = Path.GetFileNameWithoutExtension(path);
                    string year = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(path)));
                    string month = Path.GetFileName(Path.GetDirectoryName(path));
                    PhotoList.Add(new PhotoInfo(rPath, name, year, month));
                }
            }

        }

        public void DeletePhoto(string name)
        {
            string str = name.Replace("\\Images", "");
            string pathThumb = outputDir + str;
            File.Delete(pathThumb);
            string pathOrigin = pathThumb.Replace("\\thumbnails", "");
            File.Delete(pathOrigin);
            try
            {

            Monitor.Pulse(obj);
            }
            catch {
            }
        }
    }
}