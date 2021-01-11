using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class InventoryViewModel
    {
        public Guid InventoryId { get; set; } = Guid.NewGuid();

        public Guid StoreLocationId { get; set; }
        public StoreViewModel StoreLocations { get; set; }
        public Guid ProductId { get; set; }
        public ProductViewModel Products { get; set; }

        public int ProductQuantity { get; set; } // number of each product
    }
}
