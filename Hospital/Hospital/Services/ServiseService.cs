using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.ServisesViewModels;

namespace Hospital.Services
{
    public class ServiseService
    {
        private HospitalContext context;
        private static string lastkey = "";
        private IMemoryCache cache;
        private string myKey;
        public ServiseService(HospitalContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            cache = memoryCache;
        }

        public ServisesViewModel GetServises(int? provision, string nameType, int priceService, string datePrice, int page, SortState sortOrder, string cacheKey)
        {
            DateTime price;
            ServisesViewModel servises = null;
            myKey = myKey + provision + nameType + datePrice + priceService + page + sortOrder;
            if (cacheKey != "Cache")
            {
                if (lastkey != myKey)
                {
                    cache.Remove("Cache");
                    cacheKey = "Cache";
                }
                else cacheKey = "NoCache";
            }
            lastkey = myKey;
            if (!cache.TryGetValue(cacheKey, out servises))
            {
                ServiseViewModel _servise = new ServiseViewModel
                {

                    DateOfServiceProvision = Convert.ToDateTime("01.01.2000")
                };
                try
                {
                    price = Convert.ToDateTime(datePrice);
                }
                catch
                {
                    price = Convert.ToDateTime("01.01.2000");
                }
                    int pageSize = 10;
                    var serviseContext = context.Servises;
                    IQueryable<Servise> source = context.Servises;
                    if (!String.IsNullOrEmpty(nameType))
                    {
                        source = source.Where(p => p.NameType.Contains(nameType));
                    }
                    switch (sortOrder)
                    {
                        case SortState.ServisesIdDesc:
                            source = source.OrderByDescending(s => s.ServisesId);
                            break;
                        case SortState.NameTypeAsc:
                            source = source.OrderBy(s => s.NameType);
                            break;
                        case SortState.NameTypeDesc:
                            source = source.OrderByDescending(s => s.NameType);
                            break;
                        case SortState.PriceServiceAsc:
                            source = source.OrderBy(s => s.PriceService);
                            break;
                        case SortState.PriceServiceDesc:
                            source = source.OrderByDescending(s => s.PriceService);
                            break;
                        case SortState.DateOfServiceProvisionAsc:
                            source = source.OrderBy(s => s.Provision.DateOfServiceProvision);
                            break;
                        case SortState.DateOfServiceProvisionDesc:
                            source = source.OrderByDescending(s => s.Provision.DateOfServiceProvision);
                            break;
                        case SortState.DatePriceAsc:
                            source = source.OrderBy(s => s.DataPrice);
                            break;
                        case SortState.DatePriceDesc:
                            source = source.OrderByDescending(s => s.DataPrice);
                            break;
                        default:
                            source = source.OrderBy(s => s.ServisesId);
                            break;
                    }
                    var count = source.Count();
                    var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                    PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                    servises = new ServisesViewModel
                    {
                        Servises = items,
                        PageViewModel = pageViewModel,
                        SortViewModel = new ServiseSortViewModel(sortOrder),
                        FilterViewModel = new ServiseFilterViewModel(context.ProvisionOfPaidServices.ToList(), nameType, priceService, provision, price),


                    };
                    if (servises != null)
                    {
                        cache.Set("Cache", servises,
                            new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(6)));
                    }
                }
                    return servises;
                        
        }
    }
}
