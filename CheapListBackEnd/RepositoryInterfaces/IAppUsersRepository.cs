﻿using CheapListBackEnd.Interfaces;
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
        int UpdateFeild(UpdateAppUser user2epdate);
        AppUser AuthenticateUserLogin(string userName, string password);
        void UpdateUserContactsList(AppUser user);
        List<Contact> GetUserContacts(int userID);
<<<<<<< HEAD
        AppUser AuthenticateContact(string phoneNumber);
        int PostSystemAppUser(AppUser userBySystem);

        

=======
        AppUser GetExsistUserSocailID(string socailID);
        int UpdateUserExpoToken(AppUser user);
        bool IsExpoTokenUpdated(int userID);
>>>>>>> 17c6f29ae26c8b7bb4bfb42b92403ea1a86c75db

    }
}
