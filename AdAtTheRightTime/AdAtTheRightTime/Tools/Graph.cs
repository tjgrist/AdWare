using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdAtTheRightTime.Models
{
    public class Graph
    {
        public string name;
        public List<DataPoint> dataPoints;

        public Graph(string name, List<DataPoint> dataPoints)
        {
            this.name = name;
            this.dataPoints = dataPoints;
        }
    }
}