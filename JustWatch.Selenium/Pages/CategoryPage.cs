using System;
using JustWatch.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Pages
{
    public class CategoryPage : PageBase
    {
        public static CategoryPage WaitForPage(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementExists(
               PageObjectExtensions.GetElementLocator<ManufacturerPage>(x => x.Breadcrumb)));

            return new CategoryPage(driver);
        }

        private CategoryPage(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}