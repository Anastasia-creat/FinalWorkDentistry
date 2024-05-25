using FinalWorkDentistry.Domains;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Models
{
    public class DoctorPageModel
    {
        public string CategoryName { get; set; }
      //  public IEnumerable<ServicesBriefModel> ServicesForPage { get; set; }

        public IEnumerable<DoctorBriefModel> DoctorForPage { get; set; }

        public int PagesQuantity { get; set; }

        public int ActivePage { get; set; }

        [Display(Name = "ФИО")]
        public string Name { get; set; }

        //Medic new
        [Display(Name = "Категория на врачей ")]
        public string CategoryDoctor { get; set; }


      //  public CategoryDoctor CategoryDoctor2 { get; set; }








        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Должнось")]
        public string Job { get; set; }

        [Display(Name = "Фото")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}
