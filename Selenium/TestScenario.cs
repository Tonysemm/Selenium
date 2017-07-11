using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    //Глобальный тестовый базовый класс
    [TestClass]
    public abstract class TestScenario
    {
        public static ChromeDriver Driver { get; protected set; }
        public static BaseTests BaseTest { get; protected set; }
        public static string path = "http://ictas.github.io/TAD/";

        [TestInitialize()]
        public virtual void StartApplication()
        {
            BaseTest = new BaseTests();
            BaseTest.StartApplication();
        }

        [TestCleanup()]
        public virtual void EndApplication()
        {
            if (Driver != null)
                Driver.Quit();
        }
    }
}
