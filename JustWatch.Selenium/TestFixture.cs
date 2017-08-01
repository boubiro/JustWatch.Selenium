using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using JustWatch.Selenium.Pages;
using JustWatch.Selenium.Controls;

namespace JustWatch.Selenium
{
    [TestFixture]
    public class TestFixture
    {
        private IWebDriver _driver;
        private IWait<IWebDriver> _wait;

        [SetUp]
        public void SetUp()
        {
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            driverService.HideCommandPromptWindow = true;
            driverService.SuppressInitialDiagnosticInformation = true;

            var options = new FirefoxOptions();
            options.SetPreference("javascript.enabled", true);
            options.SetLoggingPreference(LogType.Browser, LogLevel.Warning);

            _driver = new FirefoxDriver(driverService, options, TimeSpan.FromSeconds(60));
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {            
            _driver.Quit();
        }

        [TestCase, Ignore("too simple")]
        public void ShouldNavigateToWebSite()
        {
            _driver.Navigate().GoToUrl("https://justwatches.ru");
            
            Assert.AreEqual(
                "Justwatches — интернет-магазин наручных часов в Санкт-Петербурге", 
                _driver.Title);
        }

        [TestCase]
        public void ShouldBuyProduct()
        {
            // Open web site            
            _driver.Navigate().GoToUrl("https://justwatches.ru");

            // Open brand menu
            var homePage = new HomePage(_driver);
            var menuItems = homePage.MainMenu.OpenMenu("Бренды");

            // Click on Swiss Military image
            menuItems.First(item => item.Text == "Swiss Military").Image.Click();
            //_wait.Until(ExpectedConditions.UrlContains("swiss-military"));
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.name>a")));

            // Click on first product label
            var manufacturerPage = new ManufacturerPage(_driver);
            manufacturerPage.GetProductCards().First().Title.Click();
            //_wait.Until(ExpectedConditions.UrlContains("watch"));
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("#button-cart")));

            // Click on cart button
            var productPage = new ProductPage(_driver);
            for (var i = 0; i < 3; i++)
            {
                try
                {
                    productPage.AddToCartButton.Click();
                    _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.mcartdiv")));
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    continue;
                }
            }

            // Click on order button
            productPage.SubmitOrderButton.Click();
            _wait.Until(ExpectedConditions.UrlContains("fast-order"));

            // Populate payment form
            var orderPage = new OrderPage(_driver);
            orderPage.FirstNameInput.SendKeys("Владимир");
            orderPage.LastNameInput.SendKeys("Владимирович");
            orderPage.EmailInput.SendKeys("stateduma@.ru");
            orderPage.TelephoneInput.SendKeys("88002002316");
            orderPage.ZoneSelect.SelectByText("Москва");
            orderPage.CityInput.SendKeys("Москва");
            orderPage.AddressInput.SendKeys("Кремль, к1");
            orderPage.ShippingAgreementCheckbox.Click();
            orderPage.PrivacyAgrementCheckbox.Click();

            //orderPage.SubmitOrderButton.Click();
            //_wait.Until(ExpectedConditions.UrlContains("route=checkout/success"));
        }

        public void WaitForPageToLoad()
        {
            _wait.Until(DocumentIsReady);
        }

        public bool DocumentIsReady(IWebDriver driver)
        {
            return driver.ExecuteJavaScript<string>("return document.readyState") == "complete";
        }

        public long ActiveAjaxCalls(IWebDriver driver)
        {
            return (long)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active");
        }

        public void MaximizeWindow()
        {
            var availableWidth = _driver.ExecuteJavaScript<long>("return window.screen.availWidth");
            var availableHeight = _driver.ExecuteJavaScript<long>("return window.screen.availHeight");

            _driver.Manage().Window.Size = new System.Drawing.Size((int)availableWidth, (int)availableHeight);
        }

        private void TakeScreenshotOnException()
        {
            _driver.TakeScreenshot().SaveAsFile("C:\\error.png", ScreenshotImageFormat.Png);
        }
    }
}
