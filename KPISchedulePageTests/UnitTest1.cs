using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using System;
using System.Linq;
using System.Diagnostics;

namespace KPISchedulePageTests
{
    public class Tests
    {
        private IWebDriver driver;

        private readonly By _searchGoogleInputButton = By.XPath("//input[@name='q']");
        private readonly By _selectHref = By.XPath("//a[contains(@href, 'http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx')]");
        private readonly By _searchScheduleInputButton = By.XPath("//input[@name='ctl00$MainContent$ctl00$txtboxGroup']");
        private readonly By _searchScheduleButton = By.XPath("//input[@name='ctl00$MainContent$ctl00$btnShowSchedule']");
        private readonly By _firstWeekTable = By.XPath("//table[@id='ctl00_MainContent_FirstScheduleTable']");
        private readonly By _secondWeekTable = By.XPath("//table[@id='ctl00_MainContent_SecondScheduleTable']");

        private const string _googleSearch = "Розклад КПІ";
        private const string _group = "КП-91";
        [SetUp]
        public void Setup()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Navigate().GoToUrl("http://google.com/");
            //driver.Navigate().GoToUrl("http://rozklad.kpi.ua/Schedules/ScheduleGroupSelection.aspx");
            driver.Manage().Window.Maximize();
        }

        [Test]
        public void Test1()
        {
            var searchGoogle = driver.FindElement(_searchGoogleInputButton);
            searchGoogle.SendKeys(_googleSearch);
            searchGoogle.SendKeys(Keys.Enter);

            var selectHref = driver.FindElement(_selectHref);
            selectHref.Click();

            var search = driver.FindElement(_searchScheduleInputButton);
            search.SendKeys(_group);

            var search_btn = driver.FindElement(_searchScheduleButton);
            search_btn.Click();

            var firstWeekTable = driver.FindElement(_firstWeekTable);
            var secondWeekTable = driver.FindElement(_secondWeekTable);

            var trFirstTable = firstWeekTable.FindElements(By.TagName("tr"));
            var trSecondTable = secondWeekTable.FindElements(By.TagName("tr"));
            var allTr = trFirstTable.Concat(trSecondTable);

            foreach(var tr in allTr)
            {
                var td = tr.FindElements(By.TagName("td"));
                if ((td[3].Text.Contains("Компоненти програмної інженерії 2. Якість та тестування програмного забезпечення")))
                {
                    Console.WriteLine("Є пара в середу");
                    Assert.Pass("Є пара в середу");
                }
            }
            Assert.Fail("Пар немає");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }
    }
}