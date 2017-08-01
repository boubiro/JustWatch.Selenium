using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace JustWatch.Selenium.Controls
{
    public class MenuButton : ControlBase
    {
        public MenuButton(IWebDriver webDriver, IWebElement webElement) :
            base(webDriver, webElement)
        {

        }

        [FindsBy(How = How.CssSelector, Using = "a.dropdown-toggle")]
        public IWebElement Link { get; set; }

        [FindsBy(How = How.CssSelector, Using = "img.megamenu-thumb")]
        public IWebElement Image { get; set; }

        public string Title
        {
            get
            {
                return Image.GetAttribute("title");
            }
        }
    }
}