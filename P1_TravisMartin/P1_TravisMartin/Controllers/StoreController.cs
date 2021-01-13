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

        /// <summary>
        /// Returns the home page view
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Returns the login page view
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Takes in login info from user, logs them in, and returns the Home page view
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns the Register new user view
        /// </summary>
        /// <returns></returns>
        public ActionResult Registration()
        {
            return View("Registration");
        }

        /// <summary>
        /// Takes in info from new user, validates it, creates new user, and returns successful creation page
        /// </summary>
        /// <param name="registrationViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Logs the current user out
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Returns the search customers view
        /// </summary>
        /// <returns></returns>
        public ActionResult SearchCustomers()
        {
            return View();
        }

        /// <summary>
        /// Takes in email to search for existing user and returns details view with details of search for user
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("ReturnCustomerSearch")]
        public ActionResult SearchCustomers(CustomerViewModel customerViewModel)
        {
            List<CustomerViewModel> listOfCustomers = _businessLogicClass.SearchCustomers(customerViewModel);
            return View(listOfCustomers[0]);
        }

        /// <summary>
        /// returns the view to take in store location choice
        /// </summary>
        /// <returns></returns>
        public ActionResult Stores()
        {
            StoreViewModel storeViewModel = new StoreViewModel();
            storeViewModel.storesList = _businessLogicClass.StoresList();
            //ViewBag.storeViewModel = new SelectList(storeViewModel.storesList, "LocationId", "Location");
            //ViewData["storeViewModel"] = new SelectList(storesList);
            return View(storeViewModel);
        }

        /// <summary>
        /// Takes in chosen Store location and returns the inventory of that location
        /// </summary>
        /// <param name="storeViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Takes in chosen product from location and returns cart page view with chosen product
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        public ActionResult Cart(ProductViewModel productViewModel)
        {
            ProductViewModel product = new ProductViewModel();
            product.ProductId = productViewModel.ProductId;
            listOfProducts = _businessLogicClass.AddToCart(product);
            if (listOfProducts != null)
            {
                try
                {
                    return View(listOfProducts[0]);
                } catch(ArgumentOutOfRangeException aore)
                {
                    _logger.LogInformation($"Trying to view empty cart threw error, {aore}");
                    return View(productViewModel);
                }
                
            } else {
                return View(productViewModel);
            }
            
        }

        /// <summary>
        /// Specfies the amount of product you want to buy, checks you out, and returns to Home page
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        public ActionResult Checkout(ProductViewModel productViewModel)
        {
            
            string custName = HttpContext.Session.GetString(SessionKeyCustName);
            string storeLocation = HttpContext.Session.GetString(SessionKeyStore);
            _businessLogicClass.OrderHistory(custName, storeLocation, productViewModel);
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Uses logged in user to search for all purchases at all locations
        /// </summary>
        /// <returns></returns>
        public ActionResult CustomerOrderHistory()
        {

            string custName = HttpContext.Session.GetString(SessionKeyCustName);
            //Guid storeId = Guid.Parse(HttpContext.Session.GetString(SessionKeyStoreId));
            List<Order> custOrderList = _businessLogicClass.CustomerOrderHistory(custName);
            return View(custOrderList);
        }

        /// <summary>
        /// Allows user to choose a store location to view order history
        /// </summary>
        /// <returns></returns>
        public ActionResult ChooseStore()
        {
            StoreViewModel storeViewModel = new StoreViewModel();
            storeViewModel.storesList = _businessLogicClass.StoresList();
            return View("ChooseStore", storeViewModel);
        }

        /// <summary>
        /// Takes in chosen store location to view all orders from all users at the location
        /// </summary>
        /// <param name="storeViewModel"></param>
        /// <returns></returns>
        public ActionResult StoreOrderHistory(StoreViewModel storeViewModel)
        {

            List<Order> storeOrderList = _businessLogicClass.StoreOrderHistory(storeViewModel);
            return View(storeOrderList);
        }

    }
}
