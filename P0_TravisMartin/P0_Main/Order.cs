using System;
using System.ComponentModel.DataAnnotations;

namespace TravisMartin_Project0
{
    public class Order
    {
        private Guid orderId = Guid.NewGuid(); // unique Id for each customer
        [Key]
        public Guid OrderId { get { return orderId; } set{ orderId = value;} }

        public Guid CustomerId { get; set; }
        public Customer Customers { get; set; }

        public Guid StoreLocationId { get; set; }
        public StoreLocation StoreLocations { get; set; }

        public DateTime Ordertime { get; set; }

        public string ProductName { get; set; }
        // public Guid ProductId { get; set; }
        // public Product Products { get; set; }
        // public ICollection<Product> Products { get; set; }

        public int OrderQuantity { get; set; }

        public double TotalOrderPrice { get; set; }

    }
}