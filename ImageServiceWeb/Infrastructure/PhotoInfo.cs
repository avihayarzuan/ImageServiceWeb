using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Infrastructure
{
    public class PhotoInfo
    {
        public string path;
        public string name;
        public string year;
        public string month;

        public PhotoInfo(string path, string name, string year, string month)
        {
            this.path = path;
            this.name = name;
            this.year = year;
            this.month = month;
        }
    }
}