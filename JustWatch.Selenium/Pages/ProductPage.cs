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

        public bool CanAddProductToCart => AddToCartButton.GetAttribute("id") == "button-cart";

        [FindsBy(How = How.CssSelector, Using = "div.cart a#button-cart, div.cart a.button:not(#button-cart)")]
        public IWebElement AddToCartButton { get; set; }

        public IWebElement SubmitOrderButton => _webDriver.FindElements(By.CssSelector("a.testbutton")).ElementAt(0);

        public IWebElement ContinueShoppingButton => _webDriver.FindElements(By.CssSelector("a.testbutton")).ElementAt(1);
    }
}
