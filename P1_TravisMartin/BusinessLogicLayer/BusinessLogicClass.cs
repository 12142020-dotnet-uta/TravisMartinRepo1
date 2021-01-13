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

        private readonly GameStopRepository _repository; // global variable to access repository layer
        private readonly MapperClass _mapperClass; // gloable variable to access mapper layer
        private readonly ILogger<BusinessLogicClass> _logger;

        /// <summary>
        /// Dependency injection for global variables
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapperClass"></param>
        /// <param name="logger"></param>
        public BusinessLogicClass(GameStopRepository repository, MapperClass mapperClass, ILogger<BusinessLogicClass> logger)
        {
            _repository = repository;
            _mapperClass = mapperClass;
            _logger = logger;
        }

        /// <summary>
        /// Registers new customer
        /// </summary>
        /// <param name="registrationViewModel"></param>
        /// <returns></returns>
        public Customer RegisterCustomer(RegistrationViewModel registrationViewModel)
        {
            // taking in customer input to register new customer
            Customer customer = new Customer()
            {
                CustomerId = registrationViewModel.CustomerId,
                FName = registrationViewModel.FName,
                LName = registrationViewModel.LName,
                Email = registrationViewModel.Email,
                UserName = registrationViewModel.UserName,
                Password = registrationViewModel.Password
            };

            // checks to see if customer already exists in database
            Customer customer1 = _repository.ValidateCustomer(customer);
            CustomerViewModel customerViewModel = new CustomerViewModel();

             return customer1;
        }

        /// <summary>
        /// Login existing customer
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        public Customer LoginCustomer(LoginViewModel loginViewModel)
        {
            // taking in customer input to login customer
            Customer customer = new Customer()
            {
                CustomerId = loginViewModel.CustomerId,
                UserName = loginViewModel.UserName,
                Password = loginViewModel.Password
            };

            Customer customer1 = _repository.ValidateLogin(customer);

            return customer1;
        }

        /// <summary>
        /// Gets the Product List from the repository layer, converts it to ProductViewModel List, and returns converted list
        /// </summary>
        /// <param name="storeViewModel"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Takes user email from user, passes it to repository layer, converts returned Customer List to CustomerViewModel List, 
        /// and returns converted list with searched for user
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        public List<CustomerViewModel> SearchCustomers(CustomerViewModel customerViewModel)
        {
            List<Customer> customerList = _repository.SearchCustomers(customerViewModel);
            List<CustomerViewModel> customerViewModelList = new List<CustomerViewModel>();
            foreach(Customer c in customerList)
            {
                customerViewModelList.Add(_mapperClass.ConvertCustomerToCustomerViewModel(c));
            }
            return customerViewModelList;
        }

        /// <summary>
        /// Calls methods from repsoitory layer to add products and store locations to their respective tables in the database
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Calls repository layer to add product to cart
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        public List<ProductViewModel> AddToCart(ProductViewModel productViewModel)
        {
            List<Product> cart = _repository.AddToCart(productViewModel);

            List<ProductViewModel> cartList = new List<ProductViewModel>();
            foreach (Product p in cart)
            {
                cartList.Add(_mapperClass.ConvertProductToProductViewModel(p));
            }
            return cartList;
        }

        /// <summary>
        /// Calls repository layer to populate Order table with a new order after checkout
        /// </summary>
        /// <param name="custName"></param>
        /// <param name="storeLocation"></param>
        /// <param name="productViewModel"></param>
        public void OrderHistory(string custName, string storeLocation, ProductViewModel productViewModel)
        {
            _repository.OrderHistory(custName, storeLocation, productViewModel);
        }

        /// <summary>
        /// Calls repository layer to query Order table for all orders by current user
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public List<Order> CustomerOrderHistory(string custId)
        {
            List<Order> orderList = _repository.CustomerOrderHistory(custId);
            /*List<OrderViewModel> orderViewModelList = new List<OrderViewModel>();
            foreach (Order o in orderList)
            {
                orderViewModelList.Add(ConvertOrderToOrderViewModel(o));
            }*/
            return orderList;
        }

        /// <summary>
        /// Calls repository layer to query Order table for all orders at specified location
        /// </summary>
        /// <param name="storeViewModel"></param>
        /// <returns></returns>
        public List<Order> StoreOrderHistory(StoreViewModel storeViewModel)
        {
            List<Order> orderList = _repository.StoreOrderHistory(storeViewModel);
            return orderList;
        }

        /*internal OrderViewModel ConvertOrderToOrderViewModel(Order order)
        {
            OrderViewModel orderViewModel = new OrderViewModel()
            {
                Customers = order.Customers,
                StoreLocations = order.StoreLocations,

            }
        }*/

        /// <summary>
        /// Takes in StoreLocation and converts to StoreViewModel
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Takes in StoreLocation and converts to StoreViewMdoel. 
        /// Made second method to avoid infinite loop
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        internal StoreViewModel ConvertStoreLocationToStoreViewModel2(StoreLocation s)
        {

            StoreViewModel storeViewModel = new StoreViewModel()
            {
                LocationId = s.LocationId,
                Location = s.Location
            };

            return storeViewModel;
        }

        /// <summary>
        /// Takes in Inventory, Product, and StoreLocation to convert Inventory to InventoryViewModel.
        /// Calls second ConvertStoreLocationToStoreViewModel method to avoid infinite loop
        /// </summary>
        /// <param name="i"></param>
        /// <param name="p"></param>
        /// <param name="s"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Shortens the Inventory list to inventories at specified store location
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
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
