using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ModelLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace P1_TravisMartin.Controllers
{
    public class StoreController : Controller
    {

        private BusinessLogicClass _businessLogicClass;
        private readonly ILogger<StoreController> _logger;
        public StoreController(BusinessLogicClass businessLogicClass, ILogger<StoreController> logger)
        {
            _businessLogicClass = businessLogicClass;
            _logger = logger;
        }

        public ActionResult Stores()
        {
            StoreViewModel storeViewModel = new StoreViewModel();
            storeViewModel.storesList = _businessLogicClass.StoresList();
            //ViewBag.storeViewModel = new SelectList(storeViewModel.storesList, "LocationId", "Location");
            //ViewData["storeViewModel"] = new SelectList(storesList);
            return View(storeViewModel);
        }

        [HttpPost]
        [ActionName("Stores")]
        public ActionResult Stores(StoreViewModel storeViewModel)
        {
            List<ProductViewModel> productsList = _businessLogicClass.ProductsList(storeViewModel);
            //ViewBag.Message($"Welcome to the {storeViewModel.Location} GameStop!");
            return View("/Views/Products/Products.cshtml", productsList);
        }


        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
