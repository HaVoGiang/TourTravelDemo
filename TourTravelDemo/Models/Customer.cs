﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TourTravelDemo.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string CountryCode { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}