using CheapListBackEnd.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CheapListBackEnd.Controllers
{
    public class WebCrawlerController : ApiController
    {
        IWebCrawlerRepository repo;
        public WebCrawlerController(IWebCrawlerRepository ir) => repo = ir;

        public Task<List<string>> Post([FromBody] List<string> productsNames)
        {
            return repo.GetImagesUrlsAsync(productsNames);
        }
    }
}