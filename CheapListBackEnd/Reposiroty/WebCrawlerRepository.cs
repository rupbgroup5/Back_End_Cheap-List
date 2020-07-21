using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CheapListBackEnd.RepositoryInterfaces;
using HtmlAgilityPack;

namespace CheapListBackEnd.Reposiroty
{
    public class WebCrawlerRepository : IWebCrawlerRepository
    {
        public async Task<List<string>> GetImagesUrlsAsync(List<string> productsNames)
        {

            //inspiration: https://www.youtube.com/watch?v=oeuvL1_5UIQ
            List<string> returnValueArr = new List<string>();
            string targetUrl = "";
            foreach (var product in productsNames)
            {
                targetUrl = $"https://www." +
                $"google.com/search?q={product}&" +
                $"tbm=isch&ved=2ahUKEwiZ_rPR2dzqAhUJMRoKHYIDABYQ2-cCegQIABAA&" +
                $"oq={product}" +
                $"&gs_lcp=CgNpbWcQAzICCAAyAggAMgIIADICCAAyAggAMgIIADICCAAyAggA" +
                $"MgIIADICCAA6BAgjECdQ3RpY2xtggiFoAHAAeACAAYkBiAGXA5IBAzAuM5gBAK" +
                $"ABAaoBC2d3cy13aXotaW1nwAEB&sclient=img&ei=qgIWX5mBGoniaIKHgLAB&" +
                $"bih=750&biw=1536";
                var httpClient = new HttpClient();

                var html = await httpClient.GetStringAsync(targetUrl);

                var htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(html);

                var divs = htmlDoc.DocumentNode.Descendants("div")
                          .Where(node => node.GetAttributeValue("class", "")
                          .Equals("")).ToList();

                var imgSrc = divs[0].Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
                returnValueArr.Add(imgSrc);
            }

            throw new NotImplementedException();
        }
    }
}