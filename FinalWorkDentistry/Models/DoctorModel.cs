using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Models
{
    public class DoctorModel
    {
        [HiddenInput(DisplayValue = false)]
        public long DoctorId { get; set; }

        [Display(Name ="ФИО")]
        public string Name { get; set; }

       public IEnumerable<DoctorBriefModel> Doctors { get; set; }

     

        //Medic new
        [Display(Name = "Категория на врачей ")]
        public string CategoryDoctor { get; set; }


     




        [Display(Name = "Описание")]
        public string Description { get; set; }
        public string Job { get; set; }

        [Display(Name = "Фото")]
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
      
        public DoctorModel()
        {   }

        public DoctorModel(Doctor entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            Job = entity.Job;
            CategoryDoctor = entity.DoctorCategory.Name;
          
            ImageUrl = entity.ImageUrl;
            DoctorId = entity.DoctorId;
        
        }
    }
}
