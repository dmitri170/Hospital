using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Treatment
    {
        [Key]
        [Display(Name = "Код лечения")]
        public int TreatmentId { get; set; }
        [Display(Name = "Диагноз")]
        public string Diagnosis { get; set; }
        [Display(Name = "Код Доктора")]
        public int? DoctorsId { get; set; }
        [Display(Name = "Код Пациента")]
        public int? PacientsId { get; set; }
        [Display(Name = "Код Отделения")]
        public int? DepartmentId { get; set; }
        [Display(Name = "Дата записи")]
        public DateTime ReceiptDate { get; set; }
        [Display(Name = "Дата выписки ")]
        public DateTime DischargeDate { get; set; }

        public Department Department { get; set; }
        public Doctor Doctors { get; set; }
        public Pacient Pacients { get; set; }
    }
}
