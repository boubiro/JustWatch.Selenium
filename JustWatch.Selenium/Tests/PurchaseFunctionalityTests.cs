using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using JustWatch.Selenium.Pages;
using JustWatch.Selenium.Extensions;
using JustWatch.Selenium.Utils;

namespace JustWatch.Selenium.Tests
{
    [TestFixture, Category("Purchase functionality")]
    public class PurchaseFunctionalityTests : TestsBase
    {
        private readonly RandomSelector randomSelector = new RandomSelector();

        [TestCase, Ignore("too simple")]
        public void ShouldNavigateToWebSite()
        {            
            Assert.AreEqual(
                "Justwatches — интернет-магазин наручных часов в Санкт-Петербурге", 
                _driver.Title);
        }

        [TestCase]
        public void ShouldBeAbleToBuyRandomProduct()
        {
            // Open brand menu
            var homePage = new HomePage(_driver);
            var menuItems = homePage.MainMenu.OpenMenu("Бренды");
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("li.megamenu-parent-block a.megamenu-parent-img img[title=\"Swiss Military\"]")));

            // Click on Swiss Military image
            randomSelector.Select(menuItems).Title.Click();
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("div.content div#res-products")));

            // Click on first product label
            var manufacturerPage = new ManufacturerPage(_driver);
            var productCards = manufacturerPage.GetProductCards();
            randomSelector.Select(productCards).Title.Click();
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
            _wait.Until(ExpectedConditions.ElementExists(By.CssSelector("select#input-payment-zone")));

            // Populate payment form
            var orderPage = new OrderPage(_driver);

            Assert.AreEqual("Оформление заказа", orderPage.Title);
            Assert.AreEqual("Оформление заказа", orderPage.Header.GetInnerHtml());

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
