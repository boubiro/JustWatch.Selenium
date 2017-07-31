using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace JustWatch.Selenium.Pages
{
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public IWebElement GetMainMenuItemByUrl(string url)
        {
            return  _webDriver.FindElement(By.CssSelector(
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
