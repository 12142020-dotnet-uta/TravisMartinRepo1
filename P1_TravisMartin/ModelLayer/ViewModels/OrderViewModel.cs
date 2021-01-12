using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class OrderViewModel
    {
        public Guid OrderId { get; set; } = Guid.NewGuid();

        public Guid CustomerId { get; set; }
        public CustomerViewModel Customers { get; set; }

        public Guid StoreLocationId { get; set; }
        public StoreViewModel StoreLocations { get; set; }

       // public string ProductName { get; set; }
         public Guid ProductId { get; set; }
         public ProductViewModel Products { get; set; }
        // public ICollection<Product> Products { get; set; }
        public DateTime Ordertime { get; set; }

        public int OrderQuantity { get; set; }

        public double TotalOrderPrice { get; set; }
    }
}
