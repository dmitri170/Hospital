using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels.DoctorsViewModels
{
    public class DoctorViewModel
    {
        [Display(Name = "Код доктора")]
        public int DoctorsId { get; set; }

        [Display(Name = "Название отделения")]
        public string NameDepartments { get; set; }

        [Display(Name = "Фамилия доктора")]
        public string Surname { get; set; }

        [Display(Name = "Имя доктора")]

        public string Name { get; set; }

        [Display(Name = "Специальность")]
        public string Specialties { get; set; }


        [Display(Name = "Категория")]
        public string Categories { get; set; }
    }
}
