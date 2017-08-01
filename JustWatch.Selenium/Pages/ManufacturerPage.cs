using System.Collections.Generic;
using System.Linq;
using JustWatch.Selenium.Controls;
using OpenQA.Selenium;

namespace JustWatch.Selenium.Pages
{
    public class ManufacturerPage : PageBase
    {
        public ManufacturerPage(IWebDriver webDriver) : base(webDriver)
        {
        }

        public IEnumerable<ProductCard> GetProductCards()
        {
            return _webDriver.FindElements(By.CssSelector("div.content div#res-products div.product"))
                .Select(element => new ProductCard(_webDriver, element));
        }
    }
}
