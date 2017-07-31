using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Pages
{
    public abstract class PageBase
    {
        protected readonly IWebDriver _webDriver;

        protected PageBase(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            PageFactory.InitElements(webDriver, this);
        }
    }
}
