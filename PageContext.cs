using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnit_practice
{
    public class PageContext
    {
        public PageContext()
        {
            Driver = new ChromeDriver();
            Actions = new Actions(Driver);
        }

        public IWebDriver Driver { get; private set; }
        public Actions Actions { get; private set; }
    }
}