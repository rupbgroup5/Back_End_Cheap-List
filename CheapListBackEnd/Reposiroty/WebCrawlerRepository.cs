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
            string emptyPic = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAMFBMVEXh6vH////5+vrl7fPv8/f0+fzt9Pbf6vD+/f/j6/Lf6vL///3f6fLr8fbv8/by9vm0HxD7AAACbUlEQVR4nO3c65abIBRAYVHTDALx/d+2VBuDII6zLHro2t/fmEn2HETn2jQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAADkG29irNHa4I9G9+qu83B2BTauu01JIIYUUUkghhbcX/ipFTGH3LKOTUmi6Qq/QGQrLovA8Ckuj8LzqC/98A2ZX5YXd2Lajf6beOaTmwm5+tnnsBNZc+NTt/OaNanWT/U5hzYWjWjzyh1VSqK1ONpTPW/fyz66kcHCqixJtHwSqPvvUKgq1D/Rjik61MZzhWPd5aJ3fTfwR6yn+P4XzBFU8RevCwvzPJCoo9IFmepdmPUUdFuqaZzgt0fcUw8T+88Ar/wrSC/UywemgcKE+7Tg/ZNRY8V2bDScYT9G6r+mdu+fOK8guXE8wmaK/Vet6980PPmUXDvEEVXLRCL942lwGkguny0RSmF76//ILuh/SE1JyYXIObk/xzfn17NJH5BZ+LvSHpmjno10yRbmFySYTDHFjivONnZ9iHC+3MLNE31NcJy7zNsn9m9DC9DIRTXG9UD977jTF1UIVWpjbZDJTDM/YeLsRWfjdBKMprj8d8RRFFm5d6Dca31OM99z1diOwMHehT01TTBe0WV00BBYem+AyRbdxcDhFgYX6YN80RZu5LTCSZ9gcLzSqz+xIZvloAgt/MsN8u+QZUkghhRRSSCGFFFJIIYWXFKqv85Towr3fcDpKSy78xygsjcLzKCyNwvPEFCpthxKsVlIKXVeGE1NYHoUUUkghhRRSeFfh2D6u0o53BOpmuOz/RA17f8MHAAAAAAAAAAAAAAAAAAAAAAAAAAAAALjcb3yLQG5tF3tgAAAAAElFTkSuQmCC";
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

                try
                {
                  var  imgSrc = divs[0].Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
                    returnValueArr.Add(imgSrc);
                }
                catch (NullReferenceException)
                {
                    returnValueArr.Add(emptyPic);
                   
                }
                
                
                
            }
            return returnValueArr;
        }
    }
}