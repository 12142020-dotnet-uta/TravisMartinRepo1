using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ModelLayer.Models
{
    public class StoreLocation
    {
        public StoreLocation(string location = null)
        {
            Location = location;
        }
        [Key]
        public Guid LocationId { get; set; } = Guid.NewGuid();

        [DisplayName("Location of Store")]
        public string Location { get; set; }

        public ICollection<Inventory> StoreInventories { get; set; }
    }
}
