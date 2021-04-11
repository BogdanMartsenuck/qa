using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;

namespace Test2
{
    public class Tests
    {
        IWebDriver driver;
        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("https://prom.ua/");
        }

        [Test]
        public void SiteOpeninngTest()
        {
            var actualeTitle = driver.Title;
            var expectedTitle = "Prom.ua Ч маркетплейс ”краины";
            Assert.AreEqual(actualeTitle, expectedTitle);
        }

        [Test]
        public void SearchFunction()
        {
            IWebElement searchField = driver.FindElement(By.XPath("//div[@class='ek-grid__item ek-grid__item_width_expand']//input"));
            IWebElement seachBtn = driver.FindElement(By.XPath("//div[@class='ek-grid__item ek-grid__item_width_expand']//button"));

            searchField.SendKeys("конструкторы");
            seachBtn.Click();

            IWebElement strToCheck = driver.FindElement(By.XPath("//h1[@data-qaid='caption']"));
            string actualResult = strToCheck.Text;
            Assert.AreEqual(actualResult, "–езультаты поиска по запросу Ђконструкторыї в ”краине", "какой то текст");
        }
        [Test]
        public void CheckTheFunctionMoveByCategory()
        {
            Actions action = new Actions(driver);
            IWebElement categories = driver.FindElement(By.XPath("//div[@data-qaid='menu_preview']//span[text()=' расота и здоровье']"));
            action.MoveToElement(categories).Build().Perform();
            driver.FindElement(By.XPath("//a[text()=' осметика по уходу']")).Click();

            String actuaResult = driver.FindElement(By.XPath("//div[@class='ek-body__section']//h1")).Text;
            Console.WriteLine(actuaResult);
        }

        
        [Test]
        public void ReturnToMainPageFunction()
        {
            SearchFunction();
            IWebElement backBtn = driver.FindElement(By.XPath("//a[@data-qaid='to_portal_btn']"));
            backBtn.Click();
            String actualeResult = driver.Url;
            Assert.AreEqual(actualeResult, "https://prom.ua/");
        }

        


        [TearDown]
        public void Close()
        {
            driver.Close();
        }
    }
}