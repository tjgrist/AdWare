using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdAtTheRightTime.Models
{
    public class DataPoint
    {
        public string x;
        public string y;

        public DataPoint(string x, string y)
        {
            this.x = x;
            this.y = y;
        }
    }
}