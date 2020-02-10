using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models;
using Hospital.ViewModels.DoctorsViewModels;

namespace Hospital.ViewModels.DoctorsViewModels
{
    public class DoctorsViewModel
    {
        public IEnumerable<Doctor> Doctors { get; set; }
        public DoctorViewModel DoctorViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SelectList DepartmentList { get; set; }
        public DoctorSortViewModel SortViewModel { get; set; }
        public DoctorFilterViewModel FilterViewModel { get; set; }
    }
}
