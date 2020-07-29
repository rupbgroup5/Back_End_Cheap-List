using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Models
{
    public class Notifications
    {
        public int NotID { get; set; }
        public int UserFrom { get; set; }
        public int UserTo { get; set; }
        public List<int> UsersTo { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string DataObject { get; set; }
        public bool HasRead { get; set; }
        public bool HasDone { get; set; }
        public string TypeNot { get; set; }
        public int GroupID { get; set; }
        public int ListID { get; set; }


    }
}