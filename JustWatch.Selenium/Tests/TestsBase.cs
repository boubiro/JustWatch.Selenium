using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace JustWatch.Selenium.Tests
{
    public abstract class TestsBase
    {
        protected IWebDriver _driver;
        protected IWait<IWebDriver> _wait;

        [OneTimeSetUp]
        public void Init()
        {
            var driverFactory = new FirefoxDriverFactory();
            _driver = driverFactory.Create();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _driver.Quit();
        }

        [SetUp]
        public void RunBeforeEachTest()
        {
            _driver.Navigate().GoToUrl(AppSettings.WebSiteUrl);
        }
    }
}
