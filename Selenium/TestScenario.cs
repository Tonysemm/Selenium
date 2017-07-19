using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Selenium.DataSet;

namespace Selenium
{
    //Глобальный тестовый базовый класс
    [TestClass]
    public abstract class TestScenario
    {
        public static string path = "http://ictas.github.io/TAD/";
        public static ChromeDriver Driver { get; protected set; }
        public static BaseTests BaseTest { get; protected set; }
        //public DataSet dataSet { get; set; }
        

        [TestInitialize()]
        public virtual void StartApplication()
        {
            //dataSet = new DataSet();
            //dataSet.ReadExcel<PersonalData>();
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
