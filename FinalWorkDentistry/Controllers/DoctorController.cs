﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;
using System.Linq;
using Mapster;


namespace FinalWorkDentistry.Controllers
{
    [AllowAnonymous]
    public class DoctorController : Controller
    {
        private readonly int _serviceQuantityPerPage = 40;
        private readonly IRepository<Doctor> _repositoryDoctors;
        private readonly IRepository<CategoryDoctor> _repositoryCategories;

        public DoctorController(IRepository<Doctor> repositoryDoctors, IRepository<CategoryDoctor> repositoryCategories)
        {
            _repositoryDoctors = repositoryDoctors;
            _repositoryCategories = repositoryCategories;
        }

        //public IActionResult CartView(string urlReturn)
        //{
        //    ViewBag.UrlReturn = urlReturn;
        //    var cart = GetCart();
        //    return View(cart);
        //}

        //public IActionResult AddToCart(long id, string urlReturn)
        //{
        //    var doctor = _repositoryDoctors.Read(id);

        //    if (doctor == null)
        //        return View("Error");

        //    var model = doctor.Adapt<DoctorModel>();
        //    var cart = GetCart();
        //    cart.AddCoupons(model);
        //    SaveCart(cart);

        //    return new RedirectResult(urlReturn);
        //}

        //private void SaveCart(Cart cart)
        //{
        //    string json = JsonConvert.SerializeObject(cart);
        //    HttpContext.Session.SetString("Cart", json);
        //}

        //private Cart GetCart()
        //{
        //    Cart cart = null;
        //    string json = HttpContext.Session.GetString("Cart");

        //    if (string.IsNullOrEmpty(json))
        //        cart = new Cart();
        //    else
        //        cart = JsonConvert.DeserializeObject<Cart>(json);

        //    return cart;
        //}

        public IActionResult ListView(string categoryName)
        {
            var category = _repositoryCategories.FindByName(categoryName);

            var query = _repositoryDoctors.GetList()
                           .Where(p => category == null || p.DoctorCategory.CategoryDoctorId == category.CategoryDoctorId);

            var servicesSample = query.OrderBy(x => x.DoctorId)
                                   .Take(_serviceQuantityPerPage)
                                   .Select(e => e.Adapt<DoctorBriefModel>());

            var totalServicesQuantity = query.Count();

            var model = new DoctorPageModel
            {
                DoctorForPage = servicesSample,
                CategoryDoctor = categoryName
            };

            return View(model);
        }

        public IActionResult DoctorsDetailsView(long id)
        {
            var entity = _repositoryDoctors.Read(id);
            var model = entity?.Adapt<DoctorModel>();

            if (model == null)
                return View("Error");

            return View(model);
        }
    }
}
