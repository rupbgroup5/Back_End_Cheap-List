using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface INotificationsRepository
    {
        List<Notifications> GetNotifactionsByID(int userID, int listID);
        int PostNotifactions(Notifications notifactions);
        int DeleteNotifactions(Notifications notifications);
        List<Notifications> GetNotifactionsByGroupID(int userID, int groupID);
        int UpdateNotifactions(List<Notifications> notifications);
        int PostNot2MultipleParticipants(Notifications notification);
        
    }
}