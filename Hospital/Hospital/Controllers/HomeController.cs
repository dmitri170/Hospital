using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.TreatmentsViewModels;

namespace Hospital.Controllers
{
    
    public class HomeController : Controller
    {

        private HospitalContext _db;

        public HomeController(HospitalContext db)
        {
            _db = db;
        }
        private TreatmentViewModel _treatment = new TreatmentViewModel
        {
            PacientSurname="",
            NameDepartment="",
            DoctorSurname=""
        };
        public IActionResult Index(int page=1)
        {
            int pageSize = 10;
            var treatmentContext = _db.Treatment.Include(p => p.Department).Include(p => p.Doctors).Include(p=>p.Pacients);
            var count = treatmentContext.Count();
            var items = treatmentContext.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            HomeViewModel homeViewModel = new HomeViewModel
            {
                Treatments= items,
                TreatmentViewmodel = _treatment,
                PageViewModel = pageViewModel
            };
            return View(homeViewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
