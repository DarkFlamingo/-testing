using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;
using System.Linq;
using System.Diagnostics;

namespace OwnPageTests
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _searchStartPageInputButton = By.XPath("//input[@name='q']");
        private readonly By _timeSpan = By.XPath("/html/body/div[1]/div[1]/header/div[2]/div/div[1]/div[1]");
        private readonly By _selectHref = By.XPath("//*[@id='rso']/div[1]/div/div/div/div/div/div[1]/a/h3");

        private const string _startPageSearch = "makeup";

        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            //driver.Navigate().GoToUrl("https://www.startpage.com/sp/search");
            driver.Navigate().GoToUrl("https://www.google.com.ua/");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var searchStartPage= driver.FindElement(_searchStartPageInputButton);
            searchStartPage.SendKeys(_startPageSearch);
            searchStartPage.SendKeys(Keys.Enter);
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));

            var selectHref = driver.FindElement(_selectHref);
            selectHref.Click();
            System.Threading.Thread.Sleep(timeout: TimeSpan.FromSeconds(2));

            var timeSpan = driver.FindElement(_timeSpan);
            if (timeSpan.Text.Contains("с 7:55 до 20:02"))
            {
                Console.WriteLine("Правильний час");
                Assert.Pass("Правильний час");
            }
            Assert.Fail("Магазин працює в інший час");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}