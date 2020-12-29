using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TravisMartin_Project0
{
    public class GameStopRepositoryLayer : GameStore
    {
        static GameStopDBContext DBContext = new GameStopDBContext();
        DbSet<Customer> customers = DBContext.customers;
        DbSet<Product> products = DBContext.products;
        DbSet<Inventory> inventory = DBContext.inventory;
        DbSet<StoreLocation> storeLocations = DBContext.storeLocations;
        DbSet<Order> orders = DBContext.orders;

        //public SortedDictionary<Products, int> productInventory = new SortedDictionary<Products, int>();
        public List<Product> listOfProducts = new List<Product>();

        /// <summary>
        /// Initializes product table and adds each product to a list
        /// </summary>
        /// <returns></returns>
        public void ProductList() {

            if (!products.Any()) {
                Product p1 = new Product("Ps4 game: Ghost of Tsushima", 39.99, "In the late 13th century, a ruthless Mongol army invades Tsushima in a quest to conquer all of japan-But for Jin Saki, one of the last surviving samurai defenders, the battle has just begun. Set aside samurai tradition and forge a new path, the path of the ghost, as you wage an unconventional war for the freedom of Japan.");
                products.Add(p1);
                listOfProducts.Add(p1);

                Product p2 = new Product("Ps4 game: Assassin's Creed Valhalla", 59.99, "Become Eivor, a legendary viking warrior raised on tales of battle and glory. Explore England's dark ages as you raid your enemies, grow your settlement, and build your political power in the quest to earn a place among the gods in Valhalla.");
                products.Add(p2);
                listOfProducts.Add(p2);

                Product p3 = new Product("Ps4 game: Cyberpunk 2077", 59.99, "Cyberpunk 2077 is an open-world action-adventure from the creaters of The Witcher 3: Wild Hunt, CD Projekt Red. Set in Night City, a megalopolis obsessed with power, glamour, and body modification, you play as V, a mercenary outlaw going after a one-of-a-kind implant that is the key to immortality. Customise your characters cyberware, skillset and playstyle, and explore a vast city where the choices you make shape the story and world around you.");
                products.Add(p3);
                listOfProducts.Add(p3);

                Product p4 = new Product("Ps4 game: The Last of Us Part II", 29.99, "Five years after their dangerous journey across the post-pandemic United States, Ellie and Joel have settled down in Jackson, Wyoming. Living amongst a thriving community of survivors has allowed them peace and stability, despite the constant threat of the infected and other, more desperate survivors. When a violent event disrupts that peace, Ellie embarks on a relentless journey to carry out justice and find closure. As she hunts those responsible one by one, she is confronted with the devastating physical and emotional repercussions of her actions.");
                products.Add(p4);
                listOfProducts.Add(p4);

                Product p5 = new Product("Ps4 game: Control", 29.99, "After a secretive agency in New York is invaded by an otherworldly threat, you become the new Director struggling to regain Control. From the developer Remedy Entertainment, this supernatural 3rd person action-adventure will challenge you to master the combination of supernatural abilities, modifiable loadouts, and reactive environments while fighting through a deep and unpredictable world.");
                products.Add(p5);
                listOfProducts.Add(p5);
            }

            DBContext.SaveChanges();

        }

        /// <summary>
        /// Iniializes each store location to have a full inventory of products
        /// </summary>
        public void AddStoreLocationsAndInventory() {

            if (!storeLocations.Any()) {
                StoreLocation store1 = new StoreLocation("Raleigh");
                foreach(Product product in listOfProducts) {
                    Inventory inventory1 = new Inventory();
                    inventory1.Products = product;
                    inventory1.ProductQuantity = 25;
                    inventory1.StoreLocations = store1;
                    inventory.Add(inventory1);
                    store1.StoreInventories.Add(inventory1);
                }
                storeLocations.Add(store1);
                

                StoreLocation store2 = new StoreLocation("Dubai");
                foreach(Product product in listOfProducts) {
                    Inventory inventory2 = new Inventory();
                    inventory2.Products = product;
                    inventory2.ProductQuantity = 25;
                    inventory2.StoreLocations = store2;
                    inventory.Add(inventory2);
                    store2.StoreInventories.Add(inventory2);
                }
                storeLocations.Add(store2);

                StoreLocation store3 = new StoreLocation("Tokyo");
                foreach(Product product in listOfProducts) {
                    Inventory inventory3 = new Inventory();
                    inventory3.Products = product;
                    inventory3.ProductQuantity = 25;
                    inventory3.StoreLocations = store3;
                    inventory.Add(inventory3);
                    store3.StoreInventories.Add(inventory3);
                }
                storeLocations.Add(store3);

                StoreLocation store4 = new StoreLocation("London");
                foreach(Product product in listOfProducts) {
                    Inventory inventory4 = new Inventory();
                    inventory4.Products = product;
                    inventory4.ProductQuantity = 25;
                    inventory4.StoreLocations = store4;
                    inventory.Add(inventory4);
                    store4.StoreInventories.Add(inventory4);
                }
                storeLocations.Add(store4);

                StoreLocation store5 = new StoreLocation("Rome");
                foreach(Product product in listOfProducts) {
                    Inventory inventory5 = new Inventory();
                    inventory5.Products = product;
                    inventory5.ProductQuantity = 25;
                    inventory5.StoreLocations = store5;
                    inventory.Add(inventory5);
                    store5.StoreInventories.Add(inventory5);
                }
                storeLocations.Add(store5);
            }
            DBContext.SaveChanges();
        }

        /// <summary>
        /// Creates new customer, or reuses old one
        /// </summary>
        /// <param name="fName"></param>
        /// <param name="lName"></param>
        /// <returns></returns>
        public Customer CreateCustomer(string fName = "null", string lName = "null") {
            Customer shopper = new Customer();
            shopper = customers.Where(x => x.Fname == fName && x.Lname == lName).FirstOrDefault();

            if (shopper == null)
            {
                shopper = new Customer()
                {
                    Fname = fName,
                    Lname = lName
                };
                customers.Add(shopper);
                DBContext.SaveChanges();
            }
            return shopper;
        }

        /// <summary>
        /// Converts console string to int to check user choice
        /// Returns -1 if unsuccessful
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int ConvertToValidInput (string input) {
            int conversionResult;
            bool logInOrQuitBool = int.TryParse(input, out conversionResult);
            if (logInOrQuitBool == false) {
                return -1;
            } else {
                return conversionResult;
            }
        }

        /// <summary>
        /// Queries the database for specific storelocations and returns them
        /// </summary>
        /// <param name="userChoice"></param>
        /// <returns></returns>
        public StoreLocation ChooseLocation(string userChoice) {
        
            StoreLocation location = new StoreLocation();

            switch(userChoice) {
                case "1": location = storeLocations.Where(x => x.Location == "Raleigh").FirstOrDefault(); break;
                case "2": location = storeLocations.Where(x => x.Location ==  "Dubai").FirstOrDefault(); break;
                case "3": location = storeLocations.Where(x => x.Location ==  "Tokyo").FirstOrDefault(); break;
                case "4": location = storeLocations.Where(x => x.Location ==  "London").FirstOrDefault(); break;
                case "5": location = storeLocations.Where(x => x.Location ==  "Rome").FirstOrDefault(); break;
            }

            return location;
        }

        /// <summary>
        /// Takes in a customer, storeLocation, productQuantity, and product in order to populate the Order table 1 entry at a time
        /// TODO had to use productName instead of productId in Order table because I kept getting errors about duplicate primary keys in the product table
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="storeLocation"></param>
        /// <param name="productQuantity"></param>
        /// <param name="product"></param>
        public void OrderHistory(Customer customer, StoreLocation storeLocation, int productQuantity, Product product) {
            Order order = new Order();
            order.Customers = customer;
            order.StoreLocations = storeLocation;
            order.OrderQuantity = productQuantity;
            order.Ordertime = DateTime.Now;
            order.TotalOrderPrice = productQuantity * product.ProductPrice;
            order.ProductName = product.ProductName;

            orders.Add(order);
            DBContext.SaveChanges();

        }

        /// <summary>
        /// Decrements the inventory when a customer checks out with at least 1 product
        /// </summary>
        /// <param name="product"></param>
        /// <param name="storeLocation"></param>
        /// <param name="productQuantity"></param>
        public void UpdateInventory(Product product, StoreLocation storeLocation, int productQuantity) {
            var decrementInventory =    from i in inventory
                                        where i.Products == product & i.StoreLocations == storeLocation
                                        select i;
            
            foreach (Inventory i in decrementInventory) {
                i.ProductQuantity -= productQuantity;
            }

            DBContext.SaveChanges();
        } 

        /// <summary>
        /// Prints all orders in the Order table with same Customer Name
        /// </summary>
        /// <param name="customer"></param>
        public void CustomerOrderHistory(Customer customer) {
            var customerOrders =    from o in orders
                                    where o.Customers == customer
                                    select o;
            foreach (Order o in customerOrders) {
                Console.WriteLine($"\n\tCustomer name: {o.Customers.Fname}"); 
                Console.WriteLine($"\tStore location: {o.StoreLocations.Location}"); 
                Console.WriteLine($"\tOrder time: {o.Ordertime}");
                Console.WriteLine($"\tProduct name: {o.ProductName}");
                Console.WriteLine($"\tOrder quantity: {o.OrderQuantity}");
                Console.WriteLine($"\tTotal price: {o.TotalOrderPrice}");
            }
        } 

        /// <summary>
        /// Prints all orders in the Order table with the same Store Location
        /// </summary>
        /// <param name="storeLocation"></param>
        public void StoreOrderHistory(StoreLocation storeLocation) {
            var storeOrders =    from o in orders
                                    where o.StoreLocations == storeLocation
                                    select o;
            foreach (Order o in storeOrders) {
                Console.WriteLine($"\n\tCustomer name: {o.Customers.Fname}"); 
                Console.WriteLine($"\tStore location: {o.StoreLocations.Location}"); 
                Console.WriteLine($"\tOrder time: {o.Ordertime}");
                Console.WriteLine($"\tProduct name: {o.ProductName}");
                Console.WriteLine($"\tOrder quantity: {o.OrderQuantity}");
                Console.WriteLine($"\tTotal price: {o.TotalOrderPrice}");
            }
        } 

    }
}