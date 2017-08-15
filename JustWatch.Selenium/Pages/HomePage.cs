using System;
using System.Collections.Generic;
using JustWatch.Selenium.Controls;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using JustWatch.Selenium.Extensions;

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

        [FindsBy(How = How.CssSelector, Using = "a#uptocall-mini")]
        public IWebElement CallButton { get; set; }

        public PhoneCallPopup OpenCallPopup()
        {
            CallButton.Click();

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));

            wait.Until(
                ExpectedConditions.ElementIsVisible(By.CssSelector("div#popup-call-phone-wrapper")), 
                "Phone call popun was not opened");

            return new PhoneCallPopup(_webDriver, By.CssSelector("div#popup-call-phone-wrapper"));
        }
    }
}
