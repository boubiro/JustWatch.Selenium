using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using log4net;

namespace JustWatch.Selenium.Tests
{
    public abstract class TestsBase
    {
        protected IWebDriver _driver;
        protected IWait<IWebDriver> _wait;
        protected ILog _logger;

        static TestsBase()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        [OneTimeSetUp]
        public void Init()
        {
            var driverFactory = new FirefoxDriverFactory();
            _driver = driverFactory.Create();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            var type = GetType();
            _logger = LogManager.GetLogger(GetType());

            _logger.Debug("OneTimeSetup");
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _driver.Quit();

            _logger.Debug("OneTimeTearDown");
        }

        [SetUp]
        public void RunBeforeEachTest()
        {
            _driver.Navigate().GoToUrl(AppSettings.WebSiteUrl);

            _logger.Debug("SetUp");
        }

        [TearDown]
        public void RunAfterEachTest()
        {
            _logger.Debug("TearDown");
        }
    }
}
