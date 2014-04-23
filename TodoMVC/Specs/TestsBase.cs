using System;
using System.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace TodoMVC
{
    public abstract class TestsBase : IDisposable
    {
        protected IWebDriver driver;
        public void Dispose()
        {
            if (driver != null)
                driver.Quit();
        }

        protected IWebDriver GetSeleniumDriver()
        {
            var url = "http://127.0.0.1:4444/wd/hub";
            var caps = DesiredCapabilities.Chrome();
            caps.SetCapability("name", "todoMVC");
            return new RemoteWebDriver(new Uri(url), caps);
        }

        protected IWebDriver GetSauceConnectDriver(string jobName)
        {
            var key = ConfigurationManager.AppSettings["sauceKey"];
            var user = ConfigurationManager.AppSettings["sauceUser"];
            var url = string.Format("http://{0}:{1}@localhost:4445/wd/hub", user, key);
            var caps = DesiredCapabilities.Chrome();
            caps.SetCapability("name", jobName);

            caps.SetCapability("username", user);
            caps.SetCapability("accessKey", key);

            return new RemoteWebDriver(new Uri(url), caps);
        }
    }
}