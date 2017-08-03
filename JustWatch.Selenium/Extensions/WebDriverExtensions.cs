using System.Linq;
using OpenQA.Selenium;

namespace JustWatch.Selenium.Extensions
{
    public static class WebDriverExtensions
    {
        public static bool ElementExists(this IWebDriver webDriver, By locator)
        {
            return webDriver.FindElements(locator).Any();
        }
    }
}
