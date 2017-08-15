using JustWatch.Selenium.Extensions;
using JustWatch.Selenium.Pages;
using NUnit.Framework;
using System;
using System.Linq;

namespace JustWatch.Selenium.Tests
{
    [TestFixture, Category("Information functionality")]
    public class InformationFunctionalityTests : TestsBase
    {
        [TestCase("О магазине", "О магазине", TestName = "ShouldOpenAboutPageFromTopPanel")]
        [TestCase("Доставка и оплата", "Доставка и оплата", TestName = "ShouldOpenDeliveryPageFromTopPanel")]
        [TestCase("Гарантия и возврат", "Гарантия", TestName = "ShouldOpenWarrantyPageFromTopPanel")]
        public void ShouldOpenInformationPageFromTopPanel(string linkText, string expectedPageHeader)
        {
            var homePage = new HomePage(_driver);

            var panelLinks = homePage.GetTopPanelLinks();

            var panelLink = panelLinks.FirstOrDefault(link => link.GetInnerHtml() == linkText);
            if (panelLink == null)
                throw new Exception($"Could not find link '{linkText}' on top panel.");

            panelLink.Click();

            var informationPage = InformationPage.WaitForPage(_driver);

            var pageHeader = informationPage.Header.GetInnerHtml();

            Assert.AreEqual(expectedPageHeader, pageHeader);
        }

        [TestCase("О магазине", "О магазине", TestName = "ShouldOpenAboutPageFromMainMenu")]
        [TestCase("Доставка и оплата", "Доставка и оплата", TestName = "ShouldOpenDeliveryPageFromMainMenu")]
        [TestCase("Гарантия и возврат", "Гарантия", TestName = "ShouldOpenWarrantyPageFromMainMenu")]
        [TestCase("Публичная оферта", "Публичная оферта", TestName = "ShouldOpenPrivacyPageFromMainMenu")]
        public void ShouldOpenInformationPageFromMainMenu(string menuTitle, string expectedPageHeader)
        {
            var siteNavigator = new SiteNavigator(_driver);

            var informationPage = siteNavigator.OpenInformationPage(menuTitle);

            Assert.AreEqual(expectedPageHeader, informationPage.Header.GetInnerHtml());
        }
    }
}
