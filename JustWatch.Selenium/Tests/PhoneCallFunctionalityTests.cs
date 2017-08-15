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
            phoneCallPopup.CallTimeInput.SendKeys("15.08.2017 16:45");
            phoneCallPopup.CommentInput.SendKeys("Я хочу проконсультироваться по поводу нужной мне модели");
            phoneCallPopup.TermCheckbox.Click();

            //if (!SystemRuntime.IsDebug())
            {
                TrySeveralTimes(() => {
                    phoneCallPopup.SubmitButton.Click();

                    Assert.IsEmpty(phoneCallPopup.GetErrorMessages());

                    _wait.Until(
                        ExpectedConditions.ElementExists(By.CssSelector("div#popup-call-phone-wrapper>div.popup-center>p")),
                        "Popup with successfull phone call request was not displayed");
                }, 3);
            }

            TrySeveralTimes(() => {
                phoneCallPopup.CloseButton.Click();
                _wait.Until(
                    FluentCondition.Throws<NoSuchElementException>(
                        ExpectedConditions.ElementExists(By.CssSelector("div#popup-call-phone-wrapper"))).Condition,
                    "Phone call popup should be closed");

            }, 3);
        }

        private void TrySeveralTimes(Action action, int attempts)
        {
            for (var i = 0; i < attempts; i++)
            {
                try
                {
                    action();
                    break;
                }
                catch (Exception)
                {
                    if (i == attempts - 1)
                    {
                        throw;
                    }
                }
            }
        }
    }
}
