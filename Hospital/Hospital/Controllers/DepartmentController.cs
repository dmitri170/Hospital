using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.DepartmentsViewModels;
using Hospital.ViewModels.DepartmentViewModels;
using Hospital.ViewModels.ProcedureViewModels;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Filtres;
using Hospital.Services;
namespace Hospital.Controllers
{
    public class DepartmentController : Controller
    {
        private HospitalContext context;
        private DepartmentService departmentService;
        private string nameDepartmentN = "";
        private int numberPlaceN = 0;
        private int pageN = 1;
        private SortState sortOrderN = SortState.DepartmentIdAsc;

        public DepartmentController(HospitalContext context,DepartmentService departmentService)
        {
            this.context = context;
            this.departmentService = departmentService;
        }
        [SetToSession("Departments")]
        public IActionResult Index(string nameDepartment,int numberPlace, int page=1, SortState sortOrder=SortState.DepartmentIdAsc,string cacheKey="NoCache")
        {
            var sessionOrganizations = HttpContext.Session.Get("Deparments");
            if (sessionOrganizations != null && nameDepartment == null && numberPlace == null && page == 0 && sortOrder == SortState.DepartmentIdAsc && cacheKey == "NoCache")
            {
                if (sessionOrganizations.Keys.Contains("nameDepartment"))
                    nameDepartment = sessionOrganizations["nameDepartment"];
                if (sessionOrganizations.Keys.Contains("numberPlace"))
                    numberPlace = Convert.ToInt32(sessionOrganizations["numberPlace"]);
                if (sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                cacheKey = "Department";
            }
            if (cacheKey == "Edit")
                cacheKey = "NoCache";
            DepartmentsViewModel departments = departmentService.GetDepartments(nameDepartment,numberPlace,page,sortOrder, cacheKey);
            return View(departments);
        }

        [HttpGet]
        public ActionResult EditDepartment(int? id)
        {
            Department department = context.Departments.Find(id);
            return View(department);
        }
        [HttpPost]
        public ActionResult EditDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Update(department);
                // сохраняем в бд все изменения
                context.SaveChanges();
                var sessionOrganizations = HttpContext.Session.Get("Department");
                if (sessionOrganizations.Keys.Contains("nameDepartment"))
                    nameDepartmentN = sessionOrganizations["nameDepartment"];
                if (sessionOrganizations.Keys.Contains("numberPlace"))
                    numberPlaceN = Convert.ToInt32(sessionOrganizations["numberPlace"]);
                if (sessionOrganizations.Keys.Contains("page"))
                    pageN = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                return RedirectToAction("Index", new { nameDepartment = nameDepartmentN, numberPlace=numberPlaceN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });
            }
            else
                return View(department);
        }
        [HttpGet]
        [ActionName("DeleteDepartment")]
        public ActionResult ConfirmDeleteDepartment(int id)
        {
            Department department = context.Departments.Find(id);
            if (department == null)
                return View("NotFound");
            else
                return View(department);
        }

        [HttpPost]
        public ActionResult DeleteDepartment(int id)
        {
            try
            {
                var department = context.Departments.FirstOrDefault(c => c.DepartmentId == id);
                context.Departments.Remove(department);
                context.SaveChanges();
            }
            catch { }
            var sessionOrganizations = HttpContext.Session.Get("Department");
            if (sessionOrganizations.Keys.Contains("nameDepartment"))
                nameDepartmentN= sessionOrganizations["nameDepartment"];
            if (sessionOrganizations.Keys.Contains("numberPlace"))
                numberPlaceN = Convert.ToInt32(sessionOrganizations["numberPlace"]);
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("Index", new { nameDepartment = nameDepartmentN, numberPlace = numberPlaceN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });
        }


        [HttpGet]
        public IActionResult CreateDepartment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                context.Departments.Add(department);
                context.SaveChanges();
                var sessionOrganizations = HttpContext.Session.Get("Department");
                if (sessionOrganizations.Keys.Contains("nameDepartment"))
                    nameDepartmentN = sessionOrganizations["nameDepartment"];
                if (sessionOrganizations.Keys.Contains("numberPlace"))
                    numberPlaceN = Convert.ToInt32(sessionOrganizations["numberPlace"]);
                if (sessionOrganizations.Keys.Contains("page"))
                    pageN = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                return RedirectToAction("Index", new { nameDepartment = nameDepartmentN, numberPlaceN = numberPlaceN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });
            }
            else
                return View(department);
        }

    }
}
