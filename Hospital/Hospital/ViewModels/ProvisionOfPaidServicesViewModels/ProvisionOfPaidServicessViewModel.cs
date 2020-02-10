using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.ViewModels.ProvisionOfPaidServicesViewModels;

namespace Hospital.ViewModels.ProvisionOfPaidServicesViewModels
{
    public class ProvisionOfPaidServicessViewModel
    {
        public IEnumerable<ProvisionOfPaidService> ProvisionOfPaidServices { get; set; }
        public ProvisionOfPaidService ProvisionOfPaidService { get; set; }
        public ProvisionOfPaidServicesViewModel ProvisionOfPaidServicesViewModel { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public SelectList DoctorsList { get; set; }
        public SelectList PacientList { get; set; }
        public ProvisionOfPaidServicesSortViewModel SortViewModel { get; set; }
        public ProvisionOfPaidServicesFilterViewModel FilterViewModel { get; set; }
    }
}
