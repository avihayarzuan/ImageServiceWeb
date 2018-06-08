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

        private Client client;
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
            getPhotos();
        }

        private void getPhotos()
        {
            string thumbnailDir = outputDir + "\thumbnails";
            string[] fileList = Directory.GetFiles(thumbnailDir, "*" , SearchOption.AllDirectories);
            foreach(string path in fileList)
            {
                string photoPath = path;
                string name = Path.GetFileNameWithoutExtension(path);
                string year = Path.GetFileName(Path.GetDirectoryName(path));
                string month = Path.GetFileName(path);
                PhotoList.Add(new PhotoInfo(path, name, year, month));
            }

        }



    }
}