using System;
using JustWatch.Selenium.Extensions;
using JustWatch.Selenium.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Tests
{
    public class SalePage : PageBase
    {
        public static SalePage WaitForPage(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementExists(
                PageObjectExtensions.GetElementLocator<ManufacturerPage>(x => x.Breadcrumb)));

            wait.Until(ExpectedConditions.ElementExists(
                PageObjectExtensions.GetElementLocator<ProductPage>(x => x.PhoneCallButton)));

            return new SalePage(driver);
        }

        private SalePage(IWebDriver webDriver) : base(webDriver)
        {
        }
    }
}