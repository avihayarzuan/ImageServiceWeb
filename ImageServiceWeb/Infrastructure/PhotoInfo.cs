using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Infrastructure
{
    // Simple class for holding our photos information
    public class PhotoInfo
    {
        public string path;
        public string name;
        public string year;
        public string month;

        /// <summary>
        /// PhotoInfo constructor
        /// </summary>
        /// <param name="path">the photos full path</param>
        /// <param name="name">photo name without extension</param>
        /// <param name="year">the photo year</param>
        /// <param name="month">the photo month</param>
        public PhotoInfo(string path, string name, string year, string month)
        {
            this.path = path;
            this.name = name;
            this.year = year;
            this.month = month;
        }
        public PhotoInfo() {}
    }
}