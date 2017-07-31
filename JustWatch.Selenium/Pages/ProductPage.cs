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
    }
}
