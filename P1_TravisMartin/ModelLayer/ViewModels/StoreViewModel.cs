using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer.ViewModels
{
    public class StoreViewModel
    {
        public Guid LocationId { get; set; } = Guid.NewGuid();

        public string Location { get; set; }

        public List <StoreViewModel> storesList { get; set; }

        public List<InventoryViewModel> StoreInventories { get; set; }

    }
}
