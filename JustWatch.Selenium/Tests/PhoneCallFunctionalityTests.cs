using JustWatch.Selenium.Pages;
using JustWatch.Selenium.Extensions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Tests
{
    [TestFixture, Category("Phone call functionality")]
    public class PhoneCallFunctionalityTests : TestsBase
    {
        [TestCase]
        public void ShouldBeAbleToMakePhoneCallRequestFromHomePage()
        {
            var page = new HomePage(_driver);

            var phoneCallPopup = page.OpenPhoneCallPopup();

            phoneCallPopup.NameImput.SendKeys("Владимир Владимирович");
            phoneCallPopup.PhoneInput.SendKeys("89312853680");
            phoneCallPopup.CommentInput.SendKeys("Я хочу проконсультироваться по поводу нужной мне модели");
            phoneCallPopup.TermCheckbox.Click();

            if (!SystemRuntime.IsDebug())
            {
                phoneCallPopup.SubmitButton.Click();

                Assert.IsEmpty(phoneCallPopup.GetErrorMessages());

                _wait.Until(
                    ExpectedConditions.ElementExists(By.CssSelector("div#popup-call-phone-wrapper>div.popup-center>p")),
                    "Popup with successfull phone call request was not displayed");
            }

            phoneCallPopup.ClosePopup();
        }

        [TestCase]
        public void ShouldBeAbleToOpenPhoneCallPopupFromManufacturerPage()
        {
            var siteNavigator = new SiteNavigator(_driver);

            var manufacturerPage = siteNavigator.OpenManufacturerPage("Swiss Military");

            var phoneCallPopup = manufacturerPage.OpenPhoneCallPopup();

            phoneCallPopup.ClosePopup();
        }

        [TestCase]
        public void ShouldBeAbleToOpenPhoneCallPopupFromCategoryPage()
        {
            var siteNavigator = new SiteNavigator(_driver);

            var categoryPage = siteNavigator.OpenCategoryPage("Немецкие");

            var phoneCallPopup = categoryPage.OpenPhoneCallPopup();

            phoneCallPopup.ClosePopup();
        }

        [TestCase]
        public void ShouldBeAbleToOpenPhoneCallPopupFromProductPage()
        {
            var siteNavigator = new SiteNavigator(_driver);

            siteNavigator.OpenManufacturerPage("Swiss Military");

            var productPage = siteNavigator.OpenProductPage("Swiss Military SMP36010.09");

            var phoneCallPopup = productPage.OpenPhoneCallPopup();

            phoneCallPopup.ClosePopup();
        }

        [TestCase]
        public void ShouldBeAbleToOpenPhoneCallPopupFromInformationPage()
        {
            var siteNavigator = new SiteNavigator(_driver);

            var informationPage = siteNavigator.OpenInformationPage("Гарантия и возврат");

            var phoneCallPopup = informationPage.OpenPhoneCallPopup();

            phoneCallPopup.ClosePopup();
        }

        [TestCase]
        public void ShouldBeAbleToOpenPhoneCallPopupFromSalePage()
        {
            var siteNavigator = new SiteNavigator(_driver);

            var salePage = siteNavigator.OpenSalePage();

            var phoneCallPopup = salePage.OpenPhoneCallPopup();

            phoneCallPopup.ClosePopup();
        }
    }
}
