using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;

namespace Hospital.ViewModels.ServisesViewModels
{
    public class ServiseSortViewModel
    {
        public SortState ServisesIdSort { get; private set; }
        public SortState NameTypeSort { get; private set; }
        public SortState PriceServiceSort { get; private set; }
        public SortState DateOfServiceProvisionSort { get; private set; }
        public SortState DatePriceSort { get; private set; }
        public SortState Current { get; private set; }

        public ServiseSortViewModel(SortState sortOrder)
        {
            ServisesIdSort = sortOrder == SortState.ServisesIdAsc ? SortState.ServisesIdDesc : SortState.ServisesIdAsc;
            NameTypeSort = sortOrder == SortState.NameTypeAsc ? SortState.NameTypeDesc : SortState.NameTypeAsc;
            PriceServiceSort = sortOrder == SortState.PriceServiceAsc ? SortState.PriceServiceDesc : SortState.PriceServiceDesc;
            DateOfServiceProvisionSort = sortOrder == SortState.DateOfServiceProvisionAsc ? SortState.DateOfServiceProvisionDesc : SortState.DateOfServiceProvisionAsc;
            DatePriceSort = sortOrder == SortState.DatePriceAsc ? SortState.DatePriceDesc : SortState.DatePriceAsc;
            Current = sortOrder;
        }
    }
}
