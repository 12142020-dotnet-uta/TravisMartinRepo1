using System.Collections.Generic;

namespace TravisMartin_Project0
{
    public interface GameStore
    {
        void ProductList(); // should put code here to initialize product table
        void AddStoreLocationsAndInventory(); // should put code here to initialize the StoreLocations and Inventory tables
        Customer CreateCustomer(string fName, string lName); // takes in first name and last name to create a customer object
        void OrderHistory(Customer customer, StoreLocation storeLocation, int productQuantity, Product product); // takes in customer order and fills Order table
        void UpdateInventory(Product product, StoreLocation storeLocation, int productQuantity); // updates Inventory table when customer makes and order (decremenets product quantity)
    }
}