using BlogDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppServiceLayer.Models
{
    public class BlogInfoMolel
    {
        public int BlogInfoId { get; set; }
        public string Title { get; set; }
        public string Subject { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string BlogUrl { get; set; }
        public virtual EmpInfo Employee { get; set; }


    }
}