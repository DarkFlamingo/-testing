using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;
using System.Linq;
using System.Diagnostics;

namespace EPICENTERPageTests
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _searchGoogleInputButton = By.XPath("//input[@name='q']");
        private readonly By _hrefInputButton = By.XPath("//*[@id='rso']/div[1]/div/div/div/div/div/div[1]/a");
        private readonly By _contactInputButton = By.XPath("/html/body/footer/div[2]/div/div[2]/div/nav/a[3]");
        private readonly By _contactInformation = By.XPath("/html/body/main/div[2]/div/section/h3");

        private const string _googleSearch = "Епіцентр";
        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            //driver.Navigate().GoToUrl("https://epicentrk.ua/");
            driver.Navigate().GoToUrl("http://google.com/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var searchGoogle = driver.FindElement(_searchGoogleInputButton);
            searchGoogle.SendKeys(_googleSearch);
            searchGoogle.SendKeys(Keys.Enter);

            var hrefInputButton = driver.FindElement(_hrefInputButton);
            hrefInputButton.Click();

            var contact = driver.FindElement(_contactInputButton);
            contact.Click();

            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(1));
            var contactInformation = driver.FindElement(_contactInformation);

            if (contactInformation.Text.Contains("07:30 до 22:30"))
            {
                Console.WriteLine("Працює в 07:30 до 22:30");
                Assert.Pass("Працює в 07:30 до 22:30");
            }
            Assert.Fail("Інший час");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}