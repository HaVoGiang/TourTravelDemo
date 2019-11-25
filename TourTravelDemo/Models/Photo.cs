using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TourTravelDemo.Models
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Tour")]
        public int TourId { get; set; }

        public string PathPhoto { get; set; }

        public int SortOrder { get; set; }

        public bool Publish { get; set; }

        public bool IsAvatar { get; set; }
        public virtual Tour Tour { get; set; }
    }
}