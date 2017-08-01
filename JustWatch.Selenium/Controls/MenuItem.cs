﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Controls
{
    public class MenuItem : ControlBase
    {
        public MenuItem(IWebDriver webDriver, IWebElement webElement) :
            base(webDriver, webElement)
        {

        }

        [FindsBy(How = How.CssSelector, Using = "a.megamenu-parent-img img")]
        public IWebElement Image { get; set; }
        
        [FindsBy(How = How.CssSelector, Using = "a.megamenu-parent-title")]
        public IWebElement Title { get; set; }

        public string Text
        {
            get
            {
                return Image.GetAttribute("title");
            }
        }
    }
}
