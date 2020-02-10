using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Hospital.ViewModels.ProvisionOfPaidServicesViewModels
{
    public class ProvisionOfPaidServicesViewModel
    {
        [Display(Name = "Код оказания платной услуги")]
        public int ProvisionId { get; set; }

        [Display(Name = "Фамилия доктора")]
        public string DoctorSurnames { get; set; }

        [Display(Name = "Фамилия пациента")]
        public string PactientSurnames { get; set; }


        [Display(Name = "Дата оказания услуги")]
        [DataType(DataType.Date)]
        public DateTime DateOfServiceProvision { get; set; }
    }
}
