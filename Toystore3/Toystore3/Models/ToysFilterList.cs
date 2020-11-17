using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Toystore3.Models
{
    public class ToysFilterList
    {
        public IEnumerable<Toy> Toys { get; set; }
        public SelectList Employees { get; set; }
    }
}