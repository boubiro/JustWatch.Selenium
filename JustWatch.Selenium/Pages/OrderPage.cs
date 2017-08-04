using System;
using JustWatch.Selenium.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Pages
{
    public class OrderPage : PageBase
    {
        public static OrderPage WaitForPage(IWebDriver driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            wait.Until(ExpectedConditions.ElementExists(
               PageObjectExtensions.GetElementLocator<OrderPage>(x => x._zoneInput)));

            return new OrderPage(driver);
        }

        [FindsBy(How = How.CssSelector, Using = "select#input-payment-zone")]
        private IWebElement _zoneInput;

        protected OrderPage(IWebDriver webDriver) : base(webDriver)
        {
            ZoneSelect = new SelectElement(_zoneInput);
        }

        [FindsBy(How = How.CssSelector, Using = "h1.category-header")]
        public IWebElement Header { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#input-payment-firstname")]
        public IWebElement FirstNameInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#input-payment-lastname")]
        public IWebElement LastNameInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#input-payment-email")]
        public IWebElement EmailInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#input-payment-telephone")]
        public IWebElement TelephoneInput { get; set; }

        public SelectElement ZoneSelect { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#input-payment-city")]
        public IWebElement CityInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#input-payment-address-1")]
        public IWebElement AddressInput { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name=\"agree\"]")]
        public IWebElement ShippingAgreementCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input[name=\"payment_agree\"]")]
        public IWebElement PrivacyAgrementCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "input#button-go")]
        public IWebElement SubmitOrderButton { get; set; }
    }
}
