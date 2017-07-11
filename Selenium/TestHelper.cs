using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    public static class TestHelper
    {
        static ChromeDriver Driver { get; set; }

        public static ChromeDriver Launch()
        {
            Driver = new ChromeDriver("..\\..\\..\\files");
            Driver.Navigate().GoToUrl(TestScenario.path);
            //присваеваем неявное ожидание = 10 секунд 
            Driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            return Driver;
        }
    }
}
