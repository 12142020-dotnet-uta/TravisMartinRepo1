using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Models
{
    public class Inventory
    {
        [Key]
        public Guid InventoryId { get; set; } = Guid.NewGuid();

        public Guid StoreLocationId { get; set; }
        public StoreLocation StoreLocations { get; set; }
        public Guid ProductId { get; set; }
        public Product Products { get; set; }

        public int ProductQuantity { get; set; } // number of each product
    }
}
