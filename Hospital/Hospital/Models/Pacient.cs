using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Pacient
    {
        public Pacient()
        {
            ProvisionOfPaidServices = new HashSet<ProvisionOfPaidService>();
            Treatments = new HashSet<Treatment>();
        }
        [Key]
        [Display(Name = "Код пациента")]
        public int PacientsId { get; set; }
        [Display(Name = "Фамилия пациента")]
        public string PatientSurnames { get; set; }
        [Display(Name = "Имя пациента")]
        public string PatientNames { get; set; }
        [Display(Name = "Номер палаты")]
        public int NumberPalat { get; set; }
        [Display(Name = "Адрес")]
        public string Adres { get; set; }

        public ICollection<ProvisionOfPaidService> ProvisionOfPaidServices { get; set; }
        public ICollection<Treatment> Treatments { get; set; }
    }
}

