using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Models
{
    public class AppProduct
    {
        public int product_barcode { get; set; }
        public string product_name { get; set; }
        public string product_description { get; set; }
        public string product_image { get; set; }
        public string manufacturer_name { get; set; }
        public int store_id { get; set; }

        //for ProductCart
        public int ListID { get; set; }
        public int GroupId { get; set; }
        public int EstimatedPrice { get; set; }
        public int Price { get; set; }

        public int check { get; set; }

      
    }
}