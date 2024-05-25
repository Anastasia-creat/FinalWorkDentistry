using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Data.Migrations;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinalWorkDentistry.Controllers
{
    [Authorize(Roles = "Administrator,ContentManager")]
    public class ContentManagerController : Controller
    {
        private readonly IRepository<Doctor> _repositoryDoctors;
        private readonly IRepository<Service> _repositoryService;
        private readonly IRepository<CategoryDoctor> _repositoryCategoryDoc;
        private readonly IRepository<CategoryService> _repositoryCategoryServ;

        public ContentManagerController(IRepository<Doctor> repositoryDoctors, IRepository<Service> repositoryService,
            IRepository<CategoryDoctor> repositoryCategoryDoc, IRepository<CategoryService> repositoryCategoryServ)
        {
            _repositoryDoctors = repositoryDoctors;
            _repositoryService = repositoryService;
            _repositoryCategoryDoc = repositoryCategoryDoc;
            _repositoryCategoryServ = repositoryCategoryServ;
        }

        public IActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// КАТЕГОРИИ ВРАЧЕЙ ПОЛНОСТЬЮ РАБОТАЮТ
        /// </summary>
        /// <returns></returns>
  
        public IActionResult EditCategoriesDoctor()
        {
            var categories = _repositoryCategoryDoc.GetList();
            return View(categories);
        }

        public IActionResult AddCategoryDoctor()
        {
            var category = new CategoryDoctor();
            return View("EditCategoryDoctor", category);
        }

        public IActionResult DeleteCategoryDoctor(long id)
        {
            var category = _repositoryCategoryDoc.ReadWithRelations(id);
            if (category.DoctorOfCategory.Count() > 0)
            {
                ViewBag.ErrorMessage = "Нельзя удалять категорию с товарами!";
                return View("Error");
            }
            else
            {
                _repositoryCategoryDoc.Delete(id);
            }

            return RedirectToAction("Index");
        }

        public IActionResult EditCategoryDoctor(long id)
        {
            var category = _repositoryCategoryDoc.Read(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult EditCategoryDoctor(CategoryDoctor model)
        {
            if (ModelState.IsValid)
            {
                if (model.CategoryDoctorId == 0)
                {
                    _repositoryCategoryDoc.Create(model);
                }
                else
                {
                    _repositoryCategoryDoc.Update(model);
                }
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Врачи---------------------------------------------------------------------


        public IActionResult AddDoctor()
        {
            var categories = _repositoryCategoryDoc.GetList();
            var model = new EditDoctorsModel(
                categories.ToList());

            model.UrlReturn = "Index";

            return View("EditDoctorView", model);
        }

        public IActionResult EditDoctorView(long id, string urlReturn)
        {
            var entity = _repositoryDoctors.Read(id);
            var categories = _repositoryCategoryDoc.GetList();

            var model = new EditDoctorsModel(
                entity,
                categories.ToList());

            model.UrlReturn = urlReturn;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditDoctorView(EditDoctorsModel editModel)
        {
            if (ModelState.IsValid)
            {
                UploadImage(editModel);
                var model = editModel.AsDoctor(_repositoryCategoryDoc);

                if (editModel.DoctorId == 0)
                {
                    _repositoryDoctors.Create(model);
                }
                else
                {
                    _repositoryDoctors.Update(model);
                }

                string urlReturn = editModel.UrlReturn;
                return new RedirectResult(urlReturn);

            }
            return View(editModel);
        }


        public IActionResult DeleteDoctor(long id)
        {
            var doc = _repositoryDoctors.GetList();

            if (doc != null)
            {
                _repositoryDoctors.Delete(id);
            }


            return RedirectToAction("ListView", "Doctor");

        }




        public IActionResult AddService()
        {
            var categories = _repositoryCategoryServ.GetList();
            var model = new EditServicesModel(
                categories.ToList());

            model.UrlReturn = "Index";

            return View("EditServiceView", model);
        }

        public IActionResult EditServiceView(long id, string urlReturn)
        {
            var entity = _repositoryService.Read(id);
            var categories = _repositoryCategoryServ.GetList();

            var model = new EditServicesModel(
                entity,
                categories.ToList());

            model.UrlReturn = urlReturn;

            return View(model);
        }

        [HttpPost]
        public IActionResult EditServiceView(EditServicesModel editModel)
        {
            if (ModelState.IsValid)
            {
              // UploadImage(editModel);
                var model = editModel.AsService(_repositoryCategoryServ);

                if (editModel.ServiceId == 0)
                {
                    _repositoryService.Create(model);
                }
                else
                {
                    _repositoryService.Update(model);
                }

                string urlReturn = editModel.UrlReturn;
                return new RedirectResult(urlReturn);

            }
            return View(editModel);
        }





        private void UploadImage(EditDoctorsModel editModel)
        {

            if (editModel.ImageFile == null)
            {
                Debug.WriteLine("картинка не найдена");
                return;
            }

            string ext = Path.GetExtension(editModel.ImageFile.FileName);
            string uniqueName = Guid.NewGuid().ToString() + ext;
            string filename = Path.Combine(
                Directory.GetCurrentDirectory(),
                @"wwwroot\DoctorsImages",
                uniqueName);

            // сохраняем физический файл на сервер
            using (var stream = System.IO.File.Create(filename))
            {
                editModel.ImageFile.CopyTo(stream);
            }

            // в БД заменить на новое имя файла
            editModel.ImageUrl = uniqueName;


        }
    }
}
