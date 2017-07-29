using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Linq;

namespace JustWatch.Selenium
{
    [TestFixture]
    public class TestFixture
    {
        private IWebDriver _driver;

        [SetUp]
        public void SetUp()
        {
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.FirefoxBinaryPath = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";
            driverService.HideCommandPromptWindow = true;
            driverService.SuppressInitialDiagnosticInformation = true;
            _driver = new FirefoxDriver(driverService, new FirefoxOptions(), TimeSpan.FromSeconds(60));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }

        [TestCase]
        public void ShouldNavigateToWebSite()
        {
            _driver.Navigate().GoToUrl("https://justwatches.ru");
            
            Assert.AreEqual(
                "Justwatches — интернет-магазин наручных часов в Санкт-Петербурге", 
                _driver.Title);            
        }

        [TestCase, Ignore]
        public void ShouldBuyProduct()
        {
            // Open web site
            _driver.Navigate().GoToUrl("https://justwatches.ru");

            // Open brand menu
            var manufacturersMenu = _driver.FindElement(By.CssSelector("nav#megamenu-menu ul.navbar-nav li.dropdown a[href=\"/brands/\"]"));
            var manufacturerImage = _driver.FindElement(By.CssSelector("li.megamenu-parent-block a.megamenu-parent-img img[title=\"Swiss Military\"]"));
            new Actions(_driver).MoveToElement(manufacturersMenu).Perform();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("li.megamenu-parent-block a.megamenu-parent-img img[title=\"Swiss Military\"]")));

            // Click on Swiss Military image
            manufacturerImage.Click();
            wait.Until(ExpectedConditions.StalenessOf(manufacturersMenu));

            // Click on first product label
            var productLabel = _driver.FindElements(By.CssSelector("div.product>div.product-about>div.name>a")).FirstOrDefault();
            productLabel.Click();
            wait.Until(ExpectedConditions.StalenessOf(productLabel));

            // Click on cart button
            var button = _driver.FindElement(By.CssSelector("a#button-cart"));
            button.Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.mcartdiv")));

            // Click on order button
            var orderButton = _driver.FindElements(By.CssSelector("div.mcartdiv a.testbutton")).FirstOrDefault();
            orderButton.Click();
            wait.Until(ExpectedConditions.StalenessOf(orderButton));

            // Populate payment form
            _driver.FindElement(By.CssSelector("input#input-payment-firstname")).SendKeys("Владимир");
            _driver.FindElement(By.CssSelector("input#input-payment-lastname")).SendKeys("Владимирович");
            _driver.FindElement(By.CssSelector("input#input-payment-email")).SendKeys("stateduma@.ru");
            _driver.FindElement(By.CssSelector("input#input-payment-telephone")).SendKeys("88002002316");
            new SelectElement(_driver.FindElement(By.CssSelector("select#input-payment-zone"))).SelectByText("Москва");
            _driver.FindElement(By.CssSelector("input#input-payment-city")).SendKeys("Москва");
            _driver.FindElement(By.CssSelector("input#input-payment-address-1")).SendKeys("Кремль, к1");
            _driver.FindElement(By.CssSelector("input[name=\"agree\"]")).Click();
            _driver.FindElement(By.CssSelector("input[name=\"payment_agree\"]")).Click();
            
            //_driver.FindElement(By.CssSelector("button#button-go")).Click();
        }
    }
}
