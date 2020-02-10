using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.DoctorsViewModels
{
    public class DoctorSortViewModel
    {
        public SortState DoctorsIdSort { get; private set; }
        public SortState SurnameSort { get; private set; }
        public SortState NameDepartmentrSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState SpecialtiesSort { get; private set; }
        public SortState CategoriesSort { get; private set; }
        public SortState Current { get; private set; }

        public DoctorSortViewModel(SortState sortOrder)
        {
            DoctorsIdSort = sortOrder == SortState.DoctorsIdAsc ? SortState.DoctorsIdDesc : SortState.DoctorsIdAsc;
            SurnameSort = sortOrder == SortState.DoctorSurnameAsc ? SortState.DoctorSurnameDesc : SortState.DoctorSurnameAsc;
            NameDepartmentrSort = sortOrder == SortState.NameDepartmentAsc ? SortState.NameDepartmentDesc : SortState.NameDepartmentAsc;
            NameSort= sortOrder == SortState.DoctorNameAsc ? SortState.DoctorNameDesc : SortState.DoctorNameAsc;
            SpecialtiesSort = sortOrder == SortState.SpecialtiesAsc ? SortState.SpecialtiesDesc : SortState.SpecialtiesAsc;
            CategoriesSort = sortOrder == SortState.CategoriesAsc ? SortState.CategoriesDesc : SortState.CategoriesAsc;
            Current = sortOrder;
        }
    }
}
