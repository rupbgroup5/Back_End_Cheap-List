﻿using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface IAppListRepository
    {
        IEnumerable<AppList> GetAllListbyGroupId(int groupID, int userID);

        //AppList GetAppListById(int groupID, int listID);
        AppList PostAppList(AppList appList);
        int DeleteAppList(int id);

        int UpdateListName(AppList appList);
        //int UpdateCityName(AppList appList);
        int UpdateLimitPrice(double limit, int listID);
        int UpdateLocation(AppList appList);


    }
}
