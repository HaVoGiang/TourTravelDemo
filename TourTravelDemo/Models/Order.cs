using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TourTravelDemo.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Tour")]
        public int TourId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public DateTime StartDate { get; set; }

        public int Adults { get; set; }

        public int Kids { get; set; }

        public DateTime CreateDate { get; set; }

        public string PaymentType { get; set; }

        public int Price { get; set; }

        public string OrderNumber { get; set; }

        public string TransNo { get; set; }

        public int Status { get; set; }

        public virtual Tour Tour { get; set; }

        public virtual Customer Customer { get; set; }
    }
}