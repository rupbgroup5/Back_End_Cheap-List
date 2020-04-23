using CheapListBackEnd.Interfaces;
using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface IAppUsersRepository 
    {
        IEnumerable<AppUser> GetAllAppUsers();
        AppUser GetAppUserByID(int userID);
        AppUser GetUser_forgotPass(string userMail);
        void PostAppUser(AppUser newUser);
        int DeleteAppUser(int id);
        int UpdateFeild(UpdateAppUser user2epdate);
        AppUser AuthenticateUserLogin(string userName, string password);
        void UpdateUserContactsList(AppUser user);
        List<Contact> GetUserContacts(int userID);


    }
}
