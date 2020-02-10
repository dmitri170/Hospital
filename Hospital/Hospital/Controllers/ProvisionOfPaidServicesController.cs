using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Hospital.ViewModels;
using Hospital.ViewModels.ProvisionOfPaidServicesViewModels;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.ViewModels.ProcedureViewModels;
using Hospital.Services;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Filtres;
namespace Hospital.Controllers
{
    public class ProvisionOfPaidServicesController:Controller
    {
        private readonly HospitalContext context;
        private ProvisionOfPaidServicesServices provisionOfPaidServicesService;
        private int pacientN = 0, doctorN = 0;
        private string dateOfServiceProvisionN="01.01.1999";
        private int pageN = 1;
        private SortState sortOrderN = SortState.ProvisionIdAsc;


        private ProvisionOfPaidServicesViewModel _provision = new ProvisionOfPaidServicesViewModel
        {
            PactientSurnames = "",
            DoctorSurnames = ""
        };

        public ProvisionOfPaidServicesController(HospitalContext context,ProvisionOfPaidServicesServices provisionOfPaidServicesService)
        {
            this.context = context;
            this.provisionOfPaidServicesService = provisionOfPaidServicesService;
        }
        [SetToSession("ProvisionOfPaidServices")]
        public IActionResult Index(int? doctor, int? pacient, string dateOfServiceProvision = "01.01.1999", int page = 1, SortState sortOrder = SortState.ProvisionIdAsc, string cacheKey = "NoCache")
        {
            var sessionOrganizations = HttpContext.Session.Get("ProvisionOfPaidServices");
            if (sessionOrganizations != null && pacient == null && doctor == null && dateOfServiceProvision == "01.01.1999" && page == 0 && sortOrder == SortState.ProvisionIdAsc && cacheKey == "NoCache")
            {
                if (sessionOrganizations.Keys.Contains("pacient"))
                    pacient = Convert.ToInt32(sessionOrganizations["pacient"]);
                if (sessionOrganizations.Keys.Contains("doctor"))
                    doctor = Convert.ToInt32(sessionOrganizations["doctor"]);
                if (sessionOrganizations.Keys.Contains("dateOfServiceProvision"))
                    dateOfServiceProvision = sessionOrganizations["dateOfServiceProvision"];
                if (sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                cacheKey = "ProvisionOfPaidServicesCache";
            }
            if (cacheKey == "Edit")
                cacheKey = "NoCache";
            ProvisionOfPaidServicessViewModel provisions = provisionOfPaidServicesService.GetProvision(doctor,pacient, dateOfServiceProvision, page, sortOrder, cacheKey);
            return View(provisions);
        }

        [HttpGet]
        public ActionResult ProvisionEdit(int? id)
        {
            var provisionContext = context.ProvisionOfPaidServices.Include(p => p.Doctors).Include(p => p.Pacients);
            var items = provisionContext.Where(p => p.ProvisionId == id).ToList();
            var doctors = new SelectList(context.Doctors, "DoctorsId", "DoctorSurnames", items.First().DoctorsId);
            var pacients = new SelectList(context.Pacients, "PacientsId", "PatientSurnames", items.First().PacientsId);
            ProvisionOfPaidServicessViewModel provisions = new ProvisionOfPaidServicessViewModel
            {
                ProvisionOfPaidServices = items,
                ProvisionOfPaidServicesViewModel = _provision,
                DoctorsList = doctors,
                PacientList = pacients
            };
            return View(provisions);
        }

        [HttpPost]
        public ActionResult ProvisionEdit(ProvisionOfPaidService provision)
        {
            context.ProvisionOfPaidServices.Update(provision);
            context.SaveChanges();
            var sessionOrganizations = HttpContext.Session.Get("ProvisionOfPaidServices");
            if (sessionOrganizations.Keys.Contains("pacient"))
                pacientN = Convert.ToInt32(sessionOrganizations["pacient"]);
            if (sessionOrganizations.Keys.Contains("doctor"))
                doctorN = Convert.ToInt32(sessionOrganizations["doctor"]);
            if (sessionOrganizations.Keys.Contains("dateOfServiceProvision"))
                dateOfServiceProvisionN = sessionOrganizations["dateOfServiceProvision"];
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("index", new { pacient=pacientN, doctor = doctorN, dateOfServiceProvision = dateOfServiceProvisionN, page = pageN, cacheKey = "Edit" });
        }

        [HttpGet]
        [ActionName("ProvisionDelete")]
        public ActionResult ConfirmProvisionDelete(int id)
        {
            var provisionContext = context.ProvisionOfPaidServices.Include(p => p.Doctors).Include(p => p.Pacients);
            var items = provisionContext.Where(p => p.ProvisionId == id).ToList();
            var doctors = new SelectList(context.Doctors, "DoctorsId", "DoctorSurnames");
            var pacients = new SelectList(context.Pacients, "PacietnsId", "PatientSurnames");
            ProvisionOfPaidServicesViewModel provisionView = new ProvisionOfPaidServicesViewModel
            {
                ProvisionId = items.First().ProvisionId,
                PactientSurnames= items.First().Pacients.PatientSurnames,
                DoctorSurnames = items.First().Doctors.DoctorSurnames,
                DateOfServiceProvision= items.First().DateOfServiceProvision
            };
            ProvisionOfPaidServicessViewModel provision = new ProvisionOfPaidServicessViewModel
            {
                ProvisionOfPaidServices = items,
                ProvisionOfPaidServicesViewModel = provisionView,
                DoctorsList= doctors,
                PacientList = pacients
            };
            if (items == null)
                return View("NotFound");
            else
                return View(provision);
        }

        [HttpPost]
        public ActionResult ProvisionDelete(int id)
        {
            try
            {
                var provision = context.ProvisionOfPaidServices.FirstOrDefault(c => c.ProvisionId == id);
                context.ProvisionOfPaidServices.Remove(provision);
                context.SaveChanges();
            }
            catch { }
            var sessionOrganizations = HttpContext.Session.Get("ProvisionOfPaidServices");
            if (sessionOrganizations.Keys.Contains("pacient"))
                pacientN = Convert.ToInt32(sessionOrganizations["pacient"]);
            if (sessionOrganizations.Keys.Contains("doctor"))
                doctorN = Convert.ToInt32(sessionOrganizations["doctor"]);
            if (sessionOrganizations.Keys.Contains("dateOfServiceProvision"))
                dateOfServiceProvisionN = sessionOrganizations["dateOfServiceProvision"];
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("index", new { pacient = pacientN, doctor = doctorN, dateOfServiceProvision = dateOfServiceProvisionN, page = pageN, cacheKey = "Edit" });
        }

        [HttpGet]
        public IActionResult ProvisionCreate()
        {
            var doctors = new SelectList(context.Doctors, "DoctorsId", "DoctorSurnames"); ;
            var pacients = new SelectList(context.Pacients, "PacientsId", "PatientSurnames");
            ViewBag.DoctorsID = doctors;
            ViewBag.PacientsID = pacients;
            return View();
        }
        [HttpPost]
        public ActionResult ProvisionCreate(ProvisionOfPaidService provision)
        {
            context.ProvisionOfPaidServices.Add(provision);
            context.SaveChanges();
            var sessionOrganizations = HttpContext.Session.Get("ProvisionOfPaidServices");
            if (sessionOrganizations.Keys.Contains("pacient"))
                pacientN = Convert.ToInt32(sessionOrganizations["pacient"]);
            if (sessionOrganizations.Keys.Contains("doctor"))
                doctorN = Convert.ToInt32(sessionOrganizations["doctor"]);
            if (sessionOrganizations.Keys.Contains("dateOfServiceProvision"))
                dateOfServiceProvisionN = sessionOrganizations["dateOfServiceProvision"];
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("index", new { pacient = pacientN, doctor = doctorN, dateOfServiceProvision = dateOfServiceProvisionN, page = pageN, cacheKey = "Edit" });
        }
    }
}
