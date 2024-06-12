using Mapster;
using Microsoft.AspNetCore.Mvc;
using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Models;
using FinalWorkDentistry.Domains;


namespace FinalWorkDentistry.BlazorServices
{
    public class BlazorService
    {
        private readonly IRepository<Service> _repositoryService;
        private readonly IRepository<CategoryService> _repositoryCategory;


        public BlazorService(IRepository<Service> repository,
            IRepository<CategoryService> repositoryCategory)
        {
            _repositoryService = repository;
            _repositoryCategory = repositoryCategory;

        }

        public int CountTotalPages(string categoryName)
        {
            // 0) ищем в базе категорию по имени
            var category = _repositoryCategory
                .FindByName(categoryName);

            // 1) добавляем механизм пагинации (разбиение на страницы)

            var query = _repositoryService
                .GetList()
                .Where(p =>
                    category == null ||
                    p.ServiceCategory.CategoryServiceId == category.CategoryServiceId);

            return (int)Math
                .Ceiling(query.Count() / 10f);
        }

        public Task<List<CategoryService>> GetCategories()
        {
            return Task.FromResult(_repositoryCategory.GetList().ToList());
        }


        public List<ServicesBriefModel> GetServices(string categoryName, int page)
        {
            // 0) ищем в базе категорию по имени
            var category = _repositoryCategory
                .FindByName(categoryName);

            // 1) добавляем механизм пагинации (разбиение на страницы)

            var query = _repositoryService
                .GetList()
                .Where(p =>
                    category == null ||
                    p.ServiceCategory.CategoryServiceId == category.CategoryServiceId);

            var productsSample = query
                 .OrderBy(p => p.ServiceId)
                .Skip((page - 1) * 10)
                .Take(10)
                .Select(e => e.Adapt<ServicesBriefModel>());

            return productsSample.ToList();
        }



        public ServicesModel GetServiceDetails(long id)
        {
            var entity = _repositoryService.Read(id);
            var model = entity.Adapt<ServicesModel>();
            return model;
        }


    }
}
