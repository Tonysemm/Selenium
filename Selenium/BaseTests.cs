using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

// Requires reference to WebDriver.Support.dll
//using OpenQA.Selenium.Support.UI;


namespace Selenium
{
    [TestClass]
    public class BaseTests : TestScenario
    {
        StartAppTests loginTest;

        public BaseTests()
        {
            loginTest = new StartAppTests();
        }

        [TestInitialize()]
        public override void StartApplication()
        {
            Driver = TestHelper.Launch();
            loginTest.AuthorizeTest();
        }
    }
}
