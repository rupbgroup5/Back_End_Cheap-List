using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface IAppListRepository
    {
        IEnumerable<AppList> GetAllList(int groupID);
        AppList GetAppListById(int groupID, int listID);
        int PostAppList(AppList appList);
        int DeleteAppList(int id);
        int UpdateListName(AppList appList);
        int UpdateCityName(string name, int listID);
        int UpdateLimitPrice(int limit, int listID);


    }
}
