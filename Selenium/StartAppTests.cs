using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace Selenium
{
    [TestClass]
    public class StartAppTests : TestScenario
    {
        [TestInitialize()]
        public override void StartApplication() { }

        [TestCategory("LoginTest"), TestMethod]
        public void AuthorizeTest()
        {
            Driver = (Driver == null) ? TestHelper.Launch() : Driver;

            Authorize("631800107", "91735376");
            //Явное ожидание, пока не загрузится главная страница
            new WebDriverWait(Driver, TimeSpan.FromSeconds(30)).Until<IWebElement>(ExpectedConditions.ElementIsVisible(By.XPath("//*[@id='app']/ui-view/section/nav/div/div/a[2]/figure/img")));
            Assert.AreEqual(Driver.Url, path + "#/main");
        }

        [TestCategory("LoginTest"), TestMethod]
        public void WrongAuthorizeTest()
        {
            Driver = (Driver == null) ? TestHelper.Launch() : Driver;

            Authorize("631800107", "1");
            //Явное ожидание, пока не загрузится кнопка
            new WebDriverWait(Driver, TimeSpan.FromSeconds(30)).Until<IWebElement>(ExpectedConditions.ElementToBeClickable(By.XPath("//*[@id='ngdialog2 - aria - describedby']/footer/button")));
            var button = Driver.FindElement(By.XPath("//*[@id='ngdialog2 - aria - describedby']/footer/button"));
            Assert.IsNotNull(button);
        }

        // Метод для авторизации
        public static void Authorize(string login, string password)
        {
            //Вводим логин
            Driver.FindElement(By.XPath("//*[@id='app']/ui-view/section/section/div/section[2]/section/section/form/div/div[1]/div/label/input")).SendKeys(login);
            //Вводим пароль
            Driver.FindElement(By.XPath("//*[@id='app']/ui-view/section/section/div/section[2]/section/section/form/div/div[2]/div/label/input")).SendKeys(password);
            //нажимаем подтвердить
            Thread.Sleep(300);
            Driver.FindElement(By.XPath("//*[@id='app']/ui-view/section/section/div/section[2]/section/section/form/button")).Click();
        }
    }
}

