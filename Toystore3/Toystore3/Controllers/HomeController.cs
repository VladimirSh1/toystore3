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
            //db.Toys.Add(toy);
            db.Entry(toy).State = EntityState.Modified;
            db.SaveChanges();
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