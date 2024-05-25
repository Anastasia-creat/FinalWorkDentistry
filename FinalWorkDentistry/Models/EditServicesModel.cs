using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Xml.Linq;
using static FinalWorkDentistry.UsersRoles.Permissions;

namespace FinalWorkDentistry.Models
{
    public class EditServicesModel
    {

        //public void ApplyCategory(IRepository<CategoryService> categoryRepository)
        //{
        //    var categories = categoryRepository.GetList().ToList();
        //    CreateSelectListItems(categories);
        //}


        public void ApplyCategory(

          IRepository<CategoryService> categoryRepository)
        {
            var categories = categoryRepository.GetList().ToList();

            CreateSelectListItems(categories);
        }

        public  Service AsService(
            IRepository<CategoryService> categoryRepository)
        {
            return new Service
            {
                Name = this.Name,
                Price = this.Price,
                ServiceId = this.ServiceId,
                ServiceCategory = categoryRepository.Read(SelectedCategoryId),
                

            };
        }

        [Display(Name = "Категория")]
        public long SelectedCategoryId { get; set; }

        public string UrlReturn { get; set; }

        public List<SelectListItem> ListCategories { get; set; }

        [Display(Name = "Категория")]
        public string CategoryService { get; set; }

        [HiddenInput(DisplayValue = false)]
        public long ServiceId { get; set; }

        [Display(Name = "Название")]
        public string Name { get; set; }

        [Display(Name = "Цена")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        public EditServicesModel()
        {

        }





        //public EditServicesModel(Service entity, List<Category> categories)
        //{
        //    ServiceId = entity.ServiceId;
        //    SelectedCategoryId = entity.ServiceCategory.CategoryId;
        //    Name = entity.Name;
        //    Category = entity.ServiceCategory.Name;
        //    Price = entity.Price;

        //    ListCategories = categories
        //        .Select(
        //        c => new SelectListItem()
        //        {
        //            Text = c.Name,
        //            Value = c.CategoryId.ToString()
        //        })
        //        .ToList();

        //}

        public EditServicesModel(
         List<CategoryService> categories)
        {
            CreateSelectListItems(categories);
        }

        public EditServicesModel(
            Service entity,
            List<CategoryService> categories)
        {
            ServiceId = entity.ServiceId;

            SelectedCategoryId = entity.ServiceCategory.CategoryServiceId;

            Name = entity.Name;
           

            CategoryService = entity.ServiceCategory.Name;

            CreateSelectListItems(categories);
        }

        private void CreateSelectListItems(
            List<CategoryService> categories
           )
        {
            ListCategories = categories
                .Select(
                c => new SelectListItem()
                {
                    Text = c.Name,
                    Value = c.CategoryServiceId.ToString()
                })
                .ToList();

        }

       

    }
}
