using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Models
{
    public class ServicesBriefModel
    {
        [HiddenInput(DisplayValue = false)]
        public long ServiceId { get; set; }

        [Display(Name = "Наименование")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Display(Name = "Категория")]
        public string CategoryService { get; set; }

        public ServicesBriefModel()
        { }
        public ServicesBriefModel(Service entity)
        {
            Name = entity.Name;
            Price = entity.Price;
            CategoryService = entity.ServiceCategory.Name;
            ServiceId = entity.ServiceId;
        }

    }
}
