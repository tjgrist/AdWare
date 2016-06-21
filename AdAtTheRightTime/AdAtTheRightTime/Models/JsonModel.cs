using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace AdAtTheRightTime.Tools
{
    public class JsonModel : DataFormat
    {
        public string jsonString;
        public JsonModel()
        {
        }
        public JsonModel(string json)
        {
            this.jsonString = json;
        }

        public void setDataString(string url)
        {
            string json = new WebClient().DownloadString(url);
            this.jsonString = json;
        }
    }
}