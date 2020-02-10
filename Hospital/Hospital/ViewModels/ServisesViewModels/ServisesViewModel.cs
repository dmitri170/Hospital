using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels.TreatmentsViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels.ServisesViewModels
{
    public class ServisesViewModel
    {
        public IEnumerable<Servise> Servises { get; set; }
        public ServiseViewModel ServiseViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SelectList ProvisionList { get; set; }
        public ServiseSortViewModel SortViewModel { get; set; }
        public ServiseFilterViewModel FilterViewModel { get; set; }
    }
}
