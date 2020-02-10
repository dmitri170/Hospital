using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.ViewModels;
using Hospital.ViewModels.ServisesViewModels;
using Hospital.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Infrastructure;
using Hospital.Infrastructure.Filtres;

namespace Hospital.Controllers
{
    public class ServisesController : Controller
    {
        private readonly HospitalContext context;
        private ServiseService serviseService;
        private int provisionN = 0, priceServiseN = 0;
        private string nameTypeN = "", datePriceN = "01.01.1999";
        private int pageN = 1;
        private SortState sortOrderN = SortState.ServisesIdAsc;

        private ServiseViewModel _servise = new ServiseViewModel
        {
            DateOfServiceProvision=Convert.ToDateTime("01.01.1999"),
        };
        public ServisesController(HospitalContext context, ServiseService serviseService)
        {
            this.context = context;
            this.serviseService = serviseService;
        }
        [SetToSession("Servises")]
        public IActionResult Index(int? provision,string nameType,int priceServise,string datePrice = "01.01.1001", int page = 1, SortState sortOrder = SortState.TreatmentIdAsc, string cacheKey = "NoCache")
        {
            
            var sessionOrganizations = HttpContext.Session.Get("Servises");
            if (sessionOrganizations != null && provision == null && priceServise==null &&nameType== null && datePrice == "01.01.1981" &&  page == 0 && sortOrder == SortState.ServisesIdAsc && cacheKey == "NoCache")
            {
                if (sessionOrganizations.Keys.Contains("provision"))
                    provision = Convert.ToInt32(sessionOrganizations["provision"]);
                if (sessionOrganizations.Keys.Contains("priceServise"))
                    priceServise = Convert.ToInt32(sessionOrganizations["priceServise"]);
                if (sessionOrganizations.Keys.Contains("nameType"))
                    nameType = sessionOrganizations["nameType"];
                if (sessionOrganizations.Keys.Contains("datePrice"))
                    datePrice = sessionOrganizations["datePrice"];
                if (sessionOrganizations.Keys.Contains("page"))
                    page = Convert.ToInt32(sessionOrganizations["page"]);
                if (sessionOrganizations.Keys.Contains("sortOrder"))
                    sortOrder = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
                cacheKey = "ServisesCache";
            }
            if (cacheKey == "Edit")
                cacheKey = "NoCache";
            ServisesViewModel servises = serviseService.GetServises(provision,nameType,priceServise,datePrice, page, sortOrder, cacheKey);
            return View(servises);
        }
        [HttpGet]
        public ActionResult EditServises(int? id)
        {
            var serviseContext = context.Servises.Include(p => p.Provision);
            var items = serviseContext.Where(p => p.ServisesId == id).ToList();
            var provisions = new SelectList(context.ProvisionOfPaidServices, "ProvisionId", "DateOfServiceProvision", items.First().ProvisionId);
            ServisesViewModel servises= new ServisesViewModel
            {
                Servises = items,
                ServiseViewModel = _servise,
                ProvisionList = provisions,
            };
            return View(servises);
        }

        [HttpPost]
        public ActionResult EditServises(Servise servise)
        {
            context.Servises.Update(servise);
            context.SaveChanges();
            var sessionOrganizations = HttpContext.Session.Get("Servises");
            if (sessionOrganizations.Keys.Contains("provision"))
                provisionN = Convert.ToInt32(sessionOrganizations["provision"]);
            if (sessionOrganizations.Keys.Contains("priceServise"))
                priceServiseN = Convert.ToInt32(sessionOrganizations["priceServise"]);
            if (sessionOrganizations.Keys.Contains("nameType"))
                nameTypeN = sessionOrganizations["nameType"];
            if (sessionOrganizations.Keys.Contains("datePrice"))
                datePriceN = sessionOrganizations["datePrice"];
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("index", new { provision = provisionN,priceServise=priceServiseN, nameType = nameTypeN, datePrice = datePriceN, page = pageN, cacheKey = "Edit" });
        }
        [HttpGet]
        [ActionName("DeleteServises")]
        public ActionResult ConfirmDeleteServises(int id)
        {
            var serviseContext = context.Servises.Include(p => p.Provision);
            var items = serviseContext.Where(p => p.ServisesId == id).ToList();
            var provision = new SelectList(context.ProvisionOfPaidServices, "ProvisionId", "DateOfServiceProvision", items.First().ProvisionId);
            ServiseViewModel treatmentView = new ServiseViewModel
            {
                ServisesId = items.First().ServisesId,
                NameType = items.First().NameType,
                PriceService= items.First().PriceService,
                DateOfServiceProvision = items.First().Provision.DateOfServiceProvision,
                DatePrice = items.First().DataPrice,
            };
            ServisesViewModel treatment = new ServisesViewModel
            {
                Servises = items,
                ServiseViewModel = _servise,
                ProvisionList = provision,
            };
            if (items == null)
                return View("NotFound");
            else
                return View(treatment);
        }

        [HttpPost]
        public ActionResult DeleteServises(int id)
        {
            try
            {
                var servise = context.Servises.FirstOrDefault(c => c.ServisesId == id);
                context.Servises.Remove(servise);
                context.SaveChanges();
            }
            catch { }
            return RedirectToAction("index", new { provision = provisionN, priceServise = priceServiseN, nameType = nameTypeN, datePrice = datePriceN, page = pageN, cacheKey = "Edit" });
        }

        [HttpGet]
        public IActionResult CreateServises()
        {
            var provision = new SelectList(context.ProvisionOfPaidServices, "ProvisionId", "DateOfServiceProvision");
            ViewBag.ProvisionId = provision;
            return View();
        }
        [HttpPost]
        public IActionResult CreateServises(Servise servise)
        {
            context.Servises.Add(servise);
            context.SaveChanges();
            var sessionOrganizations = HttpContext.Session.Get("Servises");
            if (sessionOrganizations.Keys.Contains("provision"))
                provisionN = Convert.ToInt32(sessionOrganizations["provision"]);
            if (sessionOrganizations.Keys.Contains("priceServise"))
                priceServiseN = Convert.ToInt32(sessionOrganizations["priceServise"]);
            if (sessionOrganizations.Keys.Contains("nameType"))
                nameTypeN = sessionOrganizations["nameType"];
            if (sessionOrganizations.Keys.Contains("datePrice"))
                datePriceN = sessionOrganizations["datePrice"];
            if (sessionOrganizations.Keys.Contains("page"))
                pageN = Convert.ToInt32(sessionOrganizations["page"]);
            if (sessionOrganizations.Keys.Contains("sortOrder"))
                sortOrderN = (SortState)Enum.Parse(typeof(SortState), sessionOrganizations["sortOrder"]);
            return RedirectToAction("index", new { provision = provisionN, priceServise = priceServiseN, nameType = nameTypeN, datePrice = datePriceN, page = pageN, cacheKey = "Edit" });
        }
    }
}

