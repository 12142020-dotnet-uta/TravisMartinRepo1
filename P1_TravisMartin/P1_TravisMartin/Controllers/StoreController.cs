using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using ModelLayer.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using ModelLayer.Models;

namespace P1_TravisMartin.Controllers
{
    public class StoreController : Controller
    {
        public const string SessionKeyStoreId = "_StoreLocationId";
        public const string SessionKeyStore = "_StoreLocation";
        public const string SessionKeyCartId = "_CartId";
        public const string SessionKeyCustId = "_CustId";
        public const string SessionKeyCustName = "_UserName";

        public List<ProductViewModel> listOfProducts = new List<ProductViewModel>();

        private BusinessLogicClass _businessLogicClass;
        private readonly ILogger<StoreController> _logger;
        public StoreController(BusinessLogicClass businessLogicClass, ILogger<StoreController> logger)
        {
            _businessLogicClass = businessLogicClass;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: LoginController
        public ActionResult Login()
        {
            return View();
        }

        [ActionName("LoginCustomer")]
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            if (ModelState.IsValid)
            {
                Customer customer = _businessLogicClass.LoginCustomer(loginViewModel);
                if (customer != null)
                {
                    HttpContext.Session.SetString(SessionKeyCustName, loginViewModel.UserName);
                    HttpContext.Session.SetString(SessionKeyCustId, loginViewModel.CustomerId.ToString());
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination!");
                    return View("Login");
                }
            }
            else
            {
                return View(loginViewModel);
            }

        }

        // GET: RegistrationController
        // [ActionName("Register")]
        public ActionResult Registration()
        {
            return View("Registration");
        }

        [ActionName("RegisterCustomer")]
        public ActionResult Registration(RegistrationViewModel registrationViewModel)
        {
            // this is for displaying customer details
            // CustomerViewModel customerViewModel = _businessLogicClass.RegisterCustomer(registrationViewModel);
            if (ModelState.IsValid)
            {
                Customer customer = _businessLogicClass.RegisterCustomer(registrationViewModel);
                if (customer != null)
                {
                    // TODO: make a reigstration complete view with a link to login and return that 
                    return View("RegistrationComplete");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Email or Username already exists in the database!");
                    return View("Registration");
                }
            }
            else
            {
                return View(registrationViewModel);
            }
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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
            HttpContext.Session.SetString(SessionKeyStore, storeViewModel.Location);
            HttpContext.Session.SetString(SessionKeyStoreId, storeViewModel.LocationId.ToString());
            List<ProductViewModel> productsList = _businessLogicClass.ProductsList(storeViewModel);
            //ViewBag.Message($"Welcome to the {storeViewModel.Location} GameStop!");
            return View("/Views/Products/Products.cshtml", productsList);
        }

        public ActionResult Cart(ProductViewModel productViewModel)
        {
            //List<OrderViewModel> ordersList = _businessLogicClass.OrdersList();

            //Guid customerId = Guid.Parse(HttpContext.Session.GetString(SessionKeyCustId));
            //Guid storeId = Guid.Parse(HttpContext.Session.GetString(SessionKeyStoreId));
            //List <CartViewModel> cart = new List<CartViewModel>();
            //cart = _businessLogicClass.AddToCart(customerId, storeId, productViewModel);
            ProductViewModel product = new ProductViewModel();
            product.ProductId = productViewModel.ProductId;
            listOfProducts = _businessLogicClass.AddToCart(product);
            return View(listOfProducts[0]);
        }

        public ActionResult Checkout(ProductViewModel productViewModel)
        {
            
            string custName = HttpContext.Session.GetString(SessionKeyCustName);
            string storeLocation = HttpContext.Session.GetString(SessionKeyStore);
            _businessLogicClass.OrderHistory(custName, storeLocation, productViewModel);
            return RedirectToAction("Index");
        }

        public ActionResult CustomerOrderHistory()
        {

            string custName = HttpContext.Session.GetString(SessionKeyCustName);
            //Guid storeId = Guid.Parse(HttpContext.Session.GetString(SessionKeyStoreId));
            List<Order> custOrderList = _businessLogicClass.CustomerOrderHistory(custName);
            return View(custOrderList);
        }

        public ActionResult ChooseStore()
        {
            StoreViewModel storeViewModel = new StoreViewModel();
            storeViewModel.storesList = _businessLogicClass.StoresList();
            return View("ChooseStore", storeViewModel);
        }
        public ActionResult StoreOrderHistory(StoreViewModel storeViewModel)
        {

            List<Order> storeOrderList = _businessLogicClass.StoreOrderHistory(storeViewModel);
            return View(storeOrderList);
        }

    }
}
