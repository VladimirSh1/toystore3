using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toystore3.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int ItemsPerPage { get; set; }
        public int ItemsCount { get; set; }
        public int PagesCount
        {
            get
            {
                return (int)Math.Ceiling((double)ItemsCount / ItemsPerPage);
            }
        }
    }

}