using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Pages
{
    public class InformationPage : PageBase
    {
        public InformationPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "article#content div.content h1")]
        public IWebElement Header { get; set; }
    }
}
