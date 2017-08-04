using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using JustWatch.Selenium.Pages;
using JustWatch.Selenium.Extensions;
using JustWatch.Selenium.Utils;
using System.Linq;
using System;

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
            PageBase currentPage = new HomePage(_driver);

            ManufacturerPage manufacturerPage = null;
            ProductPage productPage = null;
            
            for (int i = 0; i < 10; i++)
            {
                currentPage = manufacturerPage = OpenRandomManufacturerPage(currentPage);

                if (!manufacturerPage.HasProducts)
                {
                    manufacturerPage = null;
                    continue;
                }

                productPage = OpenRandomProductPage(manufacturerPage);

                if (!productPage.CanAddProductToCart)
                {
                    productPage = null;
                    continue;
                }

                break;
            }

            if (manufacturerPage == null || productPage == null)
                throw new Exception("Could not find available manufacturer and product");
            
            var orderPage = OrderProductOnProductPage(productPage);

            Assert.AreEqual("Оформление заказа", orderPage.Title);
            Assert.AreEqual("Оформление заказа", orderPage.Header.GetInnerHtml());

            PopulateOrderForm(orderPage);

            // ExcuteOrderOnOrderPage(orderPage);
        }

        public ManufacturerPage OpenRandomManufacturerPage(PageBase currentPage)
        {
            // Open brand menu
            var menuItems = currentPage.MainMenu.OpenMenu("Бренды");
            _wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector("li.megamenu-parent-block a.megamenu-parent-img img[title=\"Swiss Military\"]")));

            // Click on Swiss Military image
            randomSelector.Select(menuItems).Title.Click();

            _wait.Until(ExpectedConditions.ElementExists(
                 PageObjectExtensions.GetElementLocator<ManufacturerPage>(x => x.Breadcrumb)));

            return new ManufacturerPage(_driver);
        }

        public ProductPage OpenRandomProductPage(ManufacturerPage manufacturerPage)
        {
            if (!manufacturerPage.HasProducts)
                throw new Exception("There is no products on manufacturer page");

            var productCards = manufacturerPage.GetProductCards();

            randomSelector.Select(productCards).Title.Click();

            _wait.Until(ExpectedConditions.ElementExists(
                PageObjectExtensions.GetElementLocator<ProductPage>(x => x.AddToCartButton)));

            return new ProductPage(_driver);
        }

        private OrderPage OrderProductOnProductPage(ProductPage productPage)
        {
            for (var i = 0; i < 2; i++)
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

            return new OrderPage(_driver);
        }

        private void PopulateOrderForm(OrderPage orderPage)
        {
            orderPage.FirstNameInput.SendKeys("Владимир");
            orderPage.LastNameInput.SendKeys("Владимирович");
            orderPage.EmailInput.SendKeys("stateduma@.ru");
            orderPage.TelephoneInput.SendKeys("88002002316");
            orderPage.ZoneSelect.SelectByText("Москва");
            orderPage.CityInput.SendKeys("Москва");
            orderPage.AddressInput.SendKeys("Кремль, к1");
            orderPage.ShippingAgreementCheckbox.Click();
            orderPage.PrivacyAgrementCheckbox.Click();
        }

        private void ExcuteOrderOnOrderPage(OrderPage orderPage)
        {
            orderPage.SubmitOrderButton.Click();
            _wait.Until(ExpectedConditions.UrlContains("route=checkout/success"));
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
