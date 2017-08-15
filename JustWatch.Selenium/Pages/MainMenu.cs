using JustWatch.Selenium.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Collections.Generic;
using System.Linq;

namespace JustWatch.Selenium.Pages
{
    public class MainMenu
    {
        private readonly IWebDriver _webDriver;

        public MainMenu(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public IEnumerable<MenuButton> GetMenuButtons()
        {
            return _webDriver.FindElements(By.CssSelector("nav#megamenu-menu ul.navbar-nav>li"))
                .Select(element => new MenuButton(_webDriver, element));
        }

        public IEnumerable<MenuItem> OpenMenu(string title)
        {
            var menuButton = GetMenuButtons().First(button => button.Title == title);

            new Actions(_webDriver).MoveToElement(menuButton.Link).Perform();

            return menuButton.ToWebElement()
                .FindElements(By.CssSelector("div.dropdown-menu ul li"))
                .Select(element => new MenuItem(_webDriver, element));
        }
    }
}