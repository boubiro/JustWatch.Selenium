using System.Collections.Generic;
using System.Linq;
using JustWatch.Selenium.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Pages
{
    public class ProductPage : PageBase
    {
        public ProductPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "#button-cart")]
        public IWebElement AddToCartButton { get; set; }

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
