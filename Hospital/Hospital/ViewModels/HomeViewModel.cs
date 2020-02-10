using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels.TreatmentsViewModels;

namespace Hospital.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Treatment> Treatments { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public TreatmentViewModel TreatmentViewmodel { get; set; }
    }
}
