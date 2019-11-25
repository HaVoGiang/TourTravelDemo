﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TourTravelDemo.Models
{
    public class CategoryBlog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // Id danh mục tự nhập
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string FormatUrl { get; set; }

        public bool Publish { get; set; }

        public int SortOrder { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public byte Status { get; set; }

        public ICollection<Blog> Blogs { get; set; }
    }
}