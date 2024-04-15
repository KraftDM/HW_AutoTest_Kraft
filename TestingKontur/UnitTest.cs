using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestingKontur
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Autorization()
        {
            var driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/Account/Login");

            Assert.Pass();
        }
    }
}