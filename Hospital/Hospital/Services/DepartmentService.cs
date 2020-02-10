using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.DepartmentsViewModels;
using Hospital.ViewModels.DepartmentViewModels;

namespace Hospital.Services
{
    public class DepartmentService
    {
        private HospitalContext context;
        private static string lastKey = "";
        private IMemoryCache cache;
        private string myKey;
        public DepartmentService(HospitalContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            cache = memoryCache;
        }

        public DepartmentsViewModel GetDepartments(string nameDepartment, int numberPlace, int page, SortState sortOrder, string cacheKey)
        {
            DepartmentsViewModel departments= null;
            myKey = myKey + nameDepartment+numberPlace + page + sortOrder;
            if (cacheKey != "Department")
            {
                if (lastKey != myKey)
                {
                    cache.Remove("Department");
                    cacheKey = "Department";
                }
                else cacheKey = "NoCache";
            }
            lastKey = myKey;
            if (!cache.TryGetValue(cacheKey, out departments))
            {
                int pageSize = 10;
                var departmentContext = context.Departments;
                IQueryable<Department> source = context.Departments;
                if (!String.IsNullOrEmpty(nameDepartment))
                {
                    source = source.Where(p => p.NameDepartments.Contains(nameDepartment));
                }
                switch (sortOrder)
                {
                    case SortState.DepartmentIdDesc:
                        source = source.OrderByDescending(s => s.DepartmentId);
                        break;
                    case SortState.NameDepartmentAsc:
                        source = source.OrderBy(s => s.NameDepartments);
                        break;
                    case SortState.NameDepartmentDesc:
                        source = source.OrderByDescending(s => s.NameDepartments);
                        break;
                    case SortState.NumberPlaceAsc:
                        source = source.OrderBy(s => s.NumberPlace);
                        break;
                    case SortState.NumberPlaceDesc:
                        source = source.OrderByDescending(s => s.NumberPlace);
                        break;
                    default:
                        source = source.OrderBy(s => s.DepartmentId);
                        break;
                }
                var count = source.Count();
                var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                departments = new DepartmentsViewModel
                {
                    Departments = items,
                    PageViewModel = pageViewModel,
                    SortViewModel = new DepartmentsSortViewModel(sortOrder),
                    FilterViewModel = new DepartmentsFilterViewModel(nameDepartment,numberPlace),
                };
                if (departments != null)
                {
                    cache.Set("Department", departments,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }
            return departments;
        }
    }
}
