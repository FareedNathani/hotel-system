using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hotelmanagementsystem.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}