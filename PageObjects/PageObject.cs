using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace YoutubeTestItProject
{
    public abstract class PageObject
    {
        protected IWebDriver Driver;
        private WaitMethods _waitMethods;
        public const int DefaultPollingInterval = 500;
        public const int WaitTimeout = 30;

        protected PageObject(IWebDriver driver)
        {
            Driver = driver;
        }

        public WaitMethods WaitMethods => _waitMethods ?? (_waitMethods = new WaitMethods(Driver));
    }
}