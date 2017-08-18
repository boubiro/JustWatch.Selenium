using System;
using JustWatch.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Pages
{
    public class InformationPage : PageBase
    {
        public static InformationPage WaitForPage(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementExists(
                PageObjectExtensions.GetElementLocator<InformationPage>(x => x.Header)));

            wait.Until(ExpectedConditions.ElementExists(
                PageObjectExtensions.GetElementLocator<ProductPage>(x => x.PhoneCallButton)));

            return new InformationPage(driver);
        }

        protected InformationPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "article#content div.content h1")]
        public IWebElement Header { get; set; }
    }
}
