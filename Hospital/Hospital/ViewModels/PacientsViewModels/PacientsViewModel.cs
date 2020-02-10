using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.PacientsViewModels
{
    public class PacientsViewModel
    {
        public IEnumerable<Pacient> Pacients { get; set; }
        public Pacient Pacient { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public PacientsFilterViewModel FilterViewModel { get; set; }

        public PacientsSortViewModel SortViewModel { get; set; }
    }
}
