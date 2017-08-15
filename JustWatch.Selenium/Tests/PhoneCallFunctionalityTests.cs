using System;
using JustWatch.Selenium.Pages;
using JustWatch.Selenium.Extensions;
using JustWatch.Selenium.FluentWait;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JustWatch.Selenium.Tests
{
    [TestFixture, Category("Recall functionality")]
    public class PhoneCallFunctionalityTests : TestsBase
    {
        [TestCase]
        public void ShouldBeAbleToMakePhoneCallRequestFromHomePage()
        {
            var page = new HomePage(_driver);

            var phoneCallPopup = page.OpenCallPopup();

            phoneCallPopup.NameImput.SendKeys("Владимир Владимирович");
            phoneCallPopup.PhoneInput.SendKeys("89312853680");
            phoneCallPopup.CallTimeInput.Click();
            phoneCallPopup.CommentInput.Click();
            phoneCallPopup.CommentInput.SendKeys("Я хочу проконсультироваться по поводу нужной мне модели");
            phoneCallPopup.TermCheckbox.Click();

            if (!SystemRuntime.IsDebug())
            {
                TrySeveralTimes(
                  () => phoneCallPopup.SubmitButton.Click(),
                  ExpectedConditions.ElementExists(By.CssSelector("div#popup-call-phone-wrapper>div.popup-center>p")),
                  2,
                  "Popup with successfull phone call request was not displayed");
            }

            TrySeveralTimes(
                () => phoneCallPopup.CloseButton.Click(),
                FluentCondition.Throws<NoSuchElementException>(
                        ExpectedConditions.ElementExists(By.CssSelector("div#popup-call-phone-wrapper"))).Condition,
                2,
                "Phone call popup should be closed");
        }

        private void TrySeveralTimes<TWaitResult>(
            Action action, 
            Func<IWebDriver, TWaitResult> escapeCondition, 
            int attempts, 
            string errorMessage)
        {
            for (var i = 0; i < attempts; i++)
            {
                action();

                if (_wait.TryToWaitUntil(escapeCondition))
                    break;
            }

            _wait.Until(escapeCondition, errorMessage);
        }
    }
}
