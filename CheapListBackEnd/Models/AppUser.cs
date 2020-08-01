using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Models
{
    public class AppUser
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserMail { get; set; }
        public string UserAdress { get; set; }
        public List<Contact> Contacts { get; set; }
        public string WayOf_Registration { get; set; }
        public string SocialID { get; set; }
        public string ExpoToken { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsAdmin { get; set; }
        public string DateOfLast_Register { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string UserImgUrl { get; set; }

    }
}