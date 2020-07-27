using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Models
{
    public class AppProduct
    {
        public string product_barcode { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public string product_image { get; set; }
        public string manufacturer_name { get; set; }
        public double estimatedProductPrice { get; set; }

        //for ProductInList
        public int ListID { get; set; }
        public int GroupId { get; set; }
        public double ListEstimatedPrice { get; set; }
        public int Quantity { get; set; }

        public int NotID { get; set; }





    }
}