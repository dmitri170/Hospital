using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.TreatmentsViewModels
{
    public class TreatmentFilterViewModel
    {
        public TreatmentFilterViewModel(List<Doctor> doctors, List<Pacient> pacients, List<Department> departments, string diagnosis, int? doctor, int? pacient, int? department, DateTime receiptDate, DateTime dischargeDate)
        {
            doctors.Insert(0, new Doctor { DoctorsId = 0, DoctorSurnames = "Все" });
            pacients.Insert(0, new Pacient { PacientsId = 0, PatientSurnames = "Все" });
            departments.Insert(0, new Department { DepartmentId = 0, NameDepartments = "Все" });
            Doctors = new SelectList(doctors, "DoctorsId", "DoctorSurnames", doctor);
            Pacients = new SelectList(pacients, "PacientsId", "PatientSurnames", pacient);
            Departments = new SelectList(departments, "DepartmentId", "NameDepartments", department);
            SelectDiagnosis = diagnosis;
            SelectedReceiptDate = receiptDate;
            SelectedDischargeDate = dischargeDate;
        }

        public SelectList Doctors { get; private set; }
        public SelectList Pacients { get; private set; }
        public SelectList Departments { get; private set; }
        public string SelectDiagnosis { get; private set; }
        public int? SelectedDoctor { get; private set; }
        public int? SelectedPacient { get; private set; }
        public int? SelectedDepartment { get; private set; }
        public DateTime SelectedReceiptDate { get; private set; }
        public DateTime SelectedDischargeDate { get; private set; }

    }
}
