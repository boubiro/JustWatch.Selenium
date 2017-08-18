using System;
using System.Linq;
using JustWatch.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Pages
{
    public class ProductPage : PageBase
    {
        public static ProductPage WaitForPage(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementExists(
                PageObjectExtensions.GetElementLocator<ProductPage>(x => x.AddToCartButton)));

            wait.Until(ExpectedConditions.ElementExists(
                PageObjectExtensions.GetElementLocator<ProductPage>(x => x.PhoneCallButton)));

            return new ProductPage(driver);
        }

        protected ProductPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "ul.breadcrumb")]
        public IWebElement Breadcrumb { get; set; }

        public bool CanAddProductToCart => AddToCartButton.GetAttribute("id") == "button-cart";

        [FindsBy(How = How.CssSelector, Using = "div.cart a#button-cart, div.cart a.button:not(#button-cart)")]
        public IWebElement AddToCartButton { get; set; }

        public IWebElement SubmitOrderButton => _webDriver.FindElements(By.CssSelector("a.testbutton")).ElementAt(0);

        public IWebElement ContinueShoppingButton => _webDriver.FindElements(By.CssSelector("a.testbutton")).ElementAt(1);
    }
}
