using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Models
{
    public class AppList 
    {
        public int ListID { get; set; }
        public string ListName { get; set; }
        public int GroupID { get; set; }
        public string CreatorName { get; set; } // for UserInList
        public int ListTotalPrice { get; set; }
        public string ListDescription { get; set; }
      

    }
}