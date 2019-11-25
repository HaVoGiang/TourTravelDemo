using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TourTravelDemo.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CategoryBlog")]
        public int CategoryBlogId { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Photo { get; set; }

        public string Description { get; set; }

        [AllowHtml]
        public string Content { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public int SortOrder { get; set; }

        public bool Publish { get; set; }

        public byte Status { get; set; }

        public virtual CategoryBlog CategoryBlog { get; set; }
    }
}