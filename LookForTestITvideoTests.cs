using NUnit.Framework;
using OpenQA.Selenium;

namespace YoutubeTestItProject
{
    [TestFixture]
    public class LookForTestITvideoTests : TestBase
    {
        [Test]
        public void LookForTestITvideoTest()
        {
            GoToTestITchannelPage();

            WaitElement(By.CssSelector(".style-scope:nth-child(4) > .tab-content"));
            IWebElement videosTab = driver.FindElement(By.CssSelector(".style-scope:nth-child(4) > .tab-content"));
            videosTab.Click();

            WaitElement(By.XPath("//*[@id='chips']//*[@id='text']/../../yt-chip-cloud-chip-renderer[1]"));
            IWebElement recentlyUploadedFilter = driver.FindElement(By.XPath("//*[@id='chips']//*[@id='text']/../../yt-chip-cloud-chip-renderer[1]"));
            string attr = recentlyUploadedFilter.GetAttribute("aria-selected");
            Assert.AreEqual("true", attr);

            WaitElement(By.XPath("//*[@id='contents']/ytd-rich-grid-row[2]//ytd-rich-item-renderer[3]//a[@id='thumbnail']"));
            IWebElement videoPreview = driver.FindElement(By.XPath("//*[@id='contents']/ytd-rich-grid-row[2]//ytd-rich-item-renderer[3]//a[@id='thumbnail']"));
            videoPreview.Click();

            WaitElementToBeClickable(By.CssSelector(".ytp-play-button"));
            driver.FindElement(By.CssSelector(".ytp-play-button")).Click();
        }
    }
}
