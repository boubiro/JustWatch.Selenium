using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace JustWatch.Selenium.Pages
{
    public class HomePage : PageBase
    {
        public HomePage(IWebDriver webDriver) : base(webDriver)
        {
            MainMenu = new MainMenu(webDriver);
        }

        public MainMenu MainMenu { get; private set; }
    }
}
