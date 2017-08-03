using System.Linq;
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
            get { return _webDriver.FindElements(By.CssSelector("#button-cart")).Any(); }
        }
        
        public IWebElement AddToCartButton
        {
            get { return _webDriver.FindElement(By.CssSelector("#button-cart")); }
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
