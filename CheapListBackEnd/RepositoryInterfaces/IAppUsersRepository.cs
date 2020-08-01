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
        string UserForgotPassword(string userMail);
        int PostAppUser(AppUser newUser);
        int DeleteAppUser(int id);
        int UpdateFeilds(AppUser newUser);
        AppUser AuthenticateUserLogin(string userName, string password);
        void UpdateUserContactsList(AppUser user);
        List<Contact> GetUserContacts(int userID);
        AppUser AuthenticateContact(string phoneNumber);
        int PostSystemAppUser(AppUser userBySystem, string requestSenderName);
        AppUser GetExsistUserSocailID(string socailID);
        int UpdateUserExpoToken(AppUser user);
        int UpdateUserCoords(AppUser user);
        bool IsExpoTokenUpdated(int userID);
        string UploadImgUser(AppUser user);



    }
}
