
using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static FinalWorkDentistry.UsersRoles.Permissions;

namespace FinalWorkDentistry.Models
{
    public class ServicesModel
    {
        public ServicesModel()
        { }

        [Display(Name = "Категория на услуги")]
        public string CategoryService { get; set; }


        [HiddenInput(DisplayValue = false)]
        public long ServiceId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        public ServicesModel(Service entity)
        {
            Name = entity.Name;
           
            CategoryService = entity.ServiceCategory.Name;

           Price = entity.Price;    
            ServiceId = entity.ServiceId;
        }

        //public ServicesModel(Service entity)
        //{
        //    ServiceId = entity.ServiceId;

        //    Name = entity.Name;

        //    Price = entity.Price;


        //    Category = entity.ServiceCategory.Name;
        //}

    }
}
