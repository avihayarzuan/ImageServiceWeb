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

        public PhotoModel(ConfigModel configModel,Object obj)
        {
            outputDir = configModel.Output;
            photoList = new List<PhotoInfo>();
            this.obj = obj;
        }

        public void GetPhotos()
        {
            photoList.Clear();
            string[] extensions = { ".jpg", ".png", ".gif", ".bmp" };
            string thumbnailDir = outputDir + "\\thumbnails";
            string[] fileList = Directory.GetFiles(thumbnailDir, "*.*", SearchOption.AllDirectories);
            foreach (string path in fileList)
            {
                if (extensions.Contains(Path.GetExtension(path.ToLower())))
                {
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