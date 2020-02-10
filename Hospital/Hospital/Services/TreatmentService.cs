using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Hospital.Models;
using Hospital.ViewModels;
using Hospital.ViewModels.TreatmentsViewModels;

namespace Hospital.Services
{
    public class TreatmentService
    {
        private HospitalContext context;
        private static string lastkey = "";
        private IMemoryCache cache;
        private string myKey;
        public TreatmentService(HospitalContext context,IMemoryCache memoryCache)
        {
            this.context = context;
            cache = memoryCache;
        }

        public TreatmentsViewModel GetTreatments(int? department,int? pacient,int? doctor, string diagnosis,string receiptDate,string dischargeDate, int page, SortState sortOrder, string cacheKey)
        {
            DateTime receipt, discharge;
            TreatmentsViewModel treatments = null;
            myKey = myKey + doctor + pacient + department + diagnosis + receiptDate + dischargeDate + page + sortOrder;
            if(cacheKey!="Treatment")
            {
                if (lastkey != myKey)
                {
                    cache.Remove("Treatment");
                    cacheKey = "Treatment";
                }
                else cacheKey = "NoCache";
            }
            lastkey = myKey;
            if(!cache.TryGetValue(cacheKey,out treatments))
            {
                TreatmentViewModel _treatment = new TreatmentViewModel
                {
                    NameDepartment="",
                    DoctorSurname="",
                    PacientSurname=""
                };
                try
                {
                    receipt = Convert.ToDateTime(receiptDate);
                }
                catch
                {
                    receipt = Convert.ToDateTime("01.01.1999");
                }
                try
                {
                    discharge = Convert.ToDateTime(dischargeDate);
                }
                catch
                {
                    discharge = Convert.ToDateTime("10.10.2024");
                }
                int pageSize = 10;
                IQueryable<Treatment> source = context.Treatment.Include(p => p.Department).Include(p => p.Doctors).Include(p => p.Pacients);
                if (department != null && department != 0)
                    source = source.Where(p => p.DepartmentId == department);
                if (doctor != null && doctor != 0)
                    source = source.Where(p => p.DoctorsId == doctor);
                if (pacient != null && pacient != 0)
                    source = source.Where(p=>p.PacientsId==pacient);
                if (receiptDate != null || dischargeDate != null)
                    source = source.Where(p => p.ReceiptDate >= receipt && p.DischargeDate <= discharge);
                if(!String.IsNullOrEmpty(diagnosis))
                {
                    source = source.Where(p => p.Diagnosis.Contains(diagnosis));
                }

                switch (sortOrder)
                {
                    case SortState.TreatmentIdDesc:
                        source = source.OrderByDescending(s => s.DoctorsId);
                        break;
                    case SortState.DiagnosisAsc:
                        source = source.OrderBy(s => s.Diagnosis);
                        break;
                    case SortState.DiagnosisDesc:
                        source = source.OrderByDescending(s => s.Diagnosis);
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
                    case SortState.NameDepartmentAsc:
                        source = source.OrderBy(s => s.Department.NameDepartments);
                        break;
                    case SortState.NameDepartmentDesc:
                        source = source.OrderByDescending(s => s.Department.NameDepartments);
                        break;
                    case SortState.ReceiptDateAsc:
                        source = source.OrderBy(s => s.ReceiptDate);
                        break;
                    case SortState.ReceiptDateDesc:
                        source = source.OrderByDescending(s => s.ReceiptDate);
                        break;
                    case SortState.DischargeDateAsc:
                        source = source.OrderBy(s => s.DischargeDate);
                        break;
                    case SortState.DischargeDateDesc:
                        source = source.OrderByDescending(s => s.DischargeDate);
                        break;
                    default:
                        source = source.OrderBy(s => s.TreatmentId);
                        break;
                }
                var count = source.Count();
                var items = source.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
                treatments = new TreatmentsViewModel
                {
                    Treatments=items,
                    TreatmentViewModel=_treatment,
                    PageViewModel=pageViewModel,
                    SortViewModel = new TreatmentSortViewModel(sortOrder),
                    FilterViewModel=new TreatmentFilterViewModel(context.Doctors.ToList(), context.Pacients.ToList(),context.Departments.ToList(),diagnosis,doctor,pacient,department,receipt,discharge)


                };
                if(treatments !=null)
                {
                    cache.Set("Treatment", treatments,
                        new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(6)));
                }
            }
            return treatments;
        }
    }
}
