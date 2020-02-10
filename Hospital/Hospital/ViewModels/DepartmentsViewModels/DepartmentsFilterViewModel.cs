using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels.DepartmentViewModels
{
    public class DepartmentsFilterViewModel
    {
        public DepartmentsFilterViewModel(string nameDepartments, int numberPlace)
        {
            SelectedNameDepartments = nameDepartments;
            SelectedNumberPlace = numberPlace;
        }
        public string SelectedNameDepartments { get; private set; }
        public int SelectedNumberPlace { get; private set; }
    }
}
