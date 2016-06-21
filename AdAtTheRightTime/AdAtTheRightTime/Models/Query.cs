using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdAtTheRightTime.Models
{
    public class Query
    {
        [Key]
        public int QueryId { get; set; }
        public string Queries { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}