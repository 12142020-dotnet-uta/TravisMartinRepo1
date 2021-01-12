using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class CartViewModel
    {
        public Guid CartId { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; } = Guid.NewGuid();
        public Guid LocationId { get; set; } = Guid.NewGuid();
        public Guid ProductId { get; set; } = Guid.NewGuid();

        [DisplayName("PS4 Game Name")]
        public string ProductName { get; set; } // name of product
        [DisplayName("Price")]
        public double ProductPrice { get; set; } // price of product
        [DisplayName("Description")]
        public string ProductDescription { get; set; } // brief description of product
        [DisplayName("Image of Game")]
        public string JpgStringImage { get; set; }
        public int AmountChosen { get; set; } // number of products to put in cart
    }
}
