using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Hospital.Models;

namespace Hospital.ViewModels.ProcedureViewModels
{
    public class FilterViewModel
    {
        public SelectList Departments { get; private set; }
        public SelectList Doctors { get; private set; }
        public SelectList Pacients { get; private set; }
        public SelectList Treatment { get; private set; }
        public SelectList ProvisionOfPaidService { get; private set; }
        public SelectList Servises { get; private set; }
        public SelectList Object { get; private set; }
        public int? SelectedDepartment { get; private set; }
        public int? SelectedPacient { get; private set; }
        public FilterViewModel(List<object[]> objects)
        {
            objects.Insert(0, new object[] { "", "" });
            Object = new SelectList(objects);
        }

    }
}
