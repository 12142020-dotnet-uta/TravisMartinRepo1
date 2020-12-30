
namespace TravisMartin_Project0
{
    public interface GameStore
    {
        public void AddProducts(); // Initializes Product table on startup
        public void AddStoreLocationsAndInventory(); // Initializes StoreLocation table and Inventory table on startup
        public Customer CreateCustomer(string fName, string lName); // creates a customer and adds them to the Customer table
        public void OrderHistory(Customer customer, StoreLocation storeLocation, int productQuantity, Product product); // Adds customer order to the Order table 1 entry at a time
        public void UpdateInventory(Product product, StoreLocation storeLocation, int productQuantity); // Decrements product quantity in inventory when order is added to Order table
        
    }
}