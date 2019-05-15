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
        [TestMethod]
        public void ClickLoginButtonRoutesToLoginPage()
        {
            var chromeOptions = new ChromeOptions();
            // chromeOptions.AddArgument("--headless");

            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), chromeOptions))
            {
                driver.Navigate().GoToUrl("https://becksblazor.azurewebsites.net/index.html");
                var waitLoad = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                waitLoad.Until(d => d.FindElement(By.PartialLinkText("Login")));
                IWebElement LoginLink = driver.FindElement(By.PartialLinkText("Login"));
                LoginLink.Click();
                var waitClick = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                waitClick.Until(d => d.Url.Equals("https://becksblazor.azurewebsites.net/user/login"));
                Assert.AreEqual(driver.Url, "https://becksblazor.azurewebsites.net/user/login");
            }
        }
    }
}