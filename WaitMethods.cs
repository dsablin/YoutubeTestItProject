using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace YoutubeTestItProject
{
    public class WaitMethods
    {
        private readonly IWebDriver driver;

        public WaitMethods(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void WaitElement(IWebElement element, int timeout = PageObject.WaitTimeout)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            wait.Until(
                driver =>
                {
                    try
                    {
                        return element.Displayed;
                    }
                    catch (NoSuchElementException)
                    {
                    }
                    return false;
                });
        }

        public IWebElement WaitElement(By by, int timeout = PageObject.WaitTimeout, bool isIgnored = false)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(@by));
            }
            catch (WebDriverTimeoutException)
            {
                if (!isIgnored)
                {
                    Console.WriteLine("Timed out after {0} seconds while waiting for element to be visible by {1}", timeout, by.ToString());
                    throw;
                }
            }
            return null;
        }

        public void WaitListElementSafe(IList<IWebElement> list, int timeout = PageObject.WaitTimeout)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, timeout));
            try
            {
                wait.Until(
                    driver => {
                        try
                        {
                            return list.Count > 0;
                        }
                        catch (NoSuchElementException)
                        {
                        }
                        return false;
                    });
            }
            catch (WebDriverTimeoutException)
            {
            }
        }

        public void WaitForAllAjaxCalls(int totalWaitTimeout = PageObject.WaitTimeout)
        {
            var jsExecutor = driver as IJavaScriptExecutor;

            bool isJQueryCounterExists = false;
            bool isFetchCounterExists = false;

            var sw = new Stopwatch();
            sw.Start();

            while (sw.Elapsed.TotalSeconds <= totalWaitTimeout)
            {
                try
                {
                    isJQueryCounterExists = (bool)jsExecutor.ExecuteScript("return window.jQuery != undefined");
                    isFetchCounterExists = (bool)jsExecutor.ExecuteScript("return Boolean(\"fetchActive\" in window)"); // Counter for applications which are using Fetch API.
                    break;
                }
                catch (NoSuchWindowException) { } //Page Reload
            }

            if (isJQueryCounterExists || isFetchCounterExists)
            {
                while (sw.Elapsed.TotalSeconds <= totalWaitTimeout)
                {
                    bool areJQueryAjaxRequestsCompleted = true;
                    try
                    {
                        if ((bool)jsExecutor.ExecuteScript("return window.jQuery != undefined"))
                            areJQueryAjaxRequestsCompleted = (bool)jsExecutor.ExecuteScript("return jQuery.active === 0");
                        var areFetchAjaxRequestsCompleted = !isFetchCounterExists || (bool)jsExecutor.ExecuteScript("return window.fetchActive === 0");

                        if (areJQueryAjaxRequestsCompleted && areFetchAjaxRequestsCompleted)
                            break;
                    }
                    catch (NoSuchWindowException) { } //Page Reload
                }
            }

            sw.Stop();

            if (sw.Elapsed.TotalSeconds > totalWaitTimeout)
                throw new Exception("Timed out waiting for Ajax calls to finish.");
        }
    }
}
