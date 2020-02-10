using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.PacientsViewModels;

namespace Hospital.Services
{
    public class PacientService
    {
        private HospitalContext context;
        private static string lastKey = "";
        private IMemoryCache cache;
        private string myKey;
        public PacientService(HospitalContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            cache = memoryCache;
        }

        public PacientsViewModel GetPacients(string pacientSurname, string pacientName, int numberPalat,string adres, int page, SortState sortOrder, string cacheKey)
        {
            PacientsViewModel pacients = null;
            myKey = myKey + pacientSurname +pacientName + numberPalat+adres + page + sortOrder;
            if (cacheKey != "Pacient")
            {
                if (lastKey != myKey)
                {
                    cache.Remove("Pacient");
                    cacheKey = "Pacient";
                }
                else cacheKey = "NoCache";
            }
            lastKey = myKey;
            if (!cache.TryGetValue(cacheKey, out pacients))
            {
                int pageSize = 10;
                var pacientContext = context.Pacients;
                IQueryable<Pacient> source = context.Pacients;
                if (!String.IsNullOrEmpty(pacientName))
                {
                    source = source.Where(p => p.PatientNames.Contains(pacientName));
                }
                if (!String.IsNullOrEmpty(pacientSurname))
                {
                    source = source.Where(p => p.PatientSurnames.Contains(pacientSurname));
                }
                if (!String.IsNullOrEmpty(adres))
                {
                    source = source.Where(p => p.Adres.Contains(adres));
                }
                switch (sortOrder)
                {
                    case SortState.PacietnsIdDesc:
                        source = source.OrderByDescending(s => s.PacientsId);
                        break;
                    case SortState.PatientNamesAsc:
                        source = source.OrderBy(s => s.PatientNames);
                        break;
                    case SortState.PatientNamesDesc:
                        source = source.OrderByDescending(s => s.PatientNames);
                        break;
                    case SortState.PatientSurnamesAsc:
                        source = source.OrderBy(s => s.PatientSurnames);
                        break;
                    case SortState.PatientSurnamesDesc:
                        source = source.OrderByDescending(s => s.PatientSurnames);
                        break;
                    case SortState.NumberPalatAsc:
                        source = source.OrderBy(s => s.NumberPalat);
                        break;
                    case SortState.NumberPalatDesc:
                        source = source.OrderByDescending(s => s.NumberPalat);
                        break;
                    case SortState.AdresAsc:
                        source = source.OrderBy(s => s.Adres);
                        break;
                    case SortState.AdresDesc:
                        source = source.OrderByDescending(s => s.Adres);
                        break;
                    default:
                        source = source.OrderBy(s => s.PacientsId);
                        break;
                }
                var count = source.Count();
                var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                pacients = new PacientsViewModel
                {
                    Pacients = items,
                    PageViewModel = pageViewModel,
                    SortViewModel = new PacientsSortViewModel(sortOrder),
                    FilterViewModel = new PacientsFilterViewModel(pacientSurname,pacientName,numberPalat,adres),
                };
                if (pacients != null)
                {
                    cache.Set("Pacient", pacients,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return pacients;
        }
    }
}
