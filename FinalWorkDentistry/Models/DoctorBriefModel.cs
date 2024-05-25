using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;

using System.ComponentModel.DataAnnotations;

namespace FinalWorkDentistry.Models
{
    public class DoctorBriefModel
    {
        [HiddenInput(DisplayValue = false)]
        public long DoctorId { get; set; }

        [Display(Name = "ФИО")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        public string Job { get; set; }

        [Display(Name = "Категория")]
   public string CategoryDoctor { get; set; }

 // public   CategoryDoctor CategoryDoctor { get; set; }

        public string ImageUrl { get; set; }

        public DoctorBriefModel()
        {
                
        }


        public DoctorBriefModel(Doctor entity)
        {
           
            Name = entity.Name;
            Job = entity.Job;

          
            CategoryDoctor = entity.DoctorCategory.Name;
            ImageUrl = entity.ImageUrl;
            DoctorId = entity.DoctorId;


        }
    }
}
