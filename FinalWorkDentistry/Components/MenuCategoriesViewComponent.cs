using Microsoft.AspNetCore.Mvc;
using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using System.Linq;

namespace FinalWorkDentistry.Components
{
    [ViewComponent]
    public class MenuCategoriesViewComponent : ViewComponent
    {
        private readonly IRepository<CategoryDoctor> repository;

        public MenuCategoriesViewComponent(IRepository<CategoryDoctor> repository)
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
