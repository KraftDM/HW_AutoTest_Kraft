using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace TestingKontur
{
    public class Tests
    {
        ChromeDriver driver;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Autorization()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/Account/Login");

            Thread.Sleep(2000);


            var login = driver.FindElement(By.Id("Username"));
            login.SendKeys("lsv.kraft@mail.ru");

            var password = driver.FindElement(By.Id("Password"));
            password.SendKeys("!SliM788dD!");

            var enter = driver.FindElement(By.Name("button"));
            enter.Click();


            Thread.Sleep(1000);

            var currentUrl = driver.Url;
            Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news", "currentUrl = " + currentUrl + "а должен быть https://staff-testing.testkontur.ru/news");


            //Assert.Pass();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}