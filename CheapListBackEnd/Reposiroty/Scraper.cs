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
        public static string GetSrcIMG(string productName)
        {
            var driver = new ChromeDriver();
            driver.Url = "https://www.google.com/imghp";
            var search = driver.FindElementByName("q");
            search.SendKeys(productName);
            search.SendKeys(Keys.Enter);

            IWebElement img = driver.FindElementByXPath("//*[@id=\"islrg\"]/div[1]/div[1]/a[1]/div[1]/img");
            string src = img.GetAttribute("src");
            driver.Quit();
            //driver.Navigate().GoToUrl(src);

            //Console.WriteLine("count " + src.Length);
            return src;
        }

        

    }

  
}