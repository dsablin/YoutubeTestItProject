using NUnit.Framework;
using YoutubeTestItProject.PageObjects;

namespace YoutubeTestItProject
{
    [TestFixture]
    public class YoutubeTestItChannelTests : TestBase
    {
         [Test(Description = "Verify TestIT youtube channel could be found")]
        public void LookForTestItChannelTest()
        {
            YouTubeHomePage home = GoToYoutubeHomePage();

            home.FillInSearchField("Test IT");
            home.InitSearch();

            Assert.True(home.IsTestITchannelShownInSearchResults(), "Test IT channel was not found.");
        }

        [Test(Description = "Verify a particular video from TestIT channel could be opened")]
        public void OpenTestITvideoTest()
        {
            TestITchannelPage testItPage = GoToTestITchannelPage();

            testItPage.SwitchToVideosTab();

            string isRecentlyUploadedTabActive = testItPage.EnsureRecentlyUploadedVideosTabOpened();
            Assert.AreEqual("true", isRecentlyUploadedTabActive, "Recently Uploaded videos tab was not active unexpectedly.");

            testItPage.OpenVideoChosen();

            testItPage.StopPlayingVideoIfStarted();
        }
    }
}
