using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdAtTheRightTime.Models
{
    public class QueryViewModel
    {
        public string Queries { get; set; }
        public int? BusinessId { get; set; }
        public string QueryBuiltString { get; set; }
    }
}