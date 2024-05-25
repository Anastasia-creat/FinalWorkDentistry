using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using Microsoft.AspNetCore.Mvc;

namespace FinalWorkDentistry.Components
{
    [ViewComponent]
    public class MenuCategoriesServiceViewComponent : ViewComponent
    {
        private readonly IRepository<CategoryService> repository;

        public MenuCategoriesServiceViewComponent(IRepository<CategoryService> repository)
        {
            this.repository = repository;
        }

        public IViewComponentResult Invoke()
        {
            var categoriesNameList = repository
                .GetList()
                .Select(x => x.Name);

            return View(categoriesNameList);
        }
    }
}
