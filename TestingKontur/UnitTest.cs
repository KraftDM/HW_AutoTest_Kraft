using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System.Threading;
using System;
using OpenQA.Selenium.DevTools;
//using OpenQA.Selenium.DevTools.V85.Network;

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
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/Account/Login");

            //Thread.Sleep(2000);
            //var login = new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(ExpectedConditions.ElementExists(By.Id ("Username")));
            var login = driver.FindElement(By.Id("Username"));
            login.SendKeys("lsv.kraft@mail.ru");

            var password = driver.FindElement(By.Id("Password"));
            password.SendKeys("!SliM788dD!");

            var enter = driver.FindElement(By.Name("button"));
            enter.Click();

            //Thread.Sleep(500);
            driver.FindElement(By.CssSelector("[data-tid='PageHeader']"));
            var currentUrl = driver.Url;
            Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news", "currentUrl = " + currentUrl + "а должен быть https://staff-testing.testkontur.ru/news");

            //Assert.Pass();
        }

        [Test]
        public void GoToCommunities()
        {
            ChromeOptions options = new ChromeOptions();
            //options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/news");
            driver.Manage().Cookies.AddCookie(new Cookie("idsrv", "CfDJ8MZsrKfWgjxIiLkC4Dl5G3uZ1fOqHz5sqpcgsgUQpo1vKHC3di96sbN2GOKx-oRcCkHIsZTUzhlgmf4zec6Kak9QucCpgz66q3rjdjdkLBBXti7idbDb-nKBBUUlWwPj2qzvuaVSlgweTPvdT1cBzpFCgE1L_B7jyVe_tFC_0J8vLvQB48vZc2KJ68Bum1LV0ckwytyp0OZr7z75pixIDn1XySRL0qVr1RigaG_MYwWzq1Cz2X7K9akbeQx8aor9rqgJzZm1rG7-FwdihBfi-hNfGo6WTrK0tbXdOFAm9gjF9JYazYFyK8qp8RAHaGH-t33lpPzApP-5Eyo-sVmgjsFMFlvL8dqI_DgJyWPEVDwJSe3sG1E1I-QQfDkY8NbyCmsSn2Sbz5ckUa-KNS2dVTPtI7vQ6wp-iZnqLt9CogQTiX94qQf_iFFEf1_wFyE1dVhUPBxZD6YF293nDM93mEJJ1x73T4trIp_JEEYAVDgKWSUU35ckicmvVDPnIK1SHUkmqY6F2TPINbECb7T9AlF0NuZlKoMYOZxd4q6_V-ld23q0zqXSerc4_-L_YCSr9AiVPVuLXCNP0TBKvrvTloQ7kMSi4_MHosecZJWgVFjsMyuftx77AXvkSxRaKNHq5TNfKDbYADo17j60QUcC30Q"));

            try {
                var burger = driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']"));
                burger.Click();
                var sidePage = driver.FindElement(By.CssSelector("[data-tid='SidePage__container']"));  // по скольку в верстке присутствует несколько data-tid='Community'
                var communities = sidePage.FindElement(By.CssSelector("[data-tid='Community']"));       // 1 - в списоке при полном экране, 2 - в бургер меню и оба существуют одновременно
                communities.Click();                                                                    // то сначала берем сайд элемент, а потом берем внутри ссылку
            } catch {
                var communities = driver.FindElement(By.CssSelector("[data-tid='Community']"));
                communities.Click();
            }

            driver.FindElement(By.CssSelector("[data-tid='PageHeader']"));
            var currentUrl = driver.Url;
            Assert.That(currentUrl == "https://staff-testing.testkontur.ru/communities", "currentUrl = " + currentUrl + " а должен быть https://staff-testing.testkontur.ru/communities");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}