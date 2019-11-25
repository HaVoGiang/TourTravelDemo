using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TourTravelDemo.Models
{
    public class TourOptionRelate
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Tour")]
        public int TourId { get; set; }

        [ForeignKey("TourOption")]
        public int TourOptionId { get; set; }

        public virtual Tour Tour { get; set; }

        public virtual TourOption TourOption { get; set; }
    }
}