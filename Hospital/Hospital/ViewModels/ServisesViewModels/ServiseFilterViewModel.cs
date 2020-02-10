using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.ServisesViewModels
{
    public class ServiseFilterViewModel
    {
        public ServiseFilterViewModel(List<ProvisionOfPaidService> provisions, string nameType, int priceService, int? provision, DateTime datePrice)
        {
            provisions.Insert(0, new ProvisionOfPaidService { ProvisionId = 0, DateOfServiceProvision=Convert.ToDateTime("01.01.0001")});
            Provisions = new SelectList(provisions, "ProvisonId", "DateOfServiceProvision", provisions);
            SelectedDatePrice= datePrice;
            SelectedNameType = nameType;
            SelectedPriceService = priceService;

        }
        public string SelectedNameType { get; private set; }
        public int SelectedPriceService { get; private set; }
        public SelectList Provisions { get; private set; }
        public int? SelectedProvision { get; private set; }
        public DateTime SelectedDatePrice { get; private set; }
    }
}
