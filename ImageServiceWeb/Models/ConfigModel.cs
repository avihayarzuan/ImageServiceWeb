using ImageServiceWeb.Communication;
using ImageServiceWeb.Infrastructure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Threading;

namespace ImageServiceWeb.Models
{
    public class ConfigModel
    {
        private Client client;
        private Object obj;

        public ConfigModel(Object obj)
        {
            this.obj = obj;
            this.client = Client.Instance;
            this.m_handlers = new ObservableCollection<string>();
            this.client.SettingsConfigRecieved += SettingsConfigRecieved;
            this.client.SettingsCloseHandlerRecieved += HandlerRemoveRecived;
            this.GetConfig();
        }

        public int GetNumPhotos()
        {
            int fileCount = Directory.GetFiles(Output, "*.*", SearchOption.AllDirectories).Length;
            return (fileCount/2);
        }

        /// <summary>
        /// Updating our settings configuration
        /// </summary>
        /// <param name="sender"> Event sender</param>
        /// <param name="msg"> The message recived from server</param>
        public void SettingsConfigRecieved(object sender, MessageEventArgs msg)
        {
            Handlers.Clear();
            // Parsing our message
            string message = msg.Message;
            JObject obj = JObject.Parse(message);
            // Updating each configuratoin field
            Output = obj["Output"].ToString();
            SourceName = obj["SourceName"].ToString();
            LogName = obj["LogName"].ToString();
            int.TryParse(obj["thumbnailSize"].ToString(), out int thumbnailSize);
            ThumbnailSize = thumbnailSize;
            // Parsing our handlerPaths
            string[] handlerPaths = JsonConvert.DeserializeObject<string[]>(obj["handlersPaths"].ToString());
            // Adding each one to our observable collection list
            foreach (string str in handlerPaths)
            {
                    Handlers.Add(str);
            }
        }

        /// <summary>
        /// Removing a handler
        /// </summary>
        /// <param name="sender"> Event sender</param>
        /// <param name="e">The handler path to be removed</param>
        public void HandlerRemoveRecived(object sender, MessageEventArgs e)
        {
            // Notifying we can return to view since handler has been deleted
            Monitor.Pulse(obj);
            // Checking if the path is indeed on the list and removing it
            //if (this.Handlers.Contains(path))
            //{
            //    Handlers.Remove(path);
            //}

        }

        /// <summary>
        /// Sending the server the configuration command
        /// </summary>
        public void GetConfig()
        {
            int msg = (int)Infrastructure.Enums.CommandEnum.GetConfigCommand;
            this.client.SendData(msg.ToString());
        }

        /// <summary>
        /// Sending the server to remove a handler
        /// </summary>
        /// <param name="handlerPath"> The handlers path</param>
        public void RemoveHandler(string handlerPath)
        {
            int msg = (int)Infrastructure.Enums.CommandEnum.CloseCommand;
            this.client.SendData(msg.ToString() + " " + handlerPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string name)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private ObservableCollection<string> m_handlers;
        public ObservableCollection<string> Handlers
        {
            get { return m_handlers; }
            set { }
        }

        private string m_chosenHandler;
        public string ChosenHandler
        {
            get { return m_chosenHandler; }
            set
            {
                m_chosenHandler = value;
            }
        }

        private string m_output;
        public string Output
        {
            get { return m_output; }
            set 
            {
                m_output = value;
            }
        }

        private string m_sourceName;
        public string SourceName
        {
            get { return m_sourceName; }
            set
            {
                m_sourceName = value;
            }
        }

        private string m_logName;
        public string LogName
        {
            get { return m_logName; }
            set
            {
                m_logName = value;
            }
        }

        private int m_thumbnailSize;
        public int ThumbnailSize
        {
            get { return m_thumbnailSize; }
            set
            {
                m_thumbnailSize = value;
            }
        }
    }
}
