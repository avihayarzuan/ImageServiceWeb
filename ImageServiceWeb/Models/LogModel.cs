using ImageServiceWeb.Communication;
using ImageServiceWeb.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServiceWeb.Models
{
    public class LogModel
    {
        private Client client;
        public List<LogMessage> logList;
        public List<LogMessage> LogList
        {
            get { return logList; }
        }

        public LogModel()
        {
            this.client = Client.Instance;
            this.logList = new List<LogMessage>();
            this.client.LoggerCommandRecievd += LogRecieved;
        }

        /// <summary>
        /// Given Log from the client class, the method parse the Log
        /// check if its a full table or a single Log and send to the VM
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="msg"></param>
        public void LogRecieved(object sender, MessageEventArgs msg)
        {
            logList.Clear();
            string log = msg.Message;
            JObject obj = JObject.Parse(log);
            if (obj["firstTime"].ToString().Equals("true"))
            {
                Dictionary<int, string[]> map = new Dictionary<int, string[]>
                    (JsonConvert.DeserializeObject<Dictionary<int, string[]>>(obj["logMap"].ToString()));
                int i;
                int size = map.Count;
                for (i = 1; i < size; i++)
                {
                    string[] str = map[i];
                    string type = GetType(str[0]);
                    string message = str[1];
                    LogMessage logMessage = new LogMessage(type, message);

                    logList.Add(logMessage);

                }
            }
        }

        /// <summary>
        /// The method converts from the message Type to the required one.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string GetType(string msg)
        {
            if (msg.Equals("Information"))
            {
                return "INFO";
            }
            else if (msg.Equals("Warning"))
            {
                return "WARNING";
            }
            else
            {
                return "ERROR";
            }
        }

        /// <summary>
        /// The method asks from the client class to get the log table
        /// </summary>
        public void GetLog()
        {
            int msg = (int)Infrastructure.Enums.CommandEnum.LogCommand;
            this.client.SendData(msg.ToString());
        }


    }
}