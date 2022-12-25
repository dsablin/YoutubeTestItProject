using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using YoutubeTestItProject.PageObjects;

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
            //driver = new ChromeDriver();
            driver = new FirefoxDriver();
            baseURL = "https://www.youtube.com/";
        }

        [TearDown]
        protected void TearDown()
        {
            driver.Quit();
        }

        protected YouTubeHomePage GoToYoutubeHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);

            return new YouTubeHomePage(driver);
        }

        protected TestITchannelPage GoToTestITchannelPage()
        {
            driver.Navigate().GoToUrl(baseURL + "/@TestITTMS");
            return new TestITchannelPage(driver);
        }
    }
}
