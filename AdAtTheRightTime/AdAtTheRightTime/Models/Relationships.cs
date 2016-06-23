using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AdAtTheRightTime.Models
{
    public class Relationships
    {
        [Key]
        public int RelationshipId { get; set; }
        public int? BusinessId { get; set; }
        public Business Business { get; set; }
        [ForeignKey("AppUser")]
        public string UserId { get; set; }
        public ApplicationUser AppUser { get; set; }

    }
}