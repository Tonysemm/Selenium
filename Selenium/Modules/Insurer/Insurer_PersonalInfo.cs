using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium.Modules.Insurer
{
    [TestClass]
    public class Insurer_PersonalInfo : TestScenario
    {
        [TestInitialize()]
        public void ClassInit()
        {
            Driver.FindElement(By.XPath("//*[@id='app']/ui-view/section/nav/div/div/a[2]/figure/img")).Click();
            Driver.FindElement(By.XPath("//*[@id='app']/ui-view/section/section/main/div/div[2]/ul/li[1]")).Click();
            Driver.FindElement(By.XPath("//*[@id='app']/ui-view/section/header/button[1]")).Click();
        }

        [TestCategory("Insurer_PersonalInfo"), TestMethod]
        public void Insurer_PersonalInfoTestMethod()
        {
            var info = DataSet.ReadExcel<PersonalData>();


        }
    }
}
//Driver.FindElement(By.XPath("")).SendKeys("");
//Driver.FindElement(By.XPath("")).Click();