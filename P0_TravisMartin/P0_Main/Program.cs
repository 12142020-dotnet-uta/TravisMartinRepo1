using System;
using System.Linq;

namespace TravisMartin_Project0
{
    class Program
    {
        static GameStopRepositoryLayer storeContext = new GameStopRepositoryLayer();
        static GameStopDBContext databaseContext = new GameStopDBContext();
    
        static void Main(string[] args)
        {

            Console.Clear();
            //storeContext.InitializeInventory(); // initializes products table, inventory table, and storelocations table
            storeContext.AddProducts();
            storeContext.AddStoreLocationsAndInventory();

            Console.WriteLine("Welcome to the Martin Store Application!");

            int logInOrQuit;

            do {

                logInOrQuit = MainMenu(); // prints menu for user to log in or quit the program

                

                if (logInOrQuit == 2) { // quits the program
                    break;
                }

                Customer storeCustomer = new Customer(); // create a new customer

                storeCustomer = LogIn(); // validates the user login and stores name in database

                StoreLocation locations = ChooseStore(storeCustomer); // stores users store location choice

                printStoreInventory(storeCustomer, locations); // prints inventory list from store location and allows user to choose items for checkout


            } while (logInOrQuit != 2); // quits program if user chooses to do so
            
        } // END of main method

        /// <summary>
        /// Allows user to enter a user name or quit the program
        /// </summary>
        public static int MainMenu() {

            int logInOrQuit;
            do {
                Console.WriteLine("Choose one of these options: ");
                Console.WriteLine("\t1. Login \n\t2. Quit");
                logInOrQuit = storeContext.ConvertToValidInput(Console.ReadLine());

                if (logInOrQuit == -1) {
                    Console.WriteLine("Entered invalid input. Enter 1 to log in or 2 to quit.");
                }
            } while (logInOrQuit != 1 && logInOrQuit != 2);

            return logInOrQuit;
        }

        /// <summary>
        /// Takes in the customer's user name
        /// returns a string array of the first and last name
        /// will accept first name only and print null to the database for last name
        /// if more names are entered, they will be ignored
        /// </summary>
        /// <returns></returns>
        public static Customer LogIn() {

            string[] userNamesArray; // array to store first and last name
            bool validName = false;
            Customer shopper = new Customer();
            Order shopperOrder = new Order();
            do {
                Console.WriteLine("\nPlease enter your first and last name.\nDon't enter numeric values.");
                userNamesArray = Console.ReadLine().Trim().Split(' '); // splits first and last name into 2 entries

                if (userNamesArray.Length == 1) { // if only 1 name is entered, goes on without last name
                    if (Int32.TryParse(userNamesArray[0], out int result) == false && 
                    userNamesArray[0].Length < 20 && userNamesArray[0].Length > 0) {
                        shopper = storeContext.CreateCustomer(fName: userNamesArray[0]);
                        // maybe add customer to oder table here, even though customer hasn't bought anything the first time around
                        validName = true;
                    }
                } else if (userNamesArray.Length > 1) { // takes in first and last name and discards anything extra
                    if (Int32.TryParse(userNamesArray[0], out int result) == false && 
                    userNamesArray[0].Length < 20 && userNamesArray[0].Length > 0 &&
                    Int32.TryParse(userNamesArray[1], out int result1) == false && 
                    userNamesArray[1].Length < 20 && userNamesArray[1].Length > 0) {
                        shopper = storeContext.CreateCustomer(userNamesArray[0], userNamesArray[1]);
                        validName = true;
                    }
                }
            } while (validName == false); // reprompts user until valid input is recieved

            return shopper;
        }

        /// <summary>
        /// The menu for the customer to choose a store location
        /// Takes in customer choice and returns the chosen location
        /// </summary>
        /// <param name="shopper"></param>
        /// <returns></returns>
        public static StoreLocation ChooseStore(Customer shopper) {
            StoreLocation storeLocation = new StoreLocation();
            string userChoice;
            bool validLocation = false;
            do {
                Console.WriteLine($"Welcome, {shopper.Fname}, please choose a location from the list below by typing in its number.");
                Console.WriteLine("\t1. Raleigh \n\t2. Dubai \n\t3. Tokyo \n\t4. London \n\t5. Rome");
                userChoice = Console.ReadLine().Trim(); // saves choice to StoreLocation property
                if (storeContext.ConvertToValidInput(userChoice) > 5 || storeContext.ConvertToValidInput(userChoice) < 1) {
                    Console.WriteLine("You picked wrong! Try again...");
                } else {
                    storeLocation = storeContext.ChooseLocation(userChoice);
                    validLocation = true;
                }
            } while(validLocation == false);

            return storeLocation;
        }

        public static void printStoreInventory(Customer shopper, StoreLocation storeChoice) {
            string[] userChoice;
            string continueShopping;
            bool checkOut = false;
            // linq query to get product names from table
            var productName =   from p in databaseContext.products
                                select p.ProductName;
            var productPrice =  from p in databaseContext.products
                                select p.ProductPrice;                   
            do {
                Console.WriteLine($"Welcome to the {storeChoice.Location} GameStop location! Choose what product to add to the cart and how many. Enter 2 numbers separated by a space.");
                Console.WriteLine("First number must be 1-5 and Second number must be 1-3.");
                // print products here
                // grabs and prints products and prices from Product table
                Console.WriteLine($"\t1. ${productPrice.ToList()[0]} {productName.ToList()[0]}"); 
                Console.WriteLine($"\t2. ${productPrice.ToList()[1]} {productName.ToList()[1]}");
                Console.WriteLine($"\t3. ${productPrice.ToList()[2]} {productName.ToList()[2]}");
                Console.WriteLine($"\t4. ${productPrice.ToList()[3]} {productName.ToList()[3]}");
                Console.WriteLine($"\t5. ${productPrice.ToList()[4]} {productName.ToList()[4]}");
                // takes in 2 numeric values: 1st one choose item and the 2nd one choose item quantity
                userChoice = Console.ReadLine().Trim().Split(' ');
                // checks if user entered in anything besides a number, just 1 input, anything less than 1 and greater than 3
                // prints error message if true
                if (userChoice.Length == 1 || (storeContext.ConvertToValidInput(userChoice[0]) > 5 || storeContext.ConvertToValidInput(userChoice[0]) < 1) ||
                    (storeContext.ConvertToValidInput(userChoice[1]) > 3 || storeContext.ConvertToValidInput(userChoice[1]) < 1)) {
                    Console.WriteLine("You picked wrong! Try again...");
                } else {
                    string productToBuy = productName.ToList()[Int32.Parse(userChoice[0])-1]; // stores name of product customer chooses into a string
                    double priceOfProduct = productPrice.ToList()[Int32.Parse(userChoice[0])-1]; // stores prices of product customer chooses into a double
                    var productCheckout =   from p in databaseContext.products // queries products table for product that user chose
                                            where p.ProductName == productToBuy
                                            select p;
                    //Console.WriteLine(productCheckout.ToList()[0].ProductDescription);
                    int convertToInt = Int32.Parse(userChoice[1]);
                    Product convertToProduct = productCheckout.ToList()[0];
                    storeContext.OrderHistory(shopper, storeChoice, convertToInt, convertToProduct); // calls OrderHistory method to update order table
                    storeContext.UpdateInventory(convertToProduct, storeChoice, convertToInt); // calls UpdateInventory method to subtract items bought from store inventory
                    // prints out the quantity and the name of the time the user put in their cart
                    Console.WriteLine($"You added {userChoice[1]} {productToBuy} to your cart. Would you like to add more items (y/n)? ");
                    continueShopping = Console.ReadLine().Trim();
                    // validates that user entered in y or n
                    if (Int32.TryParse(continueShopping, out int result) == false && 
                        continueShopping.ToUpper() == "Y") {
                        continue;
                    } else {
                        string printOrderHistory;
                        do {
                            Console.WriteLine("If you want to print the order history, choose one of the following:");
                            Console.WriteLine($"\t1. {shopper.Fname} order history \n\t2. {storeChoice.Location} GameStop order history");
                            Console.WriteLine("Otherwise, press anything else");
                            printOrderHistory = Console.ReadLine().Trim();
                            if (storeContext.ConvertToValidInput(printOrderHistory) == 1) { // if customer chose 1, print our customer order history
                                storeContext.CustomerOrderHistory(shopper);
                            } else if (storeContext.ConvertToValidInput(printOrderHistory) == 2) { // if customer chose 2, print out store order history
                                storeContext.StoreOrderHistory(storeChoice);
                            } else { // if customer chose anything else,break out of loop and go back to start of program
                                break;
                            }
                            
                        } while(true);
                        Console.WriteLine($"Thank you for shopping at the {storeChoice.Location} GameStop location. Come back again!");
                        checkOut = true;
                    }
                }
            } while(checkOut == false);
        }

    } // END of class
} // END of namespace
