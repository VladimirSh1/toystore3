using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Toystore3.Models;

namespace Toystore3.Controllers
{
    public class HomeController : Controller
    {
        ToystoreContext db = new ToystoreContext();

        public ActionResult Index()
        {
            var toys = db.Toys.Include(t => t.Employee);

            Session["mainView"] = "Index";

            return View(toys.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            var fullNameEmpList = db.Employees.Select(e => new
            {
                e.Id,
                fullName = e.Name + " " + e.Patronymic + " " + e.Surname
            }).ToList();

            //fullNameEmpList.Add(null);

            SelectList employees = new SelectList(fullNameEmpList, "Id", "fullName", null);

            ViewBag.Employees = employees;
            return View();
        }
        [HttpPost]
        public ActionResult Create(Toy toy)
        {
            db.Toys.Add(toy);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id )
        {
            if (id == null) return HttpNotFound();

            Toy toy = db.Toys.Find(id);

            if (toy != null)
            {
                var fullNameEmpList = db.Employees.Select(e => new
                {
                    e.Id,
                    fullName = e.Name + " " + e.Patronymic + " " + e.Surname
                }).ToList();

                SelectList employees = new SelectList(fullNameEmpList, "Id", "fullName", toy.EmployeeId);

                ViewBag.Employees = employees;

                return View(toy);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Toy toy)
        {
            db.Entry(toy).State = EntityState.Modified;
            db.SaveChanges();
            //return RedirectToAction("Index");


            string link = Session["mainView"] as string;
            string pnum = Session["pageNumber"] as string;
            int? eid = (int?)Session["empId"];

            switch (link)
            {
                case "FilterIndex":
                    return RedirectToAction("FilterIndex", new { empId = eid });
                case "PageIndex":
                    return RedirectToAction("PageIndex", new {pn = pnum});
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int? id)
        {
            if (id == null) return HttpNotFound();

            Toy toy = db.Toys.Find(id);

            if (toy != null)
            {
                string fullName = "(не назначен)";
                if (toy.EmployeeId != null)
                {
                    Employee emp = db.Employees.Find(toy.EmployeeId);
                    fullName = $"{emp.Name} {emp.Patronymic} {emp.Surname}";
                }

                ViewBag.EmpName = fullName;
                return View(toy);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null) return HttpNotFound();

            Toy toy = db.Toys.Find(id);
            if (toy != null)
            {
                db.Toys.Remove(toy);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        public ActionResult PageIndex (int pn = 1)
        {
            var toyList = db.Toys.Include(t => t.Employee).ToList();
            int toysPerPage = 5;
            IEnumerable<Toy> toysToViewOnPage = toyList.Skip((pn - 1) * toysPerPage).Take(toysPerPage);
            toysToViewOnPage = toysToViewOnPage.Select(t => t);

            toysToViewOnPage = toysToViewOnPage.Select((Toy t) => {
                if (t.Employee is null) t.Employee = new Employee();
                return t; });

            PageInfo pageInfo = new PageInfo
            {
                ItemsPerPage = toysPerPage,
                PageNumber = pn,
                ItemsCount = toyList.Count
            };
            PageViewToys pageViewToys = new PageViewToys
            {
                PageInfo = pageInfo,
                Toys = toysToViewOnPage
            };

            Session["mainView"] = "PageIndex";
            Session["pageNumber"] = pn.ToString();

            return View(pageViewToys);
        }


        public ActionResult ReturnLink()
        {
            string link = Session["mainView"] as string;

            if (link == "PageIndex")
            {
                ViewBag.PN = Session["pageNumber"] as string;
                return PartialView("ReturnPLink");
            }
            if (link == "FilterIndex")
            {
                ViewBag.EmpID = (int?)Session["empId"];
                return PartialView("ReturnFLink");
            }

            return PartialView("ReturnLink");
        }

        public ActionResult FilterIndex(int? empId)
        {
            IQueryable<Toy> toys = db.Toys.Include(t => t.Employee);
            if(empId!=null && empId > 0)
            {
                toys = toys.Where(t => t.EmployeeId == empId);
            }
            else if(empId == -1)
            {
                toys = toys.Where(t => t.EmployeeId == null);
            }

            //List<Employee> slEmployee = db.Employees.ToList();

            var fullNameEmpList = db.Employees.Select(e => new
            {
                e.Id,
                fullName = e.Name + " " + e.Patronymic + " " + e.Surname
            }).ToList();


            //slEmployee.ForEach(e => e.Name = e.Name = $"{e.Name} {e.Patronymic} {e.Surname}");

            fullNameEmpList.Insert(0, new  { Id = 0, fullName = "Все" });
            fullNameEmpList.Add(new  { Id = -1, fullName = "<<--Сотрудник не назначен-->>" });

            ToysFilterList tfList = new ToysFilterList()
            {
                Toys = toys.ToList(),
                Employees = new SelectList(fullNameEmpList, "Id", "fullName")
            };

            Session["mainView"] = "FilterIndex";
            Session["empId"] = empId;

            return View(tfList);
        }


        public ActionResult FirstToy()
        {
            Toy firstToy = db.Toys.FirstOrDefault();

            return View(firstToy);
        }




        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}