using System;
using System.ComponentModel.DataAnnotations;

namespace TravisMartin_Project0
{
    public class Inventory
    {

        private Guid inventoryId = Guid.NewGuid(); // unique Id for each customer
        [Key]
        public Guid InventoryId { get{ return inventoryId; } set{ inventoryId = value;} }

        public Guid StoreLocationId { get; set; }
        public StoreLocation StoreLocations { get; set; } 
        public Guid ProductId { get; set; }
        public Product Products { get; set; } 

        public int ProductQuantity { get; set; } // number of each product

        // maybe add toString method for printing inventory info to console
    }
}