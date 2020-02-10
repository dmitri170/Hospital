using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.DoctorsViewModels;

namespace Hospital.Services
{
    public class DoctorService
    {
        private HospitalContext context;
        private static string lastkey = "";
        private IMemoryCache cache;
        private string myKey;
        public DoctorService(HospitalContext context, IMemoryCache memoryCache)
        {
            this.context = context;
            cache = memoryCache;
        }

        public DoctorsViewModel GetDoctor(int? department, string doctorSurname, string doctorName, string specialties, string categories, int page, SortState sortOrder, string cacheKey)
        {
            DoctorsViewModel doctors = null;
            myKey = myKey + doctorName + doctorSurname + specialties + categories + department + page + sortOrder;
            if (cacheKey != "Doctor")
            {
                if (lastkey != myKey)
                {
                    cache.Remove("Doctor");
                    cacheKey = "Doctor";
                }
                else cacheKey = "NoCache";
            }
            lastkey = myKey;
            if (!cache.TryGetValue(cacheKey, out doctors))
            {
                DoctorViewModel _doctor = new DoctorViewModel
                {
                    NameDepartments = ""
                };
                int pageSize = 10;
                IQueryable<Doctor> source = context.Doctors.Include(p => p.Department);
                if (department != null && department != 0)
                    source = source.Where(p => p.DepartmentId == department);
                if (!String.IsNullOrEmpty(doctorName))
                {
                    source = source.Where(p => p.DoctorNames.Contains(doctorName));
                }
                if (!String.IsNullOrEmpty(doctorSurname))
                {
                    source = source.Where(p => p.DoctorSurnames.Contains(doctorSurname));
                }
                if (!String.IsNullOrEmpty(specialties))
                {
                    source = source.Where(p => p.Specialties.Contains(specialties));
                }
                if (!String.IsNullOrEmpty(categories))
                {
                    source = source.Where(p => p.Categories.Contains(categories));
                }
                switch (sortOrder)
                {
                    case SortState.DoctorsIdDesc:
                        source = source.OrderByDescending(s => s.DoctorsId);
                        break;
                    case SortState.DoctorNameAsc:
                        source = source.OrderBy(s => s.DoctorNames);
                        break;
                    case SortState.DoctorNameDesc:
                        source = source.OrderByDescending(s => s.DoctorNames);
                        break;
                    case SortState.DoctorSurnameAsc:
                        source = source.OrderBy(s => s.DoctorSurnames);
                        break;
                    case SortState.DoctorSurnameDesc:
                        source = source.OrderByDescending(s => s.DoctorSurnames);
                        break;
                    case SortState.SpecialtiesAsc:
                        source = source.OrderBy(s => s.Specialties);
                        break;
                    case SortState.SpecialtiesDesc:
                        source = source.OrderByDescending(s => s.Specialties);
                        break;
                    case SortState.CategoriesAsc:
                        source = source.OrderBy(s => s.Categories);
                        break;
                    case SortState.CategoriesDesc:
                        source = source.OrderByDescending(s => s.Categories);
                        break;
                    case SortState.NameDepartmentAsc:
                        source = source.OrderBy(s => s.Department.NameDepartments);
                        break;
                    case SortState.NameDepartmentDesc:
                        source = source.OrderByDescending(s => s.Department.NameDepartments);
                        break;
                    default:
                        source = source.OrderBy(s => s.DoctorsId);
                        break;
                }
                var count = source.Count();
                var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                doctors = new DoctorsViewModel
                {
                    Doctors = items,
                    DoctorViewModel = _doctor,
                    PageViewModel = pageViewModel,
                    SortViewModel = new DoctorSortViewModel(sortOrder),
                    FilterViewModel = new DoctorFilterViewModel(context.Departments.ToList(), department, doctorName, doctorSurname, specialties, categories),


                };
                if (doctors != null)
                {
                    cache.Set("Cache", doctors,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(6)));
                }
            }
            return doctors;
        }
    }
}
