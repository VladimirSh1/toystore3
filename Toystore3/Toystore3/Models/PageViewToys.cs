using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toystore3.Models
{
    public class PageViewToys
    {
        public IEnumerable<Toy> Toys { get; set; }
        public PageInfo PageInfo { get; set; }
    }

}