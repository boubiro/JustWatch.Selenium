using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace JustWatch.Selenium
{
    public class FirefoxDriverFactory
    {
        public string FirefoxBinaryPath { get; set; } = @"C:\Program Files (x86)\Mozilla Firefox\firefox.exe";

        public TimeSpan CommandTimeout { get; set; } = TimeSpan.FromSeconds(60);

        public IWebDriver Create()
        {
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.FirefoxBinaryPath = FirefoxBinaryPath;
            driverService.HideCommandPromptWindow = true;
            driverService.SuppressInitialDiagnosticInformation = true;

            var options = new FirefoxOptions();
            options.SetPreference("javascript.enabled", true);
            options.SetLoggingPreference(LogType.Browser, LogLevel.Warning);

            return new FirefoxDriver(driverService, options, CommandTimeout);
        }
    }
}
