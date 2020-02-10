using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.DepartmentsViewModels
{
    public class DepartmentsSortViewModel
    {
        public SortState DepartmentIdSort { get; private set; }
        public SortState NameDepartmentSort { get; private set; }
        public SortState NumberPlace { get; private set; }
        public SortState Current { get; private set; }


        public DepartmentsSortViewModel(SortState sortOrder)
        {
            DepartmentIdSort = sortOrder == SortState.DepartmentIdAsc ? SortState.DepartmentIdDesc : SortState.DepartmentIdAsc;
            NameDepartmentSort = sortOrder == SortState.NameDepartmentAsc ? SortState.NameDepartmentDesc : SortState.NameDepartmentAsc;
            NumberPlace = sortOrder == SortState.NumberPlaceAsc ? SortState.NumberPlaceDesc : SortState.NumberPlaceAsc;
            Current = sortOrder;
        }
    }
}
