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
            //productsNames.Add("חלב");
            //productsNames.Add("גבינה");
            //productsNames.Add("לחם אחיד פרוס");
            //productsNames.Add("עגבנייה");
            //productsNames.Add("מלפפון");
            //productsNames.Add("פלפל אדום");
            //productsNames.Add("פלפל חריף");
            //productsNames.Add("ביצים");

            return repo.GetImagesUrlsAsync(productsNames);
        }
    }
}