using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class GameStopRepository
    {

		private readonly ILogger<GameStopRepository> _logger;

		private readonly GameStopDBContext _dbContext; // gloabl variable to access db context
		DbSet<Customer> customers; // global variable to access Customer table
		DbSet<Product> products; // global variable to access Product table
		DbSet<StoreLocation> storeLocations; // global variable to access StoreLocation table
		DbSet<Order> orders; // global variable to access Order table
		DbSet<Inventory> inventories; // global variable to access Inventory table

        private List<Product> productList = new List<Product>(); // contains products from Product table

        /// <summary>
        /// Has dependency injection for accessing db context variables
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="logger"></param>
		public GameStopRepository(GameStopDBContext dbContext, ILogger<GameStopRepository> logger)
		{
			_dbContext = dbContext;
			this.customers = _dbContext.customers;
			this.products = _dbContext.products;
			this.storeLocations = _dbContext.storeLocations;
			this.inventories = _dbContext.inventories;
			this.orders = _dbContext.orders;
			_logger = logger;
		}
         /// <summary>
         /// Query the Customer table to see if user already exists
         /// Checks to see if email and usernam are unique before adding new user
         /// </summary>
         /// <param name="customer"></param>
         /// <returns></returns>
		public Customer ValidateCustomer(Customer customer)
        {
			// checks if the customer is in the database
			Customer customer1 = customers.FirstOrDefault(x => x.Email == customer.Email && x.UserName == customer.UserName);

			if (customer1 == null)
            {
				customer1 = new Customer()
				{
					FName = customer.FName,
					LName = customer.LName,
					Email = customer.Email,
					UserName = customer.UserName,
					Password = customer.Password
				};

				// adds new customer to be saved to database
				customers.Add(customer1);
				// saves new custoemr to database
				try
                {
					_dbContext.SaveChanges();
				} catch (DbUpdateException dbue)
                {
					_logger.LogInformation($"Saving an empty customer to the Db threw an error, {dbue}");
				}
				

				return customer1;
            } else
            {
				return null;
            }
        }

        /// <summary>
        /// Queries the Customer table to see if uesr exists
        /// Checks username and password to see if they are in table, then allows user to login
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public Customer ValidateLogin(Customer customer)
        {
			// checks if the customer is in the database
			Customer customer1 = customers.FirstOrDefault(x =>x.UserName == customer.UserName && x.Password == customer.Password);

			if (customer1 != null)
            {
				return customer1;
            } else
            {
				return null;
            }
		}

        /// <summary>
        /// Queries Customer table with specified email to find existing user
        /// </summary>
        /// <param name="customerViewModel"></param>
        /// <returns></returns>
        public List<Customer> SearchCustomers(CustomerViewModel customerViewModel)
        {
            var custList = from c in customers
                           where c.Email == customerViewModel.Email
                           select c;

            List<Customer> searchList = custList.ToList();
            return searchList;
        }


        /// <summary>
        /// Initializes product table and adds each product to a list
        /// </summary>
        /// <returns></returns>
        public void AddProducts()
		{

			if (!products.Any())
			{
				Product p1 = new Product("Ghost of Tsushima", 39.99, "In the late 13th century, a ruthless Mongol army invades Tsushima in a quest to conquer all of japan-But for Jin Saki, one of the last surviving samurai defenders, the battle has just begun. Set aside samurai tradition and forge a new path, the path of the ghost, as you wage an unconventional war for the freedom of Japan.");
				p1.ByteArrayImage = System.IO.File.ReadAllBytes(@"./wwwroot/images/Ghost-of-Tsushima.jpg");
				products.Add(p1);
                productList.Add(p1);

				Product p2 = new Product("Assassin's Creed Valhalla", 59.99, "Become Eivor, a legendary viking warrior raised on tales of battle and glory. Explore England's dark ages as you raid your enemies, grow your settlement, and build your political power in the quest to earn a place among the gods in Valhalla.");
				p2.ByteArrayImage = System.IO.File.ReadAllBytes(@"./wwwroot/images/Assassins-Creed-Valhalla.jpg");
				products.Add(p2);
                productList.Add(p2);

                Product p3 = new Product("Cyberpunk 2077", 59.99, "Cyberpunk 2077 is an open-world action-adventure from the creaters of The Witcher 3: Wild Hunt, CD Projekt Red. Set in Night City, a megalopolis obsessed with power, glamour, and body modification, you play as V, a mercenary outlaw going after a one-of-a-kind implant that is the key to immortality. Customise your characters cyberware, skillset and playstyle, and explore a vast city where the choices you make shape the story and world around you.");
				p3.ByteArrayImage = System.IO.File.ReadAllBytes(@"./wwwroot/images/Cyberpunk-2077.jpg");
				products.Add(p3);
                productList.Add(p3);

                Product p4 = new Product("The Last of Us Part II", 29.99, "Five years after their dangerous journey across the post-pandemic United States, Ellie and Joel have settled down in Jackson, Wyoming. Living amongst a thriving community of survivors has allowed them peace and stability, despite the constant threat of the infected and other, more desperate survivors. When a violent event disrupts that peace, Ellie embarks on a relentless journey to carry out justice and find closure. As she hunts those responsible one by one, she is confronted with the devastating physical and emotional repercussions of her actions.");
				p4.ByteArrayImage = System.IO.File.ReadAllBytes(@"./wwwroot/images/The-Last-of-Us-Part-II.jpg");
				products.Add(p4);
                productList.Add(p4);

                Product p5 = new Product("Control", 29.99, "After a secretive agency in New York is invaded by an otherworldly threat, you become the new Director struggling to regain Control. From the developer Remedy Entertainment, this supernatural 3rd person action-adventure will challenge you to master the combination of supernatural abilities, modifiable loadouts, and reactive environments while fighting through a deep and unpredictable world.");
				p5.ByteArrayImage = System.IO.File.ReadAllBytes(@"./wwwroot/images/Control.jpg");
				products.Add(p5);
                productList.Add(p5);
            }

			_dbContext.SaveChanges();
		}

        /// <summary>
        /// Returns a list of StoreLocation gotten from StoreLocation table
        /// </summary>
        /// <returns></returns>
        public List<StoreLocation> StoreList()
        {
            return storeLocations.ToList();
        }

        /// <summary>
        /// Returns a list of Product gotten from Product table
        /// </summary>
        /// <returns></returns>
        public List<Product> ProductList()
        {
			return products.ToList();
        }

        /// <summary>
        /// Returns a list of Inventory gotten from Inventory table
        /// </summary>
        /// <returns></returns>
        public List<Inventory> InventoryList()
        {
            return inventories.ToList();
        }


        /// <summary>
        /// Adds Store Locations to StoreLocation table and populates Inventory table with all products from every location
        /// </summary>
        public void AddStoreLocationsAndInventory()
        {

            if (!storeLocations.Any())
            {
                StoreLocation store1 = new StoreLocation("Raleigh");
                foreach (Product product in productList)
                {
                    Inventory inventory1 = new Inventory();
                    inventory1.Products = product;
                    inventory1.ProductQuantity = 25;
                    inventory1.StoreLocations = store1;
                    inventories.Add(inventory1);
                    store1.StoreInventories.Add(inventory1);
                }
                storeLocations.Add(store1);


                StoreLocation store2 = new StoreLocation("Dubai");
                foreach (Product product in productList)
                {
                    Inventory inventory2 = new Inventory();
                    inventory2.Products = product;
                    inventory2.ProductQuantity = 25;
                    inventory2.StoreLocations = store2;
                    inventories.Add(inventory2);
                    store2.StoreInventories.Add(inventory2);
                }
                storeLocations.Add(store2);

                StoreLocation store3 = new StoreLocation("Tokyo");
                foreach (Product product in productList)
                {
                    Inventory inventory3 = new Inventory();
                    inventory3.Products = product;
                    inventory3.ProductQuantity = 25;
                    inventory3.StoreLocations = store3;
                    inventories.Add(inventory3);
                    store3.StoreInventories.Add(inventory3);
                }
                storeLocations.Add(store3);

                StoreLocation store4 = new StoreLocation("London");
                foreach (Product product in productList)
                {
                    Inventory inventory4 = new Inventory();
                    inventory4.Products = product;
                    inventory4.ProductQuantity = 25;
                    inventory4.StoreLocations = store4;
                    inventories.Add(inventory4);
                    store4.StoreInventories.Add(inventory4);
                }
                storeLocations.Add(store4);

                StoreLocation store5 = new StoreLocation("Rome");
                foreach (Product product in productList)
                {
                    Inventory inventory5 = new Inventory();
                    inventory5.Products = product;
                    inventory5.ProductQuantity = 25;
                    inventory5.StoreLocations = store5;
                    inventories.Add(inventory5);
                    store5.StoreInventories.Add(inventory5);
                }
                storeLocations.Add(store5);
            }
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Queries Product table with specified ProductId and returns a list of Product to add to cart
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        public List<Product> AddToCart(ProductViewModel productViewModel)
        {
            var cartItem =  from p in products
                            where p.ProductId == productViewModel.ProductId
                            select p;
            List<Product> cartList = cartItem.ToList();
            return cartList;
        }

        /// <summary>
        /// Takes in a Customer username, Store Location location, and ProductId in order to populate the Order table with a new order
        /// Also decrements inventory by specified product amount user bought
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="storeLocation"></param>
        /// <param name="productQuantity"></param>
        /// <param name="product"></param>
        public void OrderHistory(string custName, string storeLocation, ProductViewModel productViewModel)
        {
            var cust = from c in customers
                           where c.UserName == custName
                           select c;
            List<Customer> custList = cust.ToList();

            var store = from s in storeLocations
                       where s.Location == storeLocation
                       select s;
            List<StoreLocation> locationList = store.ToList();

            var product = from p in products
                        where p.ProductId == productViewModel.ProductId
                        select p;
            List<Product> productList = product.ToList();

            try
            {
                Order order = new Order();
                order.Customers = custList[0];
                order.StoreLocations = locationList[0];
                order.OrderQuantity = productViewModel.AmountChosen;
                order.Ordertime = DateTime.Now;
                order.TotalOrderPrice = productViewModel.AmountChosen * productList[0].ProductPrice;
                order.Products = productList[0];
                orders.Add(order);
                UpdateInventory(productList[0], locationList[0], productViewModel.AmountChosen);
                _dbContext.SaveChanges();
            } catch(ArgumentOutOfRangeException aore)
            {
                _logger.LogInformation($"Trying to purchase beyond the remaining inventory threw an error, {aore}");
            } catch(DbUpdateException dbue)
            {
                _logger.LogInformation($"Saving an empty customer to the Db threw an error, {dbue}");
            }
     
        }

        /// <summary>
        /// Decrements the inventory when a customer checks out with at least 1 product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="storeLocation"></param>
        /// <param name="productQuantity"></param>
        public void UpdateInventory(Product product, StoreLocation storeLocation, int productQuantity)
        {
            var decrementInventory = from i in inventories
                                     where i.Products == product & i.StoreLocations == storeLocation
                                     select i;

            foreach (Inventory i in decrementInventory)
            {
                i.ProductQuantity -= productQuantity;
            }

            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Takes in a CustomerID and returns all orders from the Order table by specified user
        /// </summary>
        /// <param name="custId"></param>
        /// <returns></returns>
        public List<Order> CustomerOrderHistory(string custId)
        {
            List<Customer> custList = new List<Customer>();
            var user = from c in customers
                        where c.UserName == custId
                        select c;
            custList = user.ToList();
       
            List<Order> orderList = new List<Order>();
            var customerOrders = from o in orders
                                    where o.CustomerId == custList[0].CustomerId
                                    select o;
            orderList = customerOrders.ToList();

            foreach (Order o in orderList)
            {
                var product = from p in products
                              where p.ProductId == o.ProductId
                              select p;
                o.Products = product.ToList()[0];

                var store = from s in storeLocations
                            where s.LocationId == o.StoreLocationId
                            select s;
                o.StoreLocations = store.ToList()[0];
            }
            return orderList;
        }

        /// <summary>
        /// Takes in a StoreViewModel location to query Order table for all orders at location
        /// </summary>
        /// <param name="storeViewModel"></param>
        /// <returns></returns>
        public List<Order> StoreOrderHistory(StoreViewModel storeViewModel)
        {
            List<StoreLocation> storeList = new List<StoreLocation>();
            var store = from s in storeLocations
                        where s.Location == storeViewModel.Location
                        select s;
            storeList = store.ToList();

            List<Order> orderList = new List<Order>();
            var storeOrders = from o in orders
                                 where o.StoreLocationId == storeList[0].LocationId
                                 select o;
            orderList = storeOrders.ToList();

            foreach (Order o in orderList)
            {
                var product = from p in products
                              where p.ProductId == o.ProductId
                              select p;
                o.Products = product.ToList()[0];

                var user = from c in customers
                            where c.CustomerId == o.CustomerId
                            select c;
                o.Customers = user.ToList()[0];
            }
            return orderList;
        }

    } // end of class
} // end of namespace
