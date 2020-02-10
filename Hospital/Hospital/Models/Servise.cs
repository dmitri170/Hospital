using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hospital.Models
{
    public class Servise
    {
        [Key]
        [Display(Name = "Код услуги")]
        public int ServisesId { get; set; }
        [Display(Name = "Название ")]
        public string NameType { get; set; }
        [Display(Name = "Цена услуги")]
        public int PriceService { get; set; }
        [Display(Name = "Ключ оказания услуги")]
        public int? ProvisionId { get; set; }
        [Display(Name = "Дата цены")]
        public DateTime DataPrice { get; set; }

        public ProvisionOfPaidService Provision { get; set; }
    }
}
