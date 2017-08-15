using System;
using System.Linq;
using JustWatch.Selenium.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using JustWatch.Selenium.Extensions;

namespace JustWatch.Selenium.Tests
{
    public class SiteNavigator
    {
        private readonly IWebDriver _driver;
        private readonly IWait<IWebDriver> _wait;

        private PageBase _currentPage;

        public SiteNavigator(IWebDriver webDriver)
        {
            _driver = webDriver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            _currentPage = new HomePage(webDriver);
        }

        public HomePage OpenHomePage()
        {
            _driver.Navigate().GoToUrl(AppSettings.WebSiteUrl);

            return (HomePage) (_currentPage = new HomePage(_driver));
        }

        public ManufacturerPage OpenManufacturerPage(string manufacturer)
        {
            var menuItems = _currentPage.Menu.OpenMenu("Бренды");

            _wait.Until(ExpectedConditions.ElementIsVisible(
                By.CssSelector($"li.megamenu-parent-block a.megamenu-parent-img img[title=\"{manufacturer}\"]")));

            var menuItem = menuItems.FirstOrDefault(item => item.Image.GetAttribute("title") == manufacturer);

            if (menuItem == null)
                throw new Exception($"Could not find item for manufacturer '{manufacturer}' in brands menu");

            menuItem.Image.Click();

            return (ManufacturerPage)(_currentPage = ManufacturerPage.WaitForPage(_driver));
        }

        public ProductPage OpenProductPage(string product)
        {
            var manufacturerPage = _currentPage as ManufacturerPage;

            if (manufacturerPage == null)
                throw new Exception("Could not open product page from page without product list");

            if (!manufacturerPage.HasProducts)
                throw new Exception("There is no products on manufacturer page");

            var productCards = manufacturerPage.GetProductCards();

            var productBlock = productCards.FirstOrDefault(x => x.Title.GetInnerHtml().StartsWith(product));
            if (productBlock == null)
                throw new Exception($"Could not find product '{product}' on manufacturer page");

            productBlock.Title.Click();

            return (ProductPage)(_currentPage = ProductPage.WaitForPage(_driver));
        }

        public InformationPage OpenInformationPage(string title)
        {
            var menuItems = _currentPage.Menu.OpenMenu("Информация");

            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div.dropdown-menu ul li.info-href")));

            var menuItem = menuItems.FirstOrDefault(item => item.Title.GetInnerHtml() == title);
            if (menuItem == null)
                throw new Exception($"Could not find menu with title '{title}' on mainmenu.");

            menuItem.Title.Click();

            return (InformationPage)(_currentPage = InformationPage.WaitForPage(_driver));
        }

        public CategoryPage OpenCategoryPage(string category)
        {
            var menuItems = _currentPage.Menu.OpenMenu("Мужские часы");

            var menuItem = menuItems.FirstOrDefault(item => item.Title.GetInnerHtml() == category);
            if (menuItem == null)
                throw new Exception($"Could not find menu with title '{category}' on mainmenu.");

            menuItem.Title.Click();

            return (CategoryPage)(_currentPage = CategoryPage.WaitForPage(_driver));
        }

        public SalePage OpenSalePage()
        {
            _currentPage.Menu.GetMenuButtons().Single(menu => menu.Title == "Распродажа").Image.Click();

            return (SalePage) (_currentPage = SalePage.WaitForPage(_driver));
        }
    }
}