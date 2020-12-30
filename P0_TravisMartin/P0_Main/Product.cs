using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravisMartin_Project0
{
    public class Product
    {

        public Product (string productName = "null", double productPrice = 0, string productDescription = "null") {
            this.ProductName = productName;
            this.ProductPrice = productPrice;
            this.ProductDescription = productDescription;
        }
        private Guid productId = Guid.NewGuid(); // uniques Id for each product
        [Key]
        public Guid ProductId { get{ return productId; } set{ productId = value;} }

        public string ProductName { get; set; } // name of product
        public double ProductPrice { get; set; } // price of product
        public string ProductDescription { get; set; } // brief description of product

       // public Order Orders { get; set; }
       // public ICollection<Order> Orders { get; set; }
        public ICollection<Inventory> ProductInventory { get; set; }

        // maybe add toString method for printing product details to console

    }
}