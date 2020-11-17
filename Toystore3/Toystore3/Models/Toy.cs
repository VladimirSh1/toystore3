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

        [Required(ErrorMessage ="Наименование товара должно быть заполнено")]
        [Display(Name="Наименование")]
        public string Name { set; get; }

        [Required(ErrorMessage ="Цена товара должна быть указана")]
        [Display(Name="Цена")]
        [Range(1, 999999, ErrorMessage ="Цена больше 999999")]
        [RegularExpression(@"^(?:[1-9]\d*|0)?$", ErrorMessage ="Допустимо только неотрицательное число")]
        public int Price { set; get; }

        [Display(Name="Id сотрудника")]
        public int? EmployeeId { set; get; }
        public Employee Employee { set; get; }
    }
}