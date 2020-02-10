using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels.TreatmentsViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels.TreatmentsViewModels
{
    public class TreatmentsViewModel
    {
        public IEnumerable<Treatment> Treatments { get; set; }
        public Treatment Treatment { get; set; }
        public TreatmentViewModel TreatmentViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SelectList DoctorsList { get; set; }
        public SelectList DepartmentList { get; set; }
        public SelectList PacientList { get; set; }
        public TreatmentSortViewModel SortViewModel { get; set; }
        public TreatmentFilterViewModel FilterViewModel { get; set; }
    }
}
