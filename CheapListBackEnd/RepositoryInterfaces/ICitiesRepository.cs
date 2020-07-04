using CheapListBackEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.RepositoryInterfaces
{
    public interface ICitiesRepository
    {
        int PostCities(List<Cities> cities);
        IEnumerable<Cities> GetCities();
    }
}