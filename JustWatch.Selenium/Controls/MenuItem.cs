using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Controls
{
    public class MenuItem : ControlBase
    {
        public MenuItem(IWebDriver webDriver, IWebElement webElement) :
            base(webDriver, webElement)
        {

        }

        [FindsBy(How = How.CssSelector, Using = "a img")]
        public IWebElement Image { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "a")]
        public IWebElement Title { get; set; }
    }
}
