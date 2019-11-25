using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourTravelDemo.Models
{
    public class Tour
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        [ForeignKey("Destination")]
        public int DestinationId { get; set; }

        public string Title { get; set; }

        public int Price { get; set; }

        public int DaysTour { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public string Url { get; set; }

        public byte Status { get; set; }

        public bool Publish { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual Category Category { get; set; }

        public virtual Destination Destination { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<TourOptionRelate> TourOptionRelates { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}