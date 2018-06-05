using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Infrastructure
{
    public class MessageEventArgs : EventArgs
    {
        public int CommandID { get; set; }
        public string Message { get; set; }

        public MessageEventArgs(int id, string msg)
        {
            CommandID = id;
            Message = msg;
        }
    }
}