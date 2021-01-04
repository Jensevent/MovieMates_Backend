using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

namespace MovieMates_Backend_E2ETests
{
    [Binding]
    public class TestHooks
    {

        [Before]
        public void CreateWebDriver(ScenarioContext context)
        {
            var options = new ChromeOptions();

            options.AddArgument("--start-maximized");
            options.AddArgument("--disable-notifications");

            IWebDriver webDriver = new ChromeDriver(options);
            context["WEB_DRIVER"] = webDriver;
        }


        [After]
        public void CloseWebDriver(ScenarioContext context)
        {
            var driver = context["WEB_DRIVER"] as IWebDriver;
            driver.Quit();
        }
    }
}
