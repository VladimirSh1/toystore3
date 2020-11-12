using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Toystore3.Models
{
    public class ToystoreContext: DbContext
    {
        public DbSet<Employee> Employees { set; get; }
        public DbSet<Toy> Toys { set; get; }
    }
}