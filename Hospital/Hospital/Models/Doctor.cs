using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Doctor
    {
        public Doctor()
        {
            ProvisionOfPaidServices = new HashSet<ProvisionOfPaidService>();
            Treatment = new HashSet<Treatment>();
        }
        [Key]
        [Display(Name = "Код доктора")]
        public int DoctorsId { get; set; }
        [Display(Name = "Код отделения")]
        public int DepartmentId { get; set; }
        [Display(Name = "Фамилия доктора")]
        public string DoctorSurnames { get; set; }
        [Display(Name = "Имя доктора")]
        public string DoctorNames { get; set; }
        [Display(Name = "Специальность")]
        public string Specialties { get; set; }
        [Display(Name = "Категория")]
        public string Categories { get; set; }

        public Department Department { get; set; }
        public ICollection<ProvisionOfPaidService> ProvisionOfPaidServices { get; set; }
        public ICollection<Treatment> Treatment { get; set; }
    }
}
