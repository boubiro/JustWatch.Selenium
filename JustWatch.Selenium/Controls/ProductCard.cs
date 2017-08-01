using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Controls
{
    public class ProductCard : ControlBase
    {
        public ProductCard(IWebDriver webDriver, IWebElement webElement) 
            : base(webDriver, webElement)
        {
        }

        [FindsBy(How = How.CssSelector, Using = "div.product-about a")]
        public IWebElement Title { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.product-about div.price span")]
        public IWebElement Price { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div.image a.hover-image img")]
        public IWebElement Image { get; set; }
    }
}
