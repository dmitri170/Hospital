using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels.ProvisionOfPaidServicesViewModels
{
    public class ProvisionOfPaidServicesFilterViewModel
    {
        public ProvisionOfPaidServicesFilterViewModel(List<Doctor> doctors, List<Pacient> pacients, int? doctor, int? pacient, DateTime dateOfServiceProvision)
        {
            doctors.Insert(0, new Doctor { DoctorsId = 0, DoctorSurnames = "Все" });
            pacients.Insert(0, new Pacient { PacientsId = 0, PatientSurnames = "Все" });
            Doctors = new SelectList(doctors, "DoctorsId", "DoctorSurnames", doctor);
            Pacients = new SelectList(pacients, "PacientsId", "PatientSurnames", pacient);
            SelectedDoctor = doctor;
            SelectedPacient = pacient;
            SelectedDateOfServiceProvision = dateOfServiceProvision;
        }

        public SelectList Doctors { get; private set; }
        public SelectList Pacients { get; private set; }
        public int? SelectedDoctor { get; private set; }
        public int? SelectedPacient { get; private set; }
        public DateTime SelectedDateOfServiceProvision { get; private set; }
    }
}
