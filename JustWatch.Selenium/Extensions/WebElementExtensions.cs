using OpenQA.Selenium;

namespace JustWatch.Selenium.Extensions
{
    public static class WebElementExtensions
    {
        public static string GetInnerHtml(this IWebElement webElement)
        {
            return webElement.GetAttribute("innerHTML");
        } 
    }
}
