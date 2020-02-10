using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hospital.Models;
using System.ComponentModel.DataAnnotations;

namespace Hospital.ViewModels.ServisesViewModels
{
    public class ServiseViewModel
    {
        [Display(Name = "Код услуги")]
        public int ServisesId { get; set; }
        [Display(Name = "Вид услуги")]
        public string NameType { get; set; }
        [Display(Name = "Стоимость услуги")]
        public int PriceService { get; set; }
        [Display(Name = "Дата оказания услуги")]
        [DataType(DataType.Date)]
        public DateTime DateOfServiceProvision { get; set; }
        [Display(Name = "Дата задания стоимости")]
        [DataType(DataType.Date)]
        public DateTime DatePrice { get; set; }
    }
}
