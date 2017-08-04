using System;
using System.Collections.Generic;
using System.Linq;
using JustWatch.Selenium.Controls;
using JustWatch.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Pages
{
    public class ManufacturerPage : PageBase
    {
        public static ManufacturerPage WaitForPage(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementExists(
               PageObjectExtensions.GetElementLocator<ManufacturerPage>(x => x.Breadcrumb)));

            return new ManufacturerPage(driver);
        }

        protected ManufacturerPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "ul.breadcrumb")]
        public IWebElement Breadcrumb { get; set; }

        public bool HasProducts
        {
            get { return _webDriver.FindElements(By.CssSelector("div.content div#res-products")).Any(); }
        }

        public IEnumerable<ProductCard> GetProductCards()
        {
            return _webDriver.FindElements(By.CssSelector("div.content div#res-products div.product"))
                .Select(element => new ProductCard(_webDriver, element));
        }
    }
}
