using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModelLayer.Models;
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

		private readonly GameStopDBContext _dbContext;
		DbSet<Customer> customers;
		DbSet<Product> products;
		DbSet<StoreLocation> storeLocations;
		DbSet<Order> orders;
		DbSet<Inventory> inventories;

        private List<Product> productList = new List<Product>();

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

        public List<StoreLocation> StoreList()
        {
            return storeLocations.ToList();
        }

        public List<Product> ProductList()
        {
			return products.ToList();
        }

        public List<Inventory> InventoryList()
        {
            return inventories.ToList();
        }

        /// <summary>
        /// Iniializes each store location to have a full inventory of products
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

    }
}
