using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;

namespace JustWatch.Selenium.Pages
{
    public abstract class PageBase
    {
        protected readonly IWebDriver _webDriver;

        protected PageBase(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            MainMenu = new MainMenu(webDriver);

            PageFactory.InitElements(webDriver, this);
        }

        public MainMenu MainMenu { get; private set; }

        public string Title { get { return _webDriver.Title; } }
    }
}
