using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Reflection;

namespace BlazorCrud.Tests.E2E
{
    [TestClass]
    public class UITest
    {
        private IWebDriver driver;
        private TestContext testContextInstance;

        public UITest() { }

        // Gets or sets the test context, which provides information about the current test run
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        [TestInitialize()]
        public void SetupTest()
        {
            var chromeOptions = new ChromeOptions();
            // chromeOptions.AddArgument("--headless");

            if (Environment.GetEnvironmentVariable("ChromeWebDriver") != null)
            {
                driver = new ChromeDriver(Environment.GetEnvironmentVariable("ChromeWebDriver"), chromeOptions);
            }
            else
            {
                driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions);
            }
            driver.Manage().Window.FullScreen();
        }

        [TestMethod()]
        public void ClickLoginButtonRoutesToLoginPage()
        {             
            using (driver)
            {
                driver.Navigate().GoToUrl("https://becksblazor.azurewebsites.net/index.html");
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Login")));
                IWebElement LoginLink = driver.FindElement(By.PartialLinkText("Login"));
                LoginLink.Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitClick.Until(d => d.Url.Equals("https://becksblazor.azurewebsites.net/user/login"));
                Assert.AreEqual(driver.Url, "https://becksblazor.azurewebsites.net/user/login");
            }
        }

        [TestMethod()]
        public void ClickDashboardButtonRoutesToDashboardPage()
        {
            using (driver)
            {
                driver.Navigate().GoToUrl("https://becksblazor.azurewebsites.net/index.html");
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Dashboard")));
                IWebElement DashboardLink = driver.FindElement(By.PartialLinkText("Dashboard"));
                DashboardLink.Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitClick.Until(d => d.Url.Equals("https://becksblazor.azurewebsites.net/dashboard"));
                Assert.AreEqual(driver.Url, "https://becksblazor.azurewebsites.net/dashboard");
            }
        }

        [TestCleanup()]
        public void CleanupTest()
        {
            driver.Quit();
        }
    }
}