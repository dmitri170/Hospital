using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.ProvisionOfPaidServicesViewModels
{
    public class ProvisionOfPaidServicesSortViewModel
    {
        public SortState ProvisionIdSort { get; private set; }
        public SortState DoctorSurnameSort { get; private set; }
        public SortState PacientSurnameSort { get; private set; }
        public SortState DateOfServiceProvisionSort { get; private set; }
        public SortState Current { get; private set; }
        public ProvisionOfPaidServicesSortViewModel(SortState sortOrder)
        {
            ProvisionIdSort = sortOrder == SortState.ProvisionIdAsc ? SortState.ProvisionIdDesc : SortState.ProvisionIdAsc;
            DoctorSurnameSort = sortOrder == SortState.DoctorSurnameAsc ? SortState.DoctorSurnameDesc : SortState.DoctorSurnameAsc;
            PacientSurnameSort = sortOrder == SortState.PatientSurnamesAsc ? SortState.PatientSurnamesDesc : SortState.PatientSurnamesAsc;
            DateOfServiceProvisionSort = sortOrder == SortState.DateOfServiceProvisionAsc ? SortState.DateOfServiceProvisionDesc : SortState.DateOfServiceProvisionAsc;
            Current = sortOrder;
        }
    }
}
