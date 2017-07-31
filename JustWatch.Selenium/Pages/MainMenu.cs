using OpenQA.Selenium;

namespace JustWatch.Selenium.Pages
{
    public class MainMenu
    {
        private readonly IWebDriver _webDriver;

        public MainMenu(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IWebElement GetMainMenuItemByUrl(string url)
        {
            return _webDriver.FindElement(By.CssSelector(
                $"nav#megamenu-menu ul.navbar-nav li.dropdown a[href=\"{url}\"]"));
        }

        public IWebElement GetMenuItemByTitle(string title)
        {
            return _webDriver.FindElement(By.CssSelector(
                $"nav#megamenu-menu ul.navbar-nav li.dropdown a.dropdown-toggle img.megamenu-thumb[title=\"{title}\"]"));
        }

        public IWebElement GetMenuSubItemByTitle(string title)
        {
            return _webDriver.FindElement(
                By.CssSelector($"li.megamenu-parent-block a.megamenu-parent-img img[title=\"{title}\"]"));
        }
    }
}