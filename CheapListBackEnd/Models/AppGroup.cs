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
        public int UserID { get; set; }
        public List<AppUser> Participiants { get; set; }


    }
}