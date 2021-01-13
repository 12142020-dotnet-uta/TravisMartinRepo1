using ModelLayer.Models;
using ModelLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class MapperClass
    {

        /// <summary>
        /// Converts the Customer db context model to CustoemrViewModel
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        internal CustomerViewModel ConvertCustomerToCustomerViewModel(Customer customer)
        {
            CustomerViewModel customerViewModel = new CustomerViewModel()
            {
                CustomerId = customer.CustomerId,
                FName = customer.FName,
                LName = customer.LName,
                Email = customer.Email,
                UserName = customer.UserName,
                Password = customer.Password
            };

            return customerViewModel;
        }

        /// <summary>
        /// Converts the Product db context model to ProductViewModel
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        internal ProductViewModel ConvertProductToProductViewModel(Product p)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                ProductPrice = p.ProductPrice,
                ProductDescription = p.ProductDescription,
                JpgStringImage = ConvertByteArrayToJpgString(p.ByteArrayImage)
            };

            return productViewModel;
        }

        /// <summary>
        /// Converts an image stored in a byte array to a jpg string to store in the view model
        /// </summary>
        /// <param name="byteArray"></param>
        /// <returns></returns>
        private string ConvertByteArrayToJpgString(byte[] byteArray)
        {
            if (byteArray != null)
            {
                string imageBase64Data = Convert.ToBase64String(byteArray, 0, byteArray.Length);
                string imageDataURL = string.Format($"data:image/jpg;base64,{imageBase64Data}");
                return imageDataURL;
            }
            else return null;
        }

    }
}
