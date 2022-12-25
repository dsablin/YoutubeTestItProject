using OpenQA.Selenium;
using System.Collections.Generic;

namespace YoutubeTestItProject.PageObjects
{
    public class TestITchannelPage : PageObject
    {
        #region Map of Local Elements

        private IWebElement ElementVideosTab => Driver.FindElement(By.CssSelector(".style-scope:nth-child(4) > .tab-content"));
        private IList<IWebElement> ElementVideoModeToggles => Driver.FindElements(By.XPath("//*[@id='chips']//*[@id='text']/../../yt-chip-cloud-chip-renderer[1]"));
        private IWebElement ElementRecentlyUploadedFilter => Driver.FindElement(By.XPath("//*[@id='chips']//*[@id='text']/../../yt-chip-cloud-chip-renderer[1]"));
        private IWebElement ElementVideoPreview => Driver.FindElement(By.XPath("//*[@id='contents']/ytd-rich-grid-row[2]//ytd-rich-item-renderer[3]//a[@id='thumbnail']"));
        private IWebElement ElementStartStopButton => Driver.FindElement(By.XPath("//*[@class='ytp-play-button ytp-button']"));
        private IWebElement ElementPlayButton => Driver.FindElement(By.XPath("//*[@class='ytp-play-button ytp-button'][@data-title-no-tooltip='Play']"));

        #endregion //Map of Elements

        public TestITchannelPage(IWebDriver driver) : base(driver)
        {
        }

        internal void SwitchToVideosTab()
        {
            WaitMethods.WaitElement(ElementVideosTab);
            ElementVideosTab.Click();
        }

        internal string EnsureRecentlyUploadedVideosTabOpened()
        {
            WaitMethods.WaitListElementSafe(ElementVideoModeToggles, 3);
            WaitMethods.WaitElement(ElementRecentlyUploadedFilter);
            string attr = ElementRecentlyUploadedFilter.GetAttribute("aria-selected");
            return attr;
        }

        internal void OpenVideoChosen()
        {
            WaitMethods.WaitElement(ElementVideoPreview);
            ElementVideoPreview.Click();
        }

        internal void StopPlayingVideoIfStarted()
        {
            WaitMethods.WaitElement(ElementStartStopButton);
            if (ElementStartStopButton.GetAttribute("data-title-no-tooltip").Equals("Pause"))
            {
                ElementStartStopButton.Click();
                WaitMethods.WaitElement(ElementPlayButton);
            }

        }
    }
}
