using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Toystore3.Models
{
    public class Employee
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Patronymic { set; get; }
        public string Surname { set; get; }

        public ICollection<Toy> Toys { get; set; }

        public Employee()
        {
            Toys = new List<Toy>();
        }
    }
}