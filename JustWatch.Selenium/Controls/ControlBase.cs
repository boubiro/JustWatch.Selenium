using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Controls
{
    public abstract class ControlBase
    {
        protected readonly IWebDriver _webDriver;
        protected readonly IWebElement _innerElement;

        protected ControlBase(IWebDriver webDriver, IWebElement webElement)
        {
            _webDriver = webDriver;
            _innerElement = webElement;

            PageFactory.InitElements(
                this, 
                new DefaultElementLocator(webElement), 
                new DefaultPageObjectMemberDecorator());
        }

        public IWebElement ToWebElement()
        {
            return _innerElement;
        }
    }
}
