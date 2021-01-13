using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelLayer.Models
{
    public class Product
    {
        public Product(string productName = "null", double productPrice = 0, string productDescription = "null")
        {
            ProductName = productName;
            ProductPrice = productPrice;
            ProductDescription = productDescription;
        }
        [Key]
        public Guid ProductId { get; set; } = Guid.NewGuid();

        [DisplayName("PS4 Game Name")]
        public string ProductName { get; set; } // name of product
        public double ProductPrice { get; set; } // price of product
        public string ProductDescription { get; set; } // brief description of product

        public byte[] ByteArrayImage { get; set; }

        public ICollection<Inventory> ProductInventory { get; set; }

    }
}
