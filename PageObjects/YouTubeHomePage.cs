using OpenQA.Selenium;
using System;

namespace YoutubeTestItProject
{
    public class YouTubeHomePage : PageObject
    {

        #region Map of Local Elements

        private IWebElement ElementSearchQueryInput => Driver.FindElement(By.Name("search_query"));
        private IWebElement ElementSearchButton => Driver.FindElement(By.XPath("//*[@id='search-icon-legacy']"));
        private IWebElement ElementChannelSection => Driver.FindElement(By.CssSelector("#contents > ytd-channel-renderer #content-section"));
        private IWebElement ElementChannelInfo => Driver.FindElement(By.XPath("//*[@id='info-section']//*[@id='text']"));

        private readonly string _elementSearchResultsFilterXPath  = "//*[@id='filter-menu']//*[@aria-label='Search filters']";
        
        #endregion //Map of Elements

        public YouTubeHomePage(IWebDriver driver) : base(driver)
        {
            WaitMethods.WaitForAllAjaxCalls();
        }

        internal void FillInSearchField(string query)
        {
            ElementSearchQueryInput.Click();
            ElementSearchQueryInput.SendKeys(query);
            WaitMethods.WaitForAllAjaxCalls();
        }

        internal void InitSearch()
        {
            try
            {
                WaitMethods.WaitElement(ElementSearchButton);
                ElementSearchButton.Click();
                WaitMethods.WaitElement(By.XPath(_elementSearchResultsFilterXPath), 5);
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Search button was not clicked at the first attempt. Retried.");
                WaitMethods.WaitElement(By.XPath("//*[@id='search-icon-legacy']"));
                ElementSearchButton.Click();
                WaitMethods.WaitElement(By.XPath(_elementSearchResultsFilterXPath), 5);
            }
        }

        internal bool IsTestITchannelShownInSearchResults()
        {
            WaitMethods.WaitElement(ElementChannelSection);
            return ElementChannelInfo.Text.Contains("Test IT");
        }
    }
}
