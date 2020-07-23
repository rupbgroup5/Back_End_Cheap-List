using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CheapListBackEnd.RepositoryInterfaces;
using HtmlAgilityPack;
//inspiration: https://www.youtube.com/watch?v=oeuvL1_5UIQ

namespace CheapListBackEnd.Reposiroty
{
    public class WebCrawlerRepository : IWebCrawlerRepository
    {
        public async Task<List<string>> GetImagesUrlsAsync(List<string> productsNames)
        {


            List<string> returnValueArr = new List<string>();
            string targetUrl = "";
            string imgSrc = "";
            #region EMPTY PICTURE
            string emptyPic = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOE" +
                "AAADhCAMAAAAJbSJIAAAAMFBMVEXh6vH////5+vrl7fPv8/f0+fzt9Pbf6vD+/f" +
                "/j6/Lf6vL///3f6fLr8fbv8/by9vm0HxD7AAACbUlEQVR4nO3c65abIBRAYVHTD" +
                "ALx/d+2VBuDII6zLHro2t/fmEn2HETn2jQAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" +
                "ADkG29irNHa4I9G9+qu83B2BTauu01JIIYUUUkghhbcX/ipFTGH3LKOTUmi6Qq/" +
                "QGQrLovA8Ckuj8LzqC/98A2ZX5YXd2Lajf6beOaTmwm5+tnnsBNZc+NTt/OaNan" +
                "WT/U5hzYWjWjzyh1VSqK1ONpTPW/fyz66kcHCqixJtHwSqPvvUKgq1D/Rjik61M" +
                "ZzhWPd5aJ3fTfwR6yn+P4XzBFU8RevCwvzPJCoo9IFmepdmPUUdFuqaZzgt0fcU" +
                "w8T+88Ar/wrSC/UywemgcKE+7Tg/ZNRY8V2bDScYT9G6r+mdu+fOK8guXE8wmaK" +
                "/Vet6980PPmUXDvEEVXLRCL942lwGkguny0RSmF76//ILuh/SE1JyYXIObk/xzf" +
                "n17NJH5BZ+LvSHpmjno10yRbmFySYTDHFjivONnZ9iHC+3MLNE31NcJy7zNsn9m" +
                "9DC9DIRTXG9UD977jTF1UIVWpjbZDJTDM/YeLsRWfjdBKMprj8d8RRFFm5d6Dca" +
                "31OM99z1diOwMHehT01TTBe0WV00BBYem+AyRbdxcDhFgYX6YN80RZu5LTCSZ9g" +
                "cLzSqz+xIZvloAgt/MsN8u+QZUkghhRRSSCGFFFJIIYWXFKqv85Towr3fcDpKSy" +
                "78xygsjcLzKCyNwvPEFCpthxKsVlIKXVeGE1NYHoUUUkghhRRSeFfh2D6u0o53B" +
                "OpmuOz/RA17f8MHAAAAAAAAAAAAAAAAAAAAAAAAAAAAALjcb3yLQG5tF3tgAAAA" +
                "AElFTkSuQmCC";
            #endregion


            foreach (var product in productsNames)
            {
                targetUrl = $"https://www.google.com/search?q={product}&" +
                $"tbm=isch&ved=2ahUKEwiZ_rPR2dzqAhUJMRoKHYIDABYQ2-cCegQ" +
                $"IABAA&oq={product}&gs_lcp=CgNpbWcQAzICCAAyAggAMgIIADI" +
                $"CCAAyAggAMgIIADICCAAyAggAMgIIADICCAA6BAgjECdQ3RpY2xtg" +
                $"giFoAHAAeACAAYkBiAGXA5IBAzAuM5gBAKABAaoBC2d3cy13aXota" +
                $"W1nwAEB&sclient=img&ei=qgIWX5mBGoniaIKHgLAB&bih=750&b" +
                $"iw=1536";

                var httpClient = new HttpClient();

                var html = await httpClient.GetStringAsync(targetUrl);

                var htmlDoc = new HtmlDocument();

                htmlDoc.LoadHtml(html);

                var divs = htmlDoc.DocumentNode.Descendants("div")
                            .Where(node => node.GetAttributeValue("class", "")
                            .Equals("")).ToList();
                try
                {
                    //future to be adapted when using try catch the preformence is not as good
                    //when not, so try use ternary condition operator as such:
                    //{ divs[0].Descendants("img")?. ...}
                    //and see if the Descendants default return value is null 
                    //or throw an exaption, in the first senario is true I can fix the try catch
                    //to simple if else
                    imgSrc = divs[0].Descendants("img").FirstOrDefault().ChildAttributes("src").FirstOrDefault().Value;
                }
                catch (NullReferenceException)
                {
                    imgSrc = emptyPic;
                }
                returnValueArr.Add(imgSrc);
            }
            return returnValueArr;
        }
    }
}