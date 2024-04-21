using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
//using SeleniumExtras.WaitHelpers;
using System.Threading;
using System;
using System.IO;
using EC = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace TestingKontur
{
    public class Tests
    {
        ChromeDriver driver;
        Cookie cookie = new Cookie("idsrv",
            "CfDJ8MZsrKfWgjxIiLkC4Dl5G3uZ1fOqHz5sqpcgsgUQpo1vKHC3di96sbN2GOKx-oRcCkHIsZTUzhlgmf4zec6Kak9QucCpgz66q3rjdjdkLBBXti7idbDb-nKBBUUlWwPj2qzvuaVSlgweTPvdT1cBzpFCgE1L_B7jyVe_tFC_0J8vLvQB48vZc2KJ68Bum1LV0ckwytyp0OZr7z75pixIDn1XySRL0qVr1RigaG_MYwWzq1Cz2X7K9akbeQx8aor9rqgJzZm1rG7-FwdihBfi-hNfGo6WTrK0tbXdOFAm9gjF9JYazYFyK8qp8RAHaGH-t33lpPzApP-5Eyo-sVmgjsFMFlvL8dqI_DgJyWPEVDwJSe3sG1E1I-QQfDkY8NbyCmsSn2Sbz5ckUa-KNS2dVTPtI7vQ6wp-iZnqLt9CogQTiX94qQf_iFFEf1_wFyE1dVhUPBxZD6YF293nDM93mEJJ1x73T4trIp_JEEYAVDgKWSUU35ckicmvVDPnIK1SHUkmqY6F2TPINbECb7T9AlF0NuZlKoMYOZxd4q6_V-ld23q0zqXSerc4_-L_YCSr9AiVPVuLXCNP0TBKvrvTloQ7kMSi4_MHosecZJWgVFjsMyuftx77AXvkSxRaKNHq5TNfKDbYADo17j60QUcC30Q");

        [SetUp]
        public void Setup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/news");
            driver.Manage().Cookies.AddCookie(cookie);
            driver.FindElement(By.CssSelector("[data-tid='PageHeader']"));
        }

        [Test]
        public void Autorization()
        {
            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/Account/Login");

            var login = driver.FindElement(By.Id("Username"));
            login.SendKeys("lsv.kraft@mail.ru");

            var password = driver.FindElement(By.Id("Password"));
            password.SendKeys("!SliM788dD!");

            var enter = driver.FindElement(By.Name("button"));
            enter.Click();

            driver.FindElement(By.CssSelector("[data-tid='PageHeader']"));
            var currentUrl = driver.Url;
            Assert.That(currentUrl == "https://staff-testing.testkontur.ru/news", "currentUrl = " + currentUrl + "а должен быть https://staff-testing.testkontur.ru/news");
        }

        [Test]
        public void NavigateToCommunities()
        {

            var communities = driver.FindElement(By.CssSelector("[data-tid='Community']"));
            communities.Click();

            /* тут раньше был код для маленького окна, но мне не хочется его выбрасывать
             
            var burger = driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']"));
            burger.Click();
            var sidePage = driver.FindElement(By.CssSelector("[data-tid='SidePage__container']"));  // по скольку в верстке присутствует несколько data-tid='Community'
            var communities = sidePage.FindElement(By.CssSelector("[data-tid='Community']"));       // 1 - в списоке при полном экране, 2 - в бургер меню и оба существуют одновременно
            communities.Click();                                                                    // то сначала берем сайд элемент, а потом берем внутри элемент
            */ 

            driver.FindElement(By.CssSelector("[data-tid='PageHeader']"));
            var currentUrl = driver.Url;
            Assert.That(currentUrl == "https://staff-testing.testkontur.ru/communities", "currentUrl = " + currentUrl + " а должен быть https://staff-testing.testkontur.ru/communities");
        }

        [Test]
        public void EmptyStateDir()
        {
            // сетап для этого теста: очищаем папку и копируем один элемент
            DriverExecuteScriptFromFile("./JSResponseToAPI/EmptyStateDir/ClearFolder.js");
            Thread.Sleep(50);       // необходимо зло, т.к. скрипты очистки папки и копирования работают ассинхронно
            DriverExecuteScriptFromFile("./JSResponseToAPI/EmptyStateDir/CopyFile.js");

            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/files/e0e8b17f-81ae-4392-8b1f-61d6d2cb8319");
            driver.FindElement(By.CssSelector("[data-tid='Title']"));

            var filesTable = driver.FindElement(By.CssSelector("[data-tid='FilesTable']")); // на странице несоклько data-tid='DropdownButton' уточняем через родителя
            filesTable.FindElement(By.CssSelector("[data-tid='DropdownButton']")).Click();
            driver.FindElement(By.CssSelector("[data-tid='DeleteFile']")).Click();
            driver.FindElement(By.CssSelector("[data-tid='DeleteButton']")).Click();
            
            try {
                new WebDriverWait(driver, TimeSpan.FromSeconds(1)).Until(EC.ElementExists(By.CssSelector("[filter='url(#filter1_d)']")));
            } catch {
                Assert.Fail("На странице нет заглушки, где котик? О_О");
            }
        }

        [Test]
        public void ChangeCommunityDesciption()
        {
            // сетапим описание сообщества на "SETUP"
            DriverExecuteScriptFromFile("./JSResponseToAPI/ChangeCommunityDesciption/ChangeDescription.js");

            string text = "ILOVECATS";

            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities/0d6983d3-d4ad-4f9f-9cf4-a2f729857ddd/settings");
            var descriptionDiv = driver.FindElement(By.CssSelector("[data-tid='Description']"));
            var description = descriptionDiv.FindElement(By.TagName("textarea"));
            description.SendKeys(Keys.Control + 'a'); // или (Keys.Shift + Keys.ArrowUp) .Clear() с какой-то версии не работает
            description.SendKeys(Keys.Delete);
            description.SendKeys(text);

            var pageHeader = driver.FindElement(By.CssSelector("[data-tid='PageHeader']"));
            var buttons = pageHeader.FindElements(By.TagName("button"));
            buttons[1].Click(); // 0 - отменить,  1 - сохранить

            description = driver.FindElement(By.CssSelector("[data-tid='Description']")).FindElement(By.TagName("div"));
            Assert.That(description.Text == text, "currentText = " + description.Text + " а должен быть " + text);
        }

        [Test]
        public void AddDiscussion()
        {
            driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/communities/0d6983d3-d4ad-4f9f-9cf4-a2f729857ddd?tab=discussions");
            var discussions = driver.FindElements(By.CssSelector("[data-tid='DiscussionItem']"));
            var disOldCount = discussions.Count;

            driver.FindElement(By.CssSelector("[data-tid='AddDiscussionButton']")).Click();

            var modal = driver.FindElement(By.CssSelector("[data-tid='modal-content']"));
            var name = modal.FindElement(By.TagName("input"));
            name.SendKeys(disOldCount + " CAT");

            var disc = modal.FindElement(By.CssSelector("[role='textbox']"));
            disc.SendKeys("ILOVECATS");
            modal.FindElement(By.CssSelector("[data-tid='CreateButton']")).Click();
            //modal.FindElement(By.CssSelector("[data-tid='CancelButton']")).Click(); если интересно увидеть ошибку теста

            try 
            {
                // тут такое явное ожидание, потому что новое обсуждение подгуражется и в DOM появляется не сразу
                new WebDriverWait(driver, TimeSpan.FromSeconds(1)).Until(d => d.FindElements(By.CssSelector("[data-tid='DiscussionItem']")).Count == disOldCount + 1);
            } catch {
                Assert.Fail("Новое обсуждение не появилось на экране или не создалось");
            }
        }

        [TearDown]
        public void TearDown()
        {
            driver.Close();
        }

        private void DriverExecuteScriptFromFile(string path)
        {
            string JScode = File.ReadAllText(path);
            driver.ExecuteScript(JScode);
        }

        //setTimeout(function(){debugger;}, 5000)
    }
}
