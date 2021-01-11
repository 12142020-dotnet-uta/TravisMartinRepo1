using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Web;

namespace P1_TravisMartin.Controllers
{
    public class LoginController : Controller
    {

        private BusinessLogicClass _businessLogicClass;
        private readonly ILogger<LoginController> _logger;
        public LoginController(BusinessLogicClass businessLogicClass, ILogger<LoginController> logger)
        {
            _businessLogicClass = businessLogicClass;
            _logger = logger;
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
                    //FormsAuthentication.SetAuthCookie(model.Email, false);
                    //ViewBag.Login(loginViewModel.UserName);
                    return View("/Views/Home/Index.cshtml");
                }
                else
                {
                    ModelState.AddModelError("Failure", "Wrong Username and password combination!");
                    return View("Login");
                }
            } else
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

        // GET: LoginController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginController/Create
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

        // GET: LoginController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginController/Edit/5
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

        // GET: LoginController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginController/Delete/5
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
