using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using ImageServiceWeb.Communication;
using ImageServiceWeb.Infrastructure;

namespace ImageServiceWeb.Models
{
    public class PhotoModel
    {

        public string outputDir;
        private List<PhotoInfo> photoList;
        public List<PhotoInfo> PhotoList
        {
            get { return photoList; }
            set { }
        }

        public PhotoModel(ConfigModel configModel)
        {
            outputDir = configModel.Output;
            photoList = new List<PhotoInfo>();
        }

        public void getPhotos()
        {
            photoList.Clear();
            string[] extensions = { ".jpg", ".png", ".gif", ".bmp" };
            string thumbnailDir = outputDir + "\\thumbnails";
            string[] fileList = Directory.GetFiles(thumbnailDir, "*.*", SearchOption.AllDirectories);
            foreach (string path in fileList)
            {
                if (extensions.Contains(Path.GetExtension(path.ToLower())))
                {
                    string name = Path.GetFileNameWithoutExtension(path);
                    string year = Path.GetFileName(Path.GetDirectoryName(Path.GetDirectoryName(path)));
                    string month = Path.GetFileName(Path.GetDirectoryName(path));
                    PhotoList.Add(new PhotoInfo(path, name, year, month));
                }
            }

        }



    }
}