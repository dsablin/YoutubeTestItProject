using NUnit.Framework;

namespace YoutubeTestItProject
{
    [TestFixture]
    public class TestItTests : TestBase
    {
         [Test]
        public void FindTestItChannelTest()
        {
            GoToHomePage();

            FillInSearchField("Test IT");
            InitSearch();
            WaitForTestITchannelToAppear();
        }
    }
}
