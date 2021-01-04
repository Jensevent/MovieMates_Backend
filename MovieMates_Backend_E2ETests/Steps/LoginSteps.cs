using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;
using Xunit;

namespace MovieMates_Backend_E2ETests.Steps
{
    [Binding]
    public class LoginSteps
    {
        private readonly IWebDriver webDriver;

        public LoginSteps(ScenarioContext scenarioContext)
        {
            this.webDriver = scenarioContext["WEB_DRIVER"] as IWebDriver;
        }

        private void sleep(int seconds)
        {
            DateTime now = DateTime.Now;

            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(seconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(100)
            };
            wait.Until(wd => (DateTime.Now - now) - TimeSpan.FromSeconds(seconds) > TimeSpan.Zero);
        }


        [Given(@"I have navigated to ""(.*)""")]
        public void GivenIHaveNavigatedTo(string page)
        {
            webDriver.Url = $"{page}";
        }

        //[Then(@"the page contains ""(.*)""")]
        //public void ThenThePageContains(string text)
        //{
        //    Assert.Contains(text, webDriver.PageSource);
        //}

        //[Then(@"the element ""(.*)"" contains ""(.*)""")]
        //public void AndTheElementContains(string elementID, string text)
        //{
        //    string textElement = webDriver.FindElement(By.Id(elementID)).Text;
        //    Assert.Equal(text, textElement);
        //}

        [Then(@"I Login with Username ""(.*)"" and Password ""(.*)""")]
        public void WhenILoginWithUsernameAndPasswordOnTheLoginPage(string username, string password)
        {
            var usernameBox = webDriver.FindElement(By.Id("username"));
            var passwordBox = webDriver.FindElement(By.Id("password"));
            var submitBtn = webDriver.FindElement(By.Id("login"));

            usernameBox.SendKeys(username);
            passwordBox.SendKeys(password);
            submitBtn.Click();
            sleep(3);
        }

        [Then(@"I'm redirected to ""(.*)""")]
        public void ThenImOnTheFollowingPage(string page)
        {
            var url = webDriver.Url;
            Assert.Equal(page, url);
        }

    }
}
