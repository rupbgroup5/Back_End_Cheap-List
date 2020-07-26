using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface INotificationsRepository
    {
        List<Notifications> GetNotifactionsByID(int id);
        int PostNotifactions(Notifications notifactions);
    }
}