using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using JustWatch.Selenium.Controls;
using JustWatch.Selenium.Extensions;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Pages
{
    public abstract class PageBase
    {
        protected readonly IWebDriver _webDriver;

        protected PageBase(IWebDriver webDriver)
        {
            _webDriver = webDriver;

            Menu = new MainMenu(webDriver);

            PageFactory.InitElements(webDriver, this);
        }

        public MainMenu Menu { get; private set; }

        public string Title { get { return _webDriver.Title; } }

        [FindsBy(How = How.CssSelector, Using = "a#uptocall-mini")]
        public IWebElement PhoneCallButton { get; set; }

        public PhoneCallPopup OpenPhoneCallPopup()
        {
            PhoneCallButton.Click();

            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20));

            wait.Until(
                ExpectedConditions.ElementIsVisible(By.CssSelector("div#popup-call-phone-wrapper")),
                "Phone call popun was not opened");

            return new PhoneCallPopup(_webDriver, By.CssSelector("div#popup-call-phone-wrapper"));
        }
    }
}
