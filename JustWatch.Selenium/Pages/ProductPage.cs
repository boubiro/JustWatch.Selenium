using System;
using System.Linq;
using JustWatch.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Pages
{
    public class ProductPage : PageBase
    {
        public ProductPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "ul.breadcrumb")]
        public IWebElement Breadcrumb { get; set; }

        public bool CanAddProductToCart
        {
            get
            {
                if (_webDriver.ElementExists(By.CssSelector("div.cart a#button-cart")))
                    return true;

                if (_webDriver.ElementExists(By.CssSelector("div.cart a.button:not(#button-cart)")))
                    return false;

                throw new Exception("Could not determine if product is available");
            }
        }
        
        public IWebElement AddToCartButton
        {
            get { return _webDriver.FindElement(By.CssSelector("div.cart a#button-cart")); }
        }

        public IWebElement AbsentButton
        {
            get { return _webDriver.FindElement(By.CssSelector("div.cart a.button:not(#button-cart)")); }
        }

        public IWebElement SubmitOrderButton
        {
            get { return _webDriver.FindElements(By.CssSelector("a.testbutton")).ElementAt(0); }
        }
        
        public IWebElement ContinueShoppingButton
        {
            get { return _webDriver.FindElements(By.CssSelector("a.testbutton")).ElementAt(1); }
        }
    }
}
