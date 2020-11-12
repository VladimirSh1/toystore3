using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toystore3.Models
{
    public class Toy
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Price { set; get; }
        public int? EmployeeId { set; get; }
        public Employee Employee { set; get; }
    }
}