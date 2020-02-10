using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class ProvisionOfPaidService
    {
        public ProvisionOfPaidService()
        {
            Servises = new HashSet<Servise>();
        }
        [Key]
        [Display(Name = "Код оказания услуги")]
        public int ProvisionId { get; set; }
        [Display(Name = "Код доктора")]
        public int? DoctorsId { get; set; }
        [Display(Name = "Код пациента")]
        public int? PacientsId { get; set; }
        [Display(Name = "Дата")]
        public DateTime DateOfServiceProvision { get; set; }

        public Doctor Doctors { get; set; }
        public Pacient Pacients { get; set; }
        public ICollection<Servise> Servises { get; set; }
    }
}
