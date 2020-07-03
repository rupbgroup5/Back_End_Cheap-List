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
        public int UserID { get; set; }
        /*public string CreatorName { get; set; }*/ // for UserInList
        
        public double ListEstimatedPrice { get; set; }
        public int LimitPrice { get; set; }
        public string CityName { get; set; }
        public int CityID { get; set; }
        public string TypeLocation { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int KM_radius { get; set; }



    }
}