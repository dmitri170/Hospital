using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels.DepartmentViewModels;

namespace Hospital.ViewModels.DepartmentsViewModels
{
    public class DepartmentsViewModel
    {
        public IEnumerable<Department> Departments { get; set; }
        public Department Department { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public DepartmentsSortViewModel SortViewModel { get; set; }
        public DepartmentsFilterViewModel FilterViewModel { get; set; }
    }
}
