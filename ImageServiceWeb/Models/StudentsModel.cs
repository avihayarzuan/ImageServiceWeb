using ImageServiceWeb.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Models
{
    public class StudentsModel
    {
        private List<StudentInfo> studentList = new List<StudentInfo>();
        public List<StudentInfo> StudentList
        {
            get { return studentList; }
            set { }
        }

        public void getStudents()
        {
            studentList.Clear();
            StreamReader file = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/StudentsInfo.txt"));
            string line;
            while ((line = file.ReadLine()) != null)
            {
                string[] info = line.Split(' ');
                studentList.Add(new StudentInfo() { firstName = info[0], lastName = info[1], id = int.Parse(info[2]) });
            }
            file.Close();
        }
    }
}