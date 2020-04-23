using CheapListBackEnd.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Models
{
    public class UpdateAppUser : Iupdateable
    {
        int id;
        string column2update, newValue;

        public int Id { get => id; set => id = value; }
        public string Column2update { get => column2update; set => column2update = value; }
        public string NewValue { get => newValue; set => newValue = value; }

    }
}