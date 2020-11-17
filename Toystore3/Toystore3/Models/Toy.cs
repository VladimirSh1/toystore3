using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Toystore3.Models
{
    public class Toy
    {
        [HiddenInput(DisplayValue =false)]
        public int Id { set; get; }

        [Required]
        [Display(Name="Наименование")]
        public string Name { set; get; }

        [Required]
        [Display(Name="Цена")]
        public int Price { set; get; }

        [Display(Name="Id сотрудника")]
        public int? EmployeeId { set; get; }
        public Employee Employee { set; get; }
    }
}