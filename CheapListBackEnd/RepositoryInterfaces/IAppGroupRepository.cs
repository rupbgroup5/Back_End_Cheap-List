using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface IAppGroupRepository
    {
        List<AppGroup> GetAllGroups();
        List<AppGroup> GetAppGroupById(int id);
        AppGroup PostAppGroup(AppGroup appGroup);
        int DeleteAppGroup(int id);
        int UpdateGroupName(AppGroup appGroup);
    }
}
