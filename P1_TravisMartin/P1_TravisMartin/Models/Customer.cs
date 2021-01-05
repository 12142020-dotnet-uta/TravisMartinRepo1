using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace P1_TravisMartin.Models
{
    public class Customer
    {
        public Customer(string FName = "null", string LName = "null")
        {
            this.FName = FName;
            this.LName = LName;
        }

        private Guid customerId = Guid.NewGuid(); // unique Id for each customer
       
        public Guid CustomerId { get { return customerId; } set { customerId = value; } }

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string FName { get; set; } // customer first name

        [StringLength(20, MinimumLength = 3)]
        [Required]
        public string LName { get; set; } // customer last name

    }
}
