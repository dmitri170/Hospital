using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.PacientsViewModels;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Filtres;
using Hospital.Services;

namespace Hospital.Controllers
{
    public class PacientsController:Controller
    {
        private HospitalContext context;
        PacientService pacientService;
        private string patientSurnameN = "", patientNameN = "", adresN = "";
        private int pageN = 1;
        private int numberPalatN = 0;
        private SortState sortOrderN = SortState.PacietnsIdAsc;

        public PacientsController(HospitalContext context,PacientService pacientService)
        {
            this.context = context;
            this.pacientService = pacientService;
        }
        [SetToSession("Pacients")]
        public IActionResult Index(string patientSurname,string patientName, int numberPalat,string adres, int page = 1, SortState sortOrder = SortState.PacietnsIdAsc,string cacheKey="NoCache")
        {
            var sessionOrganizations = HttpContext.Session.Get("Pacients");
            if (sessionOrganizations != null && patientSurname == null && patientName == null&& numberPalat == null&&adres==null && page == 0 && sortOrder == SortState.PacietnsIdAsc && cacheKey == "NoCache")
            {
                if (sessionOrganizations.Keys.Contains("patientSurname"))
                    patientSurname = sessionOrganizations["patientSurname"];
                if (sessionOrganizations.Keys.Contains("patientName"))
                    patientName = sessionOrganizations["patientName"];
                if (sessionOrganizations.Keys.Contains("adres"))
                    adres = sessionOrganizations["adres"];
                if (sessionOrganizations.Keys.Contains("numberPalat"))
                    numberPalat = Convert.ToInt32(sessionOrganizations["numberPalat"]);
                if (sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                cacheKey = "Pacient";
            }
            if (cacheKey == "Edit")
                cacheKey = "NoCache";
            PacientsViewModel pacients = pacientService.GetPacients(patientSurname,patientName,numberPalat,adres,page, sortOrder, cacheKey);
            return View(pacients);
        }

        [HttpGet]
        public ActionResult EditPacient(int? id)
        {
            Pacient pacient = context.Pacients.Find(id);
            return View(pacient);
        }

        [HttpPost]
        public ActionResult EditPacient(Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                context.Pacients.Update(pacient);
                // сохраняем в бд все изменения
                context.SaveChanges();
                var sessionOrganizations = HttpContext.Session.Get("Pacients");
                if (sessionOrganizations.Keys.Contains("patientSurname"))
                    patientSurnameN = sessionOrganizations["patientSurname"];
                if (sessionOrganizations.Keys.Contains("patientName"))
                    patientNameN = sessionOrganizations["patientName"];
                if (sessionOrganizations.Keys.Contains("adres"))
                    adresN = sessionOrganizations["adres"];
                if (sessionOrganizations.Keys.Contains("numberPalat"))
                    numberPalatN = Convert.ToInt32(sessionOrganizations["numberPalat"]);
                if (sessionOrganizations.Keys.Contains("page"))
                    pageN = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                return RedirectToAction("Index", new { patientSurname = patientSurnameN,patientName = patientNameN, adres = adresN,numberPalatN=numberPalatN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });
            }
            else
                return View(pacient);
        }

        [HttpGet]
        [ActionName("DeletePacient")]
        public ActionResult ConfirmDeletePacient(int id)
        {

            Pacient pacient = context.Pacients.Find(id);

            if (pacient == null)
                return View("NotFound");
            else
                return View(pacient);
        }

        [HttpPost]
        public ActionResult DeletePacient(int id)
        {
            try
            {
                var pacient = context.Pacients.FirstOrDefault(c => c.PacientsId == id);
                context.Pacients.Remove(pacient);
                context.SaveChanges();
            }
            catch { }
            var sessionOrganizations = HttpContext.Session.Get("Pacients");
            if (sessionOrganizations.Keys.Contains("patientSurname"))
                patientSurnameN = sessionOrganizations["patientSurname"];
            if (sessionOrganizations.Keys.Contains("patientName"))
                patientNameN = sessionOrganizations["patientName"];
            if (sessionOrganizations.Keys.Contains("adres"))
                adresN = sessionOrganizations["adres"];
            if (sessionOrganizations.Keys.Contains("numberPalat"))
                numberPalatN = Convert.ToInt32(sessionOrganizations["numberPalat"]);
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("Index", new { patientSurname = patientSurnameN, patientName = patientNameN, adres = adresN, numberPalatN = numberPalatN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });
        
        }

        [HttpGet]
        public IActionResult CreateRate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePacient(Pacient pacient)
        {
            if (ModelState.IsValid)
            {
                context.Pacients.Update(pacient);
                // сохраняем в бд все изменения
                context.SaveChanges();
                var sessionOrganizations = HttpContext.Session.Get("Pacients");
                if (sessionOrganizations.Keys.Contains("patientSurname"))
                    patientSurnameN = sessionOrganizations["patientSurname"];
                if (sessionOrganizations.Keys.Contains("patientName"))
                    patientNameN = sessionOrganizations["patientName"];
                if (sessionOrganizations.Keys.Contains("adres"))
                    adresN = sessionOrganizations["adres"];
                if (sessionOrganizations.Keys.Contains("numberPalat"))
                    numberPalatN = Convert.ToInt32(sessionOrganizations["numberPalat"]);
                if (sessionOrganizations.Keys.Contains("page"))
                    pageN = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                return RedirectToAction("Index", new { patientSurname = patientSurnameN, patientName = patientNameN, adres = adresN, numberPalatN = numberPalatN, page = pageN, sortOrder = sortOrderN, cacheKey = "Edit" });
            }
            else
                return View(pacient);
        }
    }
}
