using System;
using Microsoft.EntityFrameworkCore;
using TravisMartin_Project0;
using Xunit;
using System.Linq;

namespace P0_Tests
{
    public class GameStoreTests
    {
        [Fact]
        /// <summary>
        /// Tests that customer is saved to the database and that a duplicate customer is not saved
        /// after calling CreateCustomer() method
        /// </summary>
        public void TestForDuplicateCustomers()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            Customer c1 = new Customer();
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                c1 = repo.CreateCustomer("Sparky", "Jones");
                //context.SaveChanges();
            }

            // assert
            using (var context1 = new GameStopDBContext(options))
            {
                //context.Database.EnsureCreated();
                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context1);

                Customer result = repo.CreateCustomer("Sparky", "Jones");

                Assert.Equal(c1.CustomerId, result.CustomerId);
                //Assert.True(p1.playerId.Equals(result.playerId));
                //Assert.True(p1.playerId.CompareTo(result.playerId) == result.playerId.CompareTo(p1.playerId));


            }
        } // end of first test case

        [Fact]
        /// <summary>
        /// Tests that an int is outputted when given a string after 
        /// calling ConvertToValidInput() method
        /// </summary>
        public void TestForIntConversion()
        {
            // arrange
            string number = "5";
            GameStopRepositoryLayer gsrl = new GameStopRepositoryLayer();

            //act
            int  convertedInt = gsrl.ConvertToValidInput(number);

            // assert
            Assert.IsType<int>(convertedInt);

        
        } // end of second test case

        [Fact]
        /// <summary>
        /// Tests that value could not be converted to int after 
        /// calling ConvertToValidInput() method
        /// </summary>
        public void TestForValidString()
        {
            // arrange
            string string1 = "dog";
            GameStopRepositoryLayer gsrl = new GameStopRepositoryLayer();

            //act
            int convertedInt = gsrl.ConvertToValidInput(string1);

            // assert
            Assert.Equal(convertedInt, -1);

        
        } // end of third test case

        [Fact]
        /// <summary>
        /// Tests that product table is filled with products after 
        /// calling AddProducts() method
        /// </summary>
        public void TestForAddingProductToTable()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                repo.AddProducts();
            }

            using (var context = new GameStopDBContext(options))
            {
                var getProducts =   from p in context.products
                                    select p;

                // assert
                Assert.True(getProducts.Any());
            }


            
        } // end of fourth test case

        [Fact]
        /// <summary>
        /// Tests that StoreLocations table and Inventory table are populated after calling 
        /// AddStoreLocationsAndInventory() method
        /// </summary>
        public void TestForAddingLocationsAndInventoryToTable()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                repo.AddProducts();
                repo.AddStoreLocationsAndInventory();
            }

            using (var context = new GameStopDBContext(options))
            {
                var getLocations =   from l in context.storeLocations
                                     select l;

                var getInventory =   from i in context.inventory
                                     select i;
                // assert
                Assert.True(getLocations.Any());
                Assert.True(getInventory.Any());
            }
        } // end of fifth test case

        [Fact]
        /// <summary>
        /// Tests that the ChooseLocation() method returns the proper location
        /// </summary>
        public void TestForChoosingStoreLocation()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                repo.AddProducts();
                repo.AddStoreLocationsAndInventory();
            }

            using (var context = new GameStopDBContext(options))
            {
                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                string userChoice = "3";
                StoreLocation storeLocations = new StoreLocation();
                storeLocations = repo.ChooseLocation(userChoice);
                // assert
                Assert.True(storeLocations.Location == "Tokyo");
            }
        } // end of sixth test case

        [Fact]
        /// <summary>
        /// Tests that 
        /// </summary>
        public void TestForAddingOrders()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                repo.AddProducts();
                repo.AddStoreLocationsAndInventory();
            }

            using (var context = new GameStopDBContext(options))
            {
                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                
                Customer customer = new Customer("Travis", "Martin");
                string userChoice = "2";
                StoreLocation location = new StoreLocation();
                location = repo.ChooseLocation(userChoice);
                int productQuantity = 1;
                var productTable = from p in context.products
                                    select p;
                Product product = productTable.ToList()[0];
                repo.OrderHistory(customer, location, productQuantity, product);

                var parseOrderTable =   from o in context.orders
                                        where o.ProductName == product.ProductName
                                        select o;

                foreach (Order o in parseOrderTable) {
                    Assert.True(o.Customers.Fname == "Travis");
                }
            }
        } // end of seventh test case

        [Fact]
        /// <summary>
        /// Tests that calls the UpdateInventory() method to decrement the product quantity in the inventory 
        /// table based on the quantity of product ordered by the user
        /// </summary>
        public void TestForDecrementingInventoryProductQuantity()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                repo.AddProducts();
                repo.AddStoreLocationsAndInventory();
            }

            using (var context = new GameStopDBContext(options))
            {
                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                
                var productTable = from p in context.products
                                    where p.ProductName == "Ps4 game: Ghost of Tsushima"
                                    select p;

                Product product = productTable.ToList()[0];
                string userChoice = "2";
                StoreLocation location = new StoreLocation();
                location = repo.ChooseLocation(userChoice);
                int productQuantity = 1;
                repo.UpdateInventory(product, location, productQuantity);
                
                var inventoryTable = from p in context.inventory
                                        where p.StoreLocations == location & p.Products == product
                                        select p;

                foreach(Inventory i in inventoryTable) {
                    Assert.NotEqual(25, i.ProductQuantity);
                }
                
            }
        } // end of eighth test case

        [Fact]
        /// <summary>
        /// Tests that there is more than 1 order for the customer by calling CustomerOrderHistory() method
        /// </summary>
        public void TestForPrintingCustomerOrderHistory()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                repo.AddProducts();
                repo.AddStoreLocationsAndInventory();
            }

            using (var context = new GameStopDBContext(options))
            {
                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                
                Customer customer = new Customer("Travis", "Martin");
                string userChoice = "2";
                StoreLocation location = new StoreLocation();
                location = repo.ChooseLocation(userChoice);
                int productQuantity = 1;
                var productTable = from p in context.products
                                    select p;
                Product product = productTable.ToList()[0];
                repo.OrderHistory(customer, location, productQuantity, product);
                
                string userChoice2 = "4";
                StoreLocation location2 = new StoreLocation();
                location2 = repo.ChooseLocation(userChoice2);
                int productQuantity2 = 2;
                Product product2 = productTable.ToList()[2];
                repo.OrderHistory(customer, location2, productQuantity2, product2);

                int numOfOrders = repo.CustomerOrderHistory(customer);
                Assert.True(numOfOrders > 1);
                
            }
        } // end of ninth test case

        [Fact]
        /// <summary>
        /// Tests that 
        /// </summary>
        public void TestForPrintingStoreOrderHistory()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GameStopDBContext>()
            .UseInMemoryDatabase(databaseName: "GameStopDB")
            .Options;

            // act
            
            using (var context = new GameStopDBContext(options))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                repo.AddProducts();
                repo.AddStoreLocationsAndInventory();
            }

            using (var context = new GameStopDBContext(options))
            {
                GameStopRepositoryLayer repo = new GameStopRepositoryLayer(context);
                
                Customer customer = new Customer("Travis", "Martin");
                string userChoice = "2";
                StoreLocation location = new StoreLocation();
                location = repo.ChooseLocation(userChoice);
                int productQuantity = 1;
                var productTable = from p in context.products
                                    select p;
                Product product = productTable.ToList()[0];
                repo.OrderHistory(customer, location, productQuantity, product);
                
                Customer customer2 = new Customer("Handy", "Guy");
                int productQuantity2 = 3;
                Product product2 = productTable.ToList()[4];
                repo.OrderHistory(customer2, location, productQuantity2, product2);

                int numOfOrders = repo.StoreOrderHistory(location);
                Assert.True(numOfOrders > 1);
            }
        } // end of tenth test case

    } // end of class
} // end of namespace
