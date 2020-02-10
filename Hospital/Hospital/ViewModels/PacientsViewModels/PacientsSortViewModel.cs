using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using Hospital.ViewModels.PacientsViewModels;

namespace Hospital.ViewModels.PacientsViewModels
{
    public class PacientsSortViewModel
    {
        public SortState PacientIdSort { get; private set; }
        public SortState PatientSurnamesSort { get; private set; }
        public SortState PatientNamesSort { get; private set; }
        public SortState NumberPalatSort { get; private set; }
        public SortState AdresSort { get; private set; }
        public SortState Current { get; private set; }

        public PacientsSortViewModel(SortState sortOrder)
        {
            PacientIdSort = sortOrder == SortState.PacietnsIdAsc ? SortState.PacietnsIdDesc : SortState.PacietnsIdAsc;
            PatientSurnamesSort = sortOrder == SortState.PatientSurnamesAsc ? SortState.PatientSurnamesDesc : SortState.PatientSurnamesAsc;
            PatientNamesSort = sortOrder == SortState.PatientNamesAsc ? SortState.PatientNamesDesc : SortState.PatientNamesAsc;
            NumberPalatSort = sortOrder == SortState.NumberPalatAsc ? SortState.NumberPalatDesc : SortState.NumberPalatAsc;
            AdresSort = sortOrder == SortState.AdresAsc ? SortState.AdresDesc : SortState.AdresDesc;
            Current = sortOrder;
        }
    }
}
