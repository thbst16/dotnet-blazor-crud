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
        // private const string ENV_URL = "https://localhost:44377";
        private const string ENV_URL = "https://becksblazor.azurewebsites.net";

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
                driver.Navigate().GoToUrl(ENV_URL);
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Login")));
                driver.FindElement(By.PartialLinkText("Login")).Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitClick.Until(d => d.Url.Equals(ENV_URL+ "/user/login"));
                System.Threading.Thread.Sleep(1000);
                Assert.AreEqual(driver.Url, ENV_URL + "/user/login");
            }
        }

        [TestMethod()]
        public void ClickDashboardButtonRoutesToDashboardPage()
        {
            using (driver)
            {
                driver.Navigate().GoToUrl(ENV_URL);
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Dashboard")));
                driver.FindElement(By.PartialLinkText("Dashboard")).Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitClick.Until(d => d.Url.Equals(ENV_URL + "/dashboard"));
                System.Threading.Thread.Sleep(1000);
                Assert.AreEqual(driver.Url, ENV_URL + "/dashboard");
            }
        }

        [TestMethod()]
        public void PerformPatientSearchReturnsPatients()
        {
            using (driver)
            {
                driver.Navigate().GoToUrl(ENV_URL);
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Patients")));
                driver.FindElement(By.PartialLinkText("Patients")).Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitClick.Until(d => d.Url.Equals(ENV_URL + "/patient/list/1"));
                driver.FindElement(By.Name("PatientSearchInput")).SendKeys("br");
                driver.FindElement(By.Name("PatientSearchButton")).Click();
                var waitTest = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitTest.Until(d => d.PageSource.Contains("<td>Broderick Shields</td>"));
                System.Threading.Thread.Sleep(1000);
                Assert.IsTrue(driver.PageSource.Contains("<td>Brad Weimann</td>"));
                Assert.IsTrue(driver.PageSource.Contains("<td>Bradly Legros</td>"));
                Assert.IsTrue(driver.PageSource.Contains("<td>Patrick Oberbrunner</td>"));
            }
        }

        [TestMethod()]
        public void PerformOrganizationSearchReturnsOrganizations()
        {
            using (driver)
            {
                driver.Navigate().GoToUrl(ENV_URL);
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Organizations")));
                driver.FindElement(By.PartialLinkText("Organizations")).Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitClick.Until(d => d.Url.Equals(ENV_URL + "/organization/list/1"));
                driver.FindElement(By.Name("OrganizationSearchInput")).SendKeys("sa");
                driver.FindElement(By.Name("OrganizationSearchButton")).Click();
                var waitTest = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitTest.Until(d => d.PageSource.Contains("<td>Satterfield, Auer and Jast</td>"));
                System.Threading.Thread.Sleep(1000);
                Assert.IsTrue(driver.PageSource.Contains("<td>Sauer Group</td>"));
            }
        }

        [TestMethod()]
        public void PerformClaimsSearchReturnsClaims()
        {
            using (driver)
            {
                driver.Navigate().GoToUrl(ENV_URL);
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromMinutes(10));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Claims")));
                driver.FindElement(By.PartialLinkText("Claims")).Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitClick.Until(d => d.Url.Equals(ENV_URL + "/claim/list/1"));
                driver.FindElement(By.Name("ClaimSearchInput")).SendKeys("bre");
                driver.FindElement(By.Name("ClaimSearchButton")).Click();
                var waitTest = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                waitTest.Until(d => d.PageSource.Contains("<td>Brekke, Yundt and Spencer</td>"));
                System.Threading.Thread.Sleep(1000);
                Assert.IsTrue(driver.PageSource.Contains("<td>Breitenberg Inc</td>"));
                Assert.IsTrue(driver.PageSource.Contains("<td>Brent Tillman</td>"));
                Assert.IsTrue(driver.PageSource.Contains("<td>Brenden Roob</td>"));
            }
        }

        [TestCleanup()]
        public void CleanupTest()
        {
            driver.Quit();
        }
    }
}
