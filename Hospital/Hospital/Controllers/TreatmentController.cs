using Hospital.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using Hospital.Models;
using Hospital.Services;
using Hospital.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital.ViewModels.TreatmentsViewModels;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Filtres;

namespace Hospital.Controllers
{
    public class TreatmentController:Controller
    {
        private readonly HospitalContext context;
        private TreatmentService treatmentService;
        private int departmentN = 0, doctorN = 0, pacientN = 0;
        private string receiptDateN = "01.01.1999", dischargeDateN = "10.10.2024", diagnosisN = "";
        private int pageN = 1;

        private SortState sortOrderN = SortState.TreatmentIdAsc;
        private TreatmentViewModel _treatment = new TreatmentViewModel
        {
            DoctorSurname = "",
            NameDepartment = "",
            PacientSurname = ""
        };
        public TreatmentController(HospitalContext context,TreatmentService treatmentService)
        {
            this.context = context;
            this.treatmentService = treatmentService;
        }
        [SetToSession("Treatments")]
        public IActionResult Index(int? department, int? pacient, int? doctor, string diagnosis, string receiptDate="01.01.1001",string dischargeDate="01.01.2020",int page=1,SortState sortOrder=SortState.TreatmentIdAsc,string cacheKey="NoCache")
        {
            var sessionOrganizations = HttpContext.Session.Get("Treatments");
            if (sessionOrganizations != null && department == null && pacient == null &&doctor==null&&diagnosis==null&& receiptDate == "01.01.1999" && dischargeDate == "10.01.2091" && page == 0 && sortOrder == SortState.TreatmentIdAsc && cacheKey == "NoCache")
            {
                if (sessionOrganizations.Keys.Contains("department"))
                    department = Convert.ToInt32(sessionOrganizations["department"]);
                if (sessionOrganizations.Keys.Contains("pacient"))
                    pacient = Convert.ToInt32(sessionOrganizations["pacient"]);
                if (sessionOrganizations.Keys.Contains("doctor"))
                    doctor = Convert.ToInt32(sessionOrganizations["doctor"]);
                if (sessionOrganizations.Keys.Contains("diagnosis"))
                    diagnosis = sessionOrganizations["diagnosis"];
                if (sessionOrganizations.Keys.Contains("receiptDate"))
                    receiptDate = sessionOrganizations["receiptDate"];
                if (sessionOrganizations.Keys.Contains("dischargeDate"))
                    dischargeDate = sessionOrganizations["dischargeDate"];
                if (sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                cacheKey = "TreatmentsCache";
            }
            if (cacheKey == "Edit")
                cacheKey = "NoCache";
            TreatmentsViewModel treatments = treatmentService.GetTreatments(department,pacient,doctor,diagnosis,receiptDate,dischargeDate, page, sortOrder, cacheKey);
            return View(treatments);
        }
        [HttpGet]
        public ActionResult EditTreatment(int? id)
        {
            var treatmentContext = context.Treatment.Include(p => p.Department).Include(p => p.Doctors).Include(p => p.Pacients);
            var items = treatmentContext.Where(p => p.TreatmentId == id).ToList();
            var departments = new SelectList(context.Departments, "DepartmentId","NameDepartments",items.First().DepartmentId);
            var pacients = new SelectList(context.Pacients, "PacientsId","PatientSurnames",items.First().PacientsId);
            var doctors = new SelectList(context.Doctors, "DoctorsId","DoctorSurnames",items.First().DoctorsId);
            TreatmentsViewModel treatment = new TreatmentsViewModel
            {
                Treatments=items,
                TreatmentViewModel=_treatment,
                DepartmentList=departments,
                PacientList=pacients,
                DoctorsList=doctors
            };
            return View(treatment);
        }
        
        [HttpPost]
        public ActionResult EditTreatment(Treatment treatment)
        {
                context.Treatment.Update(treatment);
                context.SaveChanges();
                var sessionOrganizations = HttpContext.Session.Get("Treatments");
                if (sessionOrganizations.Keys.Contains("department"))
                    departmentN = Convert.ToInt32(sessionOrganizations["department"]);
                if (sessionOrganizations.Keys.Contains("pacient"))
                    pacientN = Convert.ToInt32(sessionOrganizations["pacient"]);
                if (sessionOrganizations.Keys.Contains("doctor"))
                    doctorN = Convert.ToInt32(sessionOrganizations["doctor"]);
                if (sessionOrganizations.Keys.Contains("diagnosis"))
                    diagnosisN = sessionOrganizations["diagnosis"];
                if (sessionOrganizations.Keys.Contains("receiptDate"))
                    receiptDateN = sessionOrganizations["receiptDate"];
                if (sessionOrganizations.Keys.Contains("dischargeDate"))
                    dischargeDateN = sessionOrganizations["dischargeDate"];
                if (sessionOrganizations.Keys.Contains("page"))
                    pageN = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                return RedirectToAction("index", new { department = departmentN, pacient = pacientN, doctor = doctorN, diagnosis = diagnosisN, receiptDate = receiptDateN, dischargeDate = dischargeDateN, page = pageN, cacheKey = "Edit" });

        }
        [HttpGet]
        [ActionName("DeleteTreatment")]
        public ActionResult ConfirmDeleteTreatment(int id)
        {
            var treatmentContext = context.Treatment.Include(p => p.Department).Include(p => p.Doctors).Include(p => p.Pacients);
            var items = treatmentContext.Where(p => p.TreatmentId == id).ToList();
            var department = new SelectList(context.Departments, "DepartmentId", "NameDepartments", items.First().DepartmentId);
            var pacient = new SelectList(context.Pacients, "PacientsId", "PacientSurname", items.First().PacientsId);
            var doctor = new SelectList(context.Doctors, "DoctorsId", "DoctorSurname", items.First().DoctorsId);
            TreatmentViewModel treatmentView = new TreatmentViewModel
            {
                TreatmentId = items.First().TreatmentId,
                Diagnosis = items.First().Diagnosis,
                DoctorSurname = items.First().Doctors.DoctorSurnames,
                PacientSurname = items.First().Pacients.PatientSurnames,
                NameDepartment = items.First().Department.NameDepartments,
                ReceiptDate = items.First().ReceiptDate,
                DischargeDate=items.First().DischargeDate
            };
            TreatmentsViewModel treatment = new TreatmentsViewModel
            {
                Treatments = items,
                TreatmentViewModel = treatmentView,
                DepartmentList = department,
                PacientList = pacient,
                DoctorsList = doctor
            };
            if (items == null)
                return View("NotFound");
            else
                return View(treatment);
        }

        [HttpPost]
        public ActionResult DeleteTreatment(int id)
        {
            try
            {
                var treatment = context.Treatment.FirstOrDefault(c => c.TreatmentId == id);
                context.Treatment.Remove(treatment);
                context.SaveChanges();
            }
            catch { }
return RedirectToAction("index", new { department = departmentN, pacient = pacientN, doctor = doctorN, diagnosis = diagnosisN, receiptDate = receiptDateN, dischargeDate = dischargeDateN, page = pageN, cacheKey = "Edit" });

        }

        [HttpGet]
        public IActionResult CreateTreatment()
        {
            var department = new SelectList(context.Departments, "DepartmentId", "NameDepartments");
            var pacient = new SelectList(context.Pacients, "PacientsId", "PatientSurnames");
            var doctor = new SelectList(context.Doctors, "DoctorsId", "DoctorSurnames");
            ViewBag.DepartmentId = department;
            ViewBag.PacientsId = pacient;
            ViewBag.DoctorsId = doctor;
            return View();
        }
        [HttpPost]
        public IActionResult CreateTreatment(Treatment treatment)
        {
            context.Treatment.Add(treatment);
            context.SaveChanges();
            var sessionOrganizations = HttpContext.Session.Get("Treatments");
            if (sessionOrganizations.Keys.Contains("department"))
                departmentN = Convert.ToInt32(sessionOrganizations["department"]);
            if (sessionOrganizations.Keys.Contains("pacient"))
                pacientN = Convert.ToInt32(sessionOrganizations["pacient"]);
            if (sessionOrganizations.Keys.Contains("doctor"))
                doctorN = Convert.ToInt32(sessionOrganizations["doctor"]);
            if (sessionOrganizations.Keys.Contains("diagnosis"))
                diagnosisN = sessionOrganizations["diagnosis"];
            if (sessionOrganizations.Keys.Contains("receiptDate"))
                receiptDateN = sessionOrganizations["receiptDate"];
            if (sessionOrganizations.Keys.Contains("dischargeDate"))
                dischargeDateN = sessionOrganizations["dischargeDate"];
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("index", new { department = departmentN, pacient = pacientN, doctor = doctorN, diagnosis = diagnosisN, receiptDate = receiptDateN, dischargeDate = dischargeDateN, page = pageN, cacheKey = "Edit" });

        }
    }
}
