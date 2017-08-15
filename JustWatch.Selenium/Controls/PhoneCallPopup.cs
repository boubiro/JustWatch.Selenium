using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Controls
{
    public class PhoneCallPopup : ControlBase
    {
        public PhoneCallPopup(IWebDriver webDriver, By selector) : base(webDriver, selector)
        {
        }

        public PhoneCallPopup(IWebDriver webDriver, IWebElement webElement) : base(webDriver, webElement)
        {
        }

        [FindsBy(How = How.Name, Using = "name")]
        public IWebElement NameImput { get; set; }

        [FindsBy(How = How.Name, Using = "telephone")]
        public IWebElement PhoneInput { get; set; }

        [FindsBy(How = How.Name, Using = "time")]
        public IWebElement CallTimeInput { get; set; }

        [FindsBy(How = How.Name, Using = "comment")]
        public IWebElement CommentInput { get; set; }

        [FindsBy(How = How.Name, Using = "terms")]
        public IWebElement TermCheckbox { get; set; }

        [FindsBy(How = How.CssSelector, Using = "a#popup-send-button")]
        public IWebElement SubmitButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "button.mfp-close")]
        public IWebElement CloseButton { get; set; }
    }
}

