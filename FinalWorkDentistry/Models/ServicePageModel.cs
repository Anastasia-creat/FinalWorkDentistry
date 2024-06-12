using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Models
{
    public class ServicePageModel
    {
        /// <summary>
        /// Информация о странице с услугами
        /// </summary>
        public string CategoryName { get; set; }
        public IEnumerable<ServicesBriefModel> ServicesForPage { get; set; }

       // public IEnumerable<DoctorBriefModel> DoctorForPage { get; set; }

        public int PagesQuantity { get; set; }

        public int ActivePage { get; set; }


        [Display(Name = "ФИО")]
        public string Name { get; set; }

        //Medic new
        [Display(Name = "Категория на врачей ")]
        public string CategoryService { get; set; }


        //  public CategoryDoctor CategoryDoctor2 { get; set; }



        public string SearchString { get; set; }




        [Display(Name = "Описание")]
        public int Price { get; set; }
    }
}
