using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models;

namespace Hospital.ViewModels.DoctorsViewModels
{
    public class DoctorFilterViewModel
    {
        public DoctorFilterViewModel(List<Department> departments, int? department,string doctorSurname,string doctorName,string specialties,string categories)
        {
            departments.Insert(0, new Department { DepartmentId = 0, NameDepartments = "Все" });
            Departments = new SelectList(departments, "DepartmentId", "NameDepartments", department);
            SelectedDepartment = department;
            SelectDoctorSurname = doctorSurname;
            SelectDoctorName = doctorName;
            SelectSpecialties = specialties;
            SelectCategories = categories;
        }
        public SelectList Departments { get; private set; }
        public string SelectDoctorSurname { get; private set; }
        public string SelectDoctorName { get; private set; }
        public string SelectSpecialties { get; private set; }
        public string SelectCategories { get; private set; }
        public int? SelectedDepartment { get; private set; }
    }
}
