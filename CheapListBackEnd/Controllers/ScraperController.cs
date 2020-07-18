using CheapListBackEnd.Reposiroty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CheapListBackEnd.Controllers
{
    public class ScraperController : ApiController
    {
    
        public List<string> Post([FromBody] List<string> arrName)
        {
           return Scraper.GetSrcIMG(arrName);
        }


    }
}