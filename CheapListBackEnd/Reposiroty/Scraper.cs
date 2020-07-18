using Microsoft.Ajax.Utilities;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheapListBackEnd.Reposiroty
{
    public class Scraper
    {
        public static List<string> GetSrcIMG(List<string> arrName)
        {
            var driver = new ChromeDriver();
            driver.Url = "https://www.google.com/imghp";
            List<string> returnValueArr = new List<string>();
            foreach (var item in arrName)
            {
                var search = driver.FindElementByName("q");
                search.SendKeys(item);
                search.SendKeys(Keys.Enter);
                IWebElement img = driver.FindElementByXPath("//*[@id=\"islrg\"]/div[1]/div[1]/a[1]/div[1]/img");
                string src = img.GetAttribute("src");
                returnValueArr.Add(src);
                driver.FindElementByName("q").Clear();
            }
            driver.Quit();
            return returnValueArr;
        }

        

    }

  
}