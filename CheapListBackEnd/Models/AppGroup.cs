using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Models
{
    public class AppGroup 
    {

        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public string GroupImg { get; set; }
        public string CreatorName { get; set; }
        public bool IsAdmin { get; set; }

    }
}