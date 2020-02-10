using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.ViewModels;
using Hospital.Services;
using Microsoft.EntityFrameworkCore;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.ViewModels.DoctorsViewModels;
using Hospital.ViewModels.ProcedureViewModels;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Filtres;

namespace Hospital.Controllers
{
    public class DoctorsController:Controller
    {
        private readonly HospitalContext context;
        private DoctorService doctorService;
        private string doctorSurnameN = "", doctorNameN = "", specialtiesN = "", categoriesN = "";
        private int departmentN = 0;
        private int pageN = 1;
        private SortState sortOrderN = SortState.DoctorsIdAsc;

        private DoctorViewModel _doctor = new DoctorViewModel
        {
            NameDepartments = ""
        };

        public DoctorsController(HospitalContext context, DoctorService doctorService)
        {
            this.context = context;
            this.doctorService =doctorService;
        }
        [SetToSession("Doctors")]
        public IActionResult Index(int? department, string doctorSurname, string doctorName, string specialties, string categories, int page = 1, SortState sortOrder = SortState.DoctorsIdAsc, string cacheKey = "NoCache")
        {
            var sessionOrganizations = HttpContext.Session.Get("Doctors");
            if(sessionOrganizations!=null && doctorName==null && doctorSurname==null && specialties==null && categories==null && page==0 && sortOrder==SortState.DoctorsIdAsc && cacheKey=="NoCache")
            {
                if (sessionOrganizations.Keys.Contains("department"))
                    department = Convert.ToInt32(sessionOrganizations["department"]);
                if (sessionOrganizations.Keys.Contains("doctorSurname"))
                    doctorSurname = sessionOrganizations["doctorSurname"];
                if (sessionOrganizations.Keys.Contains("doctorName"))
                    doctorName = sessionOrganizations["doctorName"];
                if (sessionOrganizations.Keys.Contains("specialties"))
                    specialties = sessionOrganizations["specialties"];
                if (sessionOrganizations.Keys.Contains("categories"))
                    categories = sessionOrganizations["categories"];
                if (sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                cacheKey = "Doctor";
            }
            if (cacheKey == "Edit")
                cacheKey = "Nocache";
            DoctorsViewModel doctors = doctorService.GetDoctor(department,doctorSurname,doctorName,specialties,categories, page, sortOrder, cacheKey);
            return View(doctors);
        }

        [HttpGet]
        public ActionResult DoctorEdit(int? id)
        {
            var doctorContext = context.Doctors.Include(p => p.Department);
            var items = doctorContext.Where(p => p.DoctorsId == id).ToList();
            var department = new SelectList(context.Departments, "DepartmentId", "NameDepartments", items.First().DepartmentId);
            DoctorsViewModel doctor = new DoctorsViewModel
            {
                Doctors = items,
                DoctorViewModel = _doctor,
                DepartmentList = department,
            };
            return View(doctor);
        }

        [HttpPost]
        public ActionResult DoctorEdit(Doctor doctor)
        {
            
                context.Doctors.Update(doctor);
                // сохраняем в бд все изменения
                context.SaveChanges();
                var sessionOrganizations = HttpContext.Session.Get("Doctors");
                if (sessionOrganizations.Keys.Contains("doctorSurname"))
                    doctorSurnameN = sessionOrganizations["doctorSurname"];
                if (sessionOrganizations.Keys.Contains("department"))
                    departmentN = Convert.ToInt32(sessionOrganizations["department"]);
                if (sessionOrganizations.Keys.Contains("doctorName"))
                    doctorNameN = sessionOrganizations["doctorName"];
                if (sessionOrganizations.Keys.Contains("specialties"))
                    specialtiesN = sessionOrganizations["specialties"];
                if (sessionOrganizations.Keys.Contains("categories"))
                    categoriesN = sessionOrganizations["categories"];
                if (sessionOrganizations.Keys.Contains("page"))
                    pageN = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                return RedirectToAction("Index", new { department=departmentN, doctorSurname = doctorSurnameN, doctorName = doctorNameN, categories = categoriesN,specialties=specialtiesN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });
           
        }

        [HttpGet]
        [ActionName("DoctorDelete")]
        public ActionResult ConfirmDoctorDelete(int id)
        {
            var doctorContext = context.Doctors.Include(p => p.Department);
            var items = doctorContext.Where(p => p.DoctorsId == id).ToList();
            var department = new SelectList(items, "Department", "NameDepartments"); 
            DoctorViewModel doctorView = new DoctorViewModel
            {
                DoctorsId = items.First().DoctorsId,
                Name = items.First().DoctorNames,
                Surname = items.First().DoctorSurnames,
                Specialties = items.First().Specialties,
                Categories = items.First().Categories,
                NameDepartments = items.First().Department.NameDepartments,
            };
            DoctorsViewModel doctor = new DoctorsViewModel
            {
                Doctors = items,
                DoctorViewModel = doctorView,
                DepartmentList = department
            };
            if (items == null)
                return View("NotFound");
            else
                return View(doctor);
        }

        [HttpPost]
        public ActionResult DoctorDelete(int id)
        {
            try
            {
                var doctor = context.Doctors.FirstOrDefault(c => c.DoctorsId == id);
                context.Doctors.Remove(doctor);
                context.SaveChanges();
            }
            catch { }
            return RedirectToAction("Index", new { department = departmentN, doctorSurname = doctorSurnameN, doctorName = doctorNameN, categories = categoriesN, specialties = specialtiesN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });

        }
    

        [HttpGet]
        public IActionResult DoctorCreate()
        {
            var department = new SelectList(context.Departments, "DepartmentId", "NameDepartments"); 
            ViewBag.DepartmentId = department;
            return View();
        }
        [HttpPost]
        public ActionResult DoctorCreate(Doctor doctor)
        {
            context.Doctors.Update(doctor);
            // сохраняем в бд все изменения
            context.SaveChanges();
            var sessionOrganizations = HttpContext.Session.Get("Doctors");
            if (sessionOrganizations.Keys.Contains("doctorSurname"))
                doctorSurnameN = sessionOrganizations["doctorSurname"];
            if (sessionOrganizations.Keys.Contains("department"))
                departmentN = Convert.ToInt32(sessionOrganizations["department"]);
            if (sessionOrganizations.Keys.Contains("doctorName"))
                doctorNameN = sessionOrganizations["doctorName"];
            if (sessionOrganizations.Keys.Contains("specialties"))
                specialtiesN = sessionOrganizations["specialties"];
            if (sessionOrganizations.Keys.Contains("categories"))
                categoriesN = sessionOrganizations["categories"];
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("Index", new { department = departmentN, doctorSurname = doctorSurnameN, doctorName = doctorNameN, categories = categoriesN, specialties = specialtiesN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });

        }
    }
}

