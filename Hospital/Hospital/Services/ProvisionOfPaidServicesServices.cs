using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.ProvisionOfPaidServicesViewModels;

namespace Hospital.Services
{
    public class ProvisionOfPaidServicesServices
    {
        private HospitalContext context;
        private static string lastkey = "";
        private IMemoryCache cache;
        private string myKey;
        public ProvisionOfPaidServicesServices(HospitalContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            cache = memoryCache;
        }

        public ProvisionOfPaidServicessViewModel GetProvision(int? doctor,int? pacient, string dateOfServiceProvision, int page, SortState sortOrder, string cacheKey)
        {
            DateTime Provision;
            ProvisionOfPaidServicessViewModel provisions = null;
            myKey = myKey + doctor+pacient +dateOfServiceProvision + page + sortOrder;
            if (cacheKey != "Provision")
            {
                if (lastkey != myKey)
                {
                    cache.Remove("Provision");
                    cacheKey = "Provision";
                }
                else cacheKey = "NoCache";
            }
            lastkey = myKey;
            if (!cache.TryGetValue(cacheKey, out provisions))
            {
                ProvisionOfPaidServicesViewModel _provision = new ProvisionOfPaidServicesViewModel
                {
                    DoctorSurnames="",
                    PactientSurnames=""
                };
                try
                {
                    Provision = Convert.ToDateTime(dateOfServiceProvision);
                }
                catch
                {
                    Provision = Convert.ToDateTime("01.01.1991");
                }

                int pageSize = 10;
                IQueryable<ProvisionOfPaidService> source = context.ProvisionOfPaidServices.Include(p => p.Doctors).Include(p => p.Pacients);
                if (doctor != null && doctor != 0)
                    source = source.Where(p => p.DoctorsId == doctor);
                if (pacient != null && pacient != 0)
                    source = source.Where(p => p.PacientsId == pacient);
                switch (sortOrder)
                {
                    case SortState.ProvisionIdDesc:
                        source = source.OrderByDescending(s => s.ProvisionId);
                        break;
                    case SortState.DoctorSurnameAsc:
                        source = source.OrderBy(s => s.Doctors.DoctorSurnames);
                        break;
                    case SortState.DoctorSurnameDesc:
                        source = source.OrderByDescending(s => s.Doctors.DoctorSurnames);
                        break;
                    case SortState.PatientSurnamesAsc:
                        source = source.OrderBy(s => s.Pacients.PatientSurnames);
                        break;
                    case SortState.PatientSurnamesDesc:
                        source = source.OrderByDescending(s => s.Pacients.PatientSurnames);
                        break;
                    case SortState.DateOfServiceProvisionAsc:
                        source = source.OrderBy(s => s.DateOfServiceProvision);
                        break;
                    case SortState.DateOfServiceProvisionDesc:
                        source = source.OrderByDescending(s => s.DateOfServiceProvision);
                        break;
                    default:
                        source = source.OrderBy(s => s.ProvisionId);
                        break;
                }
                var count = source.Count();
                var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                provisions = new ProvisionOfPaidServicessViewModel
                {
                    ProvisionOfPaidServices = items,
                    ProvisionOfPaidServicesViewModel = _provision,
                    PageViewModel = pageViewModel,
                    SortViewModel = new ProvisionOfPaidServicesSortViewModel(sortOrder),
                    FilterViewModel = new ProvisionOfPaidServicesFilterViewModel(context.Doctors.ToList(),context.Pacients.ToList(),doctor,pacient,Provision)


                };
                if (provisions != null)
                {
                    cache.Set("Cache", provisions,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(6)));
                }
            }
            return provisions;
        }
    }
}
