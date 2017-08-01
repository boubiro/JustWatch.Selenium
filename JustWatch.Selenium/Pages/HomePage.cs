using System.Collections.Generic;
using OpenQA.Selenium;

namespace JustWatch.Selenium.Pages
{
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public IEnumerable<IWebElement> GetTopPanelLinks()
        {
            return _webDriver.FindElements(By.CssSelector("div.top-panel ul.top-panel-ul li a"));
        }
    }
}
