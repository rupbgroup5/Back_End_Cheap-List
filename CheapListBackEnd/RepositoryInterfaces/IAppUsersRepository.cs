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
        AppUser GetAppUserByName(string userName);
        void PostAppUser(AppUser newUser);
        int DeleteAppUser(int id);
        int UpdateFeild(UpdateAppUser user2epdate);
        
    }
}
