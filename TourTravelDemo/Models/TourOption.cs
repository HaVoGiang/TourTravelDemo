using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourTravelDemo.Models
{
    public class TourOption
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public int SortOrder { get; set; }

        public bool Publish { get; set; }

        public virtual ICollection<TourOptionRelate> TourOptionRelates { get; set; }
    }
}