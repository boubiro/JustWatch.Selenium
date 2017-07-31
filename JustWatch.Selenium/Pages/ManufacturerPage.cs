using System.Collections.Generic;
using OpenQA.Selenium;

namespace JustWatch.Selenium.Pages
{
    public class ManufacturerPage : PageBase
    {
        public ManufacturerPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public IEnumerable<IWebElement> GetProductTitles()
        {
            return _webDriver.FindElements(By.CssSelector("div.name>a"));
        }
    }
}
