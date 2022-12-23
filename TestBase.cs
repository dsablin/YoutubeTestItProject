using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace YoutubeTestItProject
{
    public class TestBase
    {
        protected IWebDriver driver;
        protected string baseURL;
        protected const int WaitTimeout = 30;

        [SetUp]
        public void SetUp()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.youtube.com/";
        }

        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }

        protected void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        protected void GoToTestITchannelPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/@TestITTMS");
        }

        protected void FillInSearchField(string query)
        {
            driver.FindElement(By.Name("search_query")).Click();
            driver.FindElement(By.Name("search_query")).SendKeys(query);
        }

        protected void InitSearch()
        {
            WaitElementToBeClickable(By.XPath("//*[@id='search-icon-legacy']"));
            IWebElement searchButton = driver.FindElement(By.XPath("//*[@id='search-icon-legacy']"));
            searchButton.Click();
        }

            protected void WaitForTestITchannelToAppear()
        {
            WaitListElement(By.XPath("//*[@id='info-section']//*[@id='text'][contains(., 'Test IT')]"));
            var channelElements = driver.FindElements(By.XPath("//*[@id='info-section']//*[@id='text'][contains(., 'Test IT')]"));
            Assert.True(channelElements.Count > 0);
        }

        protected IList<IWebElement> WaitListElement(By by)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitTimeout));
            wait.Until(driver =>
            {
                try
                {
                    var list = this.driver.FindElements(by);
                    return list.Count > 0;
                }
                catch (NoSuchElementException)
                {
                }

                return false;
            });
            return driver.FindElements(by);
        }

        protected void WaitElement(By by)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitTimeout));
            wait.Until(
                driver =>
                {
                    try
                    {
                        driver.FindElement(by);
                        return true;
                    }
                    catch (NoSuchElementException)
                    {
                    }
                    return false;
                });
        }

        protected void WaitElementAndClickWhenReady(IWebElement element)
        {
            WaitElementToBeClickable(element);
            try
            {
                element.Click();
            }
            catch (TargetInvocationException)
            {
                throw;
            }
        }

        protected void WaitElementToBeClickable(IWebElement element)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitTimeout));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(element));
            }
            catch (WebDriverTimeoutException)
            {
                throw;
            }
        }

        protected void WaitElementToBeClickable(By by)
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, WaitTimeout));
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(@by));
            }
            catch (WebDriverTimeoutException)
            {
                throw;
            }
        }
    }
}
