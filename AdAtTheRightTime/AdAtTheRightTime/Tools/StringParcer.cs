using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using Newtonsoft.Json;

namespace AdAtTheRightTime.Tools
{
    public class StringParcer<T>
    {
        public T parse(JsonModel json)
        {
            T dataParsed = JsonConvert.DeserializeObject<T>(json.jsonString);
            return dataParsed;
        }
    }
}