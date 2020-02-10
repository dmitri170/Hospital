using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.ViewModels.PacientsViewModels
{
    public class PacientsFilterViewModel
    {
        public PacientsFilterViewModel(string patientSurnames,string patientNames, int numberPalat,string adres)
        {
            SelectedPatientSurnames = patientSurnames;
            SelectedPatientNames = patientNames;
            SelectedNumberPalat = numberPalat;
            SelectedAdres = adres;
        }
        public string SelectedPatientSurnames { get; private set; }
        public string SelectedPatientNames { get; private set; }
        public string SelectedAdres { get; private set; }
        public int SelectedNumberPalat { get; private set; }
    }
}
