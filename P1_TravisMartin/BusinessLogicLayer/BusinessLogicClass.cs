using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using RepositoryLayer;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BusinessLogicLayer
{
    public class BusinessLogicClass
    {

        private readonly GameStopRepository _repository;
        private readonly MapperClass _mapperClass;
        private readonly ILogger<BusinessLogicClass> _logger;

        public BusinessLogicClass(GameStopRepository repository, MapperClass mapperClass, ILogger<BusinessLogicClass> logger)
        {
            _repository = repository;
            _mapperClass = mapperClass;
            _logger = logger;
        }

        /// <summary>
        /// Registers new customer
        /// TODO: Might make return type CustomerViewModel if needed
        /// </summary>
        /// <param name="registrationViewModel"></param>
        /// <returns></returns>
        public Customer RegisterCustomer(RegistrationViewModel registrationViewModel)
        {
            // taking in customer input to register new customer
            Customer customer = new Customer()
            {
                FName = registrationViewModel.FName,
                LName = registrationViewModel.LName,
                Email = registrationViewModel.Email,
                UserName = registrationViewModel.UserName,
                Password = registrationViewModel.Password
            };

            // checks to see if customer already exists in database
            Customer customer1 = _repository.ValidateCustomer(customer);
            CustomerViewModel customerViewModel = new CustomerViewModel();

            // if registered customer already exists, returns null; so try/catch block will handle that case
            /*try 
            {
                // converts the new Customer object to a CustomerViewModel object
                customerViewModel = _mapperClass.ConvertCustomerToCustomerViewModel(customer1);
                return customerViewModel;
            } catch(ArgumentNullException ane)
            {
                _logger.LogInformation($"Saving a customer to the Db threw an error, {ane}");
            }*/

            // TODO: may change return type or return null
             return customer1;
        }

        public Customer LoginCustomer(LoginViewModel loginViewModel)
        {
            // taking in customer input to login customer
            Customer customer = new Customer()
            {
                UserName = loginViewModel.UserName,
                Password = loginViewModel.Password
            };

            Customer customer1 = _repository.ValidateLogin(customer);

            return customer1;
        }

        public List<ProductViewModel> ProductsList(StoreViewModel storeViewModel)
        {
            
            //call Repo method to get a List<Products>
            List<Product> productList = _repository.ProductList();

            // query inventory list for product quantity
            List<Inventory> i = new List<Inventory>();
            i = _repository.InventoryList();
            List<Inventory> shortenedInventoryList = i.Where(x => x.StoreLocationId == storeViewModel.LocationId).ToList();

            //convert that List<Player> to List<PlayerViewModel>
            List<ProductViewModel> productViewModelList = new List<ProductViewModel>();
            for (int j = 0; j < productList.Count; j++)
            {
                List<Inventory> singleInventory = shortenedInventoryList.Where(x => x.ProductId == productList.ElementAt(j).ProductId).ToList();
                productViewModelList.Add(_mapperClass.ConvertProductToProductViewModel(productList.ElementAt(j)));
                productViewModelList.ElementAt(j).ProductQuantity = singleInventory.ElementAt(0).ProductQuantity;
            }

            return productViewModelList;
        }

        public List<StoreViewModel> StoresList()
        {
            // adds products to databse if table is empty
            _repository.AddProducts();
            _repository.AddStoreLocationsAndInventory();

            //call Repo method to get a List<StoreLocations>
            List<StoreLocation> storesList = _repository.StoreList();

            //convert that List<Player> to List<PlayerViewModel>
            List<StoreViewModel> storeViewModelList = new List<StoreViewModel>();
            foreach (StoreLocation s in storesList)
            {
                storeViewModelList.Add(ConvertStoreLocationToStoreViewModel(s));
            }

            return storeViewModelList;
        }

        internal StoreViewModel ConvertStoreLocationToStoreViewModel(StoreLocation s)
        {

            StoreViewModel storeViewModel = new StoreViewModel()
            {
                LocationId = s.LocationId,
                Location = s.Location,
                StoreInventories = QueryInventoryList(s)
            };

            return storeViewModel;
        }

        internal StoreViewModel ConvertStoreLocationToStoreViewModel2(StoreLocation s)
        {

            StoreViewModel storeViewModel = new StoreViewModel()
            {
                LocationId = s.LocationId,
                Location = s.Location
            };

            return storeViewModel;
        }
        internal InventoryViewModel ConvertInventoryToInventoryViewModel(Inventory i, Product p, StoreLocation s)
        {
            InventoryViewModel inventoryViewModel = new InventoryViewModel()
            {
                InventoryId = i.InventoryId,
                ProductQuantity = i.ProductQuantity,
                StoreLocationId = ConvertStoreLocationToStoreViewModel2(s).LocationId,
                StoreLocations = ConvertStoreLocationToStoreViewModel2(s),
                ProductId = i.ProductId
                //ProductId = _mapperClass.ConvertProductToProductViewModel(p).ProductId
                //Products = _mapperClass.ConvertProductToProductViewModel(p)
            };

            return inventoryViewModel;
        }

        internal List<InventoryViewModel> QueryInventoryList(StoreLocation s)
        {
            //List<Product> productList = _repository.ProductList();
            List<Inventory> storeInventoryList = _repository.InventoryList();
            List<Inventory> shortenedInventoryList = storeInventoryList.Where(x => x.StoreLocationId == s.LocationId).ToList();
            List<InventoryViewModel> inventoryViewModel = new List<InventoryViewModel>();
            foreach (Inventory i in shortenedInventoryList)
            {
                inventoryViewModel.Add(ConvertInventoryToInventoryViewModel(i, i.Products, i.StoreLocations));
            }

            return inventoryViewModel;
        }
    }
}
