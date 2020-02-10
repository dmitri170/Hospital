using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Department
    {
        public Department()
        {
            Doctors = new HashSet<Doctor>();
            Treatment = new HashSet<Treatment>();
        }
        [Key]
        [Display(Name = "Код отделения")]
        public int DepartmentId { get; set; }
        [Display(Name = "Название отделения")]
        public string NameDepartments { get; set; }
        [Display(Name = "Количество мест")]
        public int NumberPlace { get; set; }

        public ICollection<Doctor> Doctors { get; set; }
        public ICollection<Treatment> Treatment { get; set; }
    }
}
