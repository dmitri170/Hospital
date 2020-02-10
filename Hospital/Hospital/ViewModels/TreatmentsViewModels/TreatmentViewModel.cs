using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using System.ComponentModel.DataAnnotations;

namespace Hospital.ViewModels.TreatmentsViewModels
{
    public class TreatmentViewModel
    {
        [Display(Name="Код лечения")]
        public int TreatmentId { get; set; }
        [Display(Name ="Диагноз")]
        public string Diagnosis { get; set; }
        [Display(Name = "Фамилия врача")]
        public string DoctorSurname { get; set; }
        [Display(Name = "Фамилия пациента")]
        public string PacientSurname { get; set; }
        [Display(Name ="Название отделения")]
        public string NameDepartment { get; set; }
        [Display(Name ="Дата записи")]
        [DataType(DataType.Date)]
        public DateTime ReceiptDate { get; set; }
        [Display(Name ="Дата выписки")]
        [DataType(DataType.Date)]
        public DateTime DischargeDate { get; set; }
    }
}
