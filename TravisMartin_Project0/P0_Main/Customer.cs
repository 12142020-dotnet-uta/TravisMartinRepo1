using System;
using System.ComponentModel.DataAnnotations;

namespace TravisMartin_Project0
{
    public class Customer
    {
        public Customer(string fname = "null", string lname = "null")
        {
            this.Fname = fname;
            this.Lname = lname;
        }

        private Guid customerId = Guid.NewGuid(); // unique Id for each customer
        [Key]
        public Guid CustomerId { get{ return customerId; } set{ customerId = value;} }

        private string fName; // customer first name
        public string Fname 
        {
            get { return fName; }
            set{ fName = value;}
        }

        private string lName; // customer last name
        public string Lname 
        {
            get { return lName; }
            set { lName = value; }
        }
    }
}