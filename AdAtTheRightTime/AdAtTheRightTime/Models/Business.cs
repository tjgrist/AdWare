using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AdAtTheRightTime.Models
{
    public class Business
    {
        [Key]
        public int BusinessId { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Industry { get; set; }
    }
}