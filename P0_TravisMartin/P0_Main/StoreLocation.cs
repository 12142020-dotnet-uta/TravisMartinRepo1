using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravisMartin_Project0
{
    public class StoreLocation
    {

        public StoreLocation(string location = "null") {
            this.location = location;
        }
        private Guid locationId = Guid.NewGuid(); // unique Id for each customer
        [Key]
        public Guid LocationId { get{ return locationId; } set{ locationId = value;} }

        private string location;
        public string Location { get { return location; } set { location = value; } }

        public ICollection<Inventory> StoreInventories { get; set; }

       // public ICollection<Order> StoreOrderHistory { get; set; }

        // maybe add method for printing store info to conosle        
        
    }
}