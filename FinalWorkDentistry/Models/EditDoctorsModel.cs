using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using FinalWorkDentistry.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using FinalWorkDentistry.Domains;
using System.Linq;

namespace FinalWorkDentistry.Models
{
    public class EditDoctorsModel
    {
        // для загрузки картинок с клиента на сервер!
        public IFormFile ImageFile { get; set; }

        public void ApplyCategory(

           IRepository<CategoryDoctor> categoryRepository)
        {
            var categories = categoryRepository.GetList().ToList();

            CreateSelectListItems(categories);
        }
        public Doctor AsDoctor(
     IRepository<CategoryDoctor> categoryRepository)
        {
            return new Doctor
            {
                Description = this.Description,
                Job = this.Job,
                ImageUrl = this.ImageUrl,
                Name = this.Name,
                
                DoctorId = this.DoctorId,
               
                DoctorCategory = categoryRepository.Read(SelectedCategoryId)
            };
        }

        public string UrlReturn { get; set; }

        [Display(Name = "Категория")]
        public long SelectedCategoryId { get; set; }

       

        public List<SelectListItem> ListCategories { get; set; }

      


        [Display(Name = "Категория")]
        public string CategoryDoctor { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long DoctorId { get; set; }

        [Display(Name = "Ф.И.О.")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        public string Description { get; set; }
        [Display(Name = "Должность")]
        public string Job { get; set; }

     

        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }

        public EditDoctorsModel()
        { }

        public EditDoctorsModel(
           List<CategoryDoctor> categories)
        {
            CreateSelectListItems(categories);
        }

        public EditDoctorsModel(
            Doctor entity,
            List<CategoryDoctor> categories)
        {
            DoctorId = entity.DoctorId;

            SelectedCategoryId = entity.DoctorCategory.CategoryDoctorId;

            Name = entity.Name;
            Description = entity.Description;
            Job = entity.Job;

            ImageUrl = entity.ImageUrl;

            CategoryDoctor = entity.DoctorCategory.Name;

            CreateSelectListItems(categories);
        }

        private void CreateSelectListItems(
            List<CategoryDoctor> categories)
        {
            ListCategories = categories
                .Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.CategoryDoctorId.ToString()
                })
                .ToList();
          
        }

       
    }
}
