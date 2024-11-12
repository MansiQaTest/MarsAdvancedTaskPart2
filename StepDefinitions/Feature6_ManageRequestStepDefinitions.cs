using MarsAdvancedTaskPart2.Pages.Components;
using MarsAdvancedTaskPart2.Pages;
using MarsAdvancedTaskPart2.Utils;
using System;
using TechTalk.SpecFlow;
using MarsAdvancedTaskPart2.TestModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace MarsAdvancedTaskPart2.StepDefinitions
{
    [Binding, Scope(Feature = "Feature6_ManageRequest")]
    public class Feature6_ManageRequestStepDefinitions : CommonDriver
    {
        LoginPage loginPageObj;
        Homepage homepageObj;
        ManageListings manageListingsObj;
        SearchSkills searchSkillsObj;
        ManageRequests manageRequestsObj;
        List<string> ManageListingDataToCleanUp;

        public Feature6_ManageRequestStepDefinitions()
        {
            loginPageObj = new LoginPage();
            homepageObj = new Homepage();
            manageListingsObj = new ManageListings();
            searchSkillsObj = new SearchSkills();
            manageRequestsObj = new ManageRequests();
            ManageListingDataToCleanUp = new List<string>();

        }
        private void RunAddListingTest(string jsonDataFile)
        {

            try
            {

                List<ManageListingsModel> AddSkillData = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
                foreach (ManageListingsModel AddShareSkill in AddSkillData)
                {
                    try
                    {
                        manageListingsObj.CreateShareSkill(AddShareSkill);
                        ManageListingDataToCleanUp.Add(AddShareSkill.Title);
                    }
                    catch (Exception ex)
                    {
                        //   test.Log(Status.Fail, $"Failed to add education with degree {degree}: {ex.Message}");
                        throw; // Ensure the test fails if the education could not be added
                    }

                }
            }
            catch (Exception e)
            {
                //test.Log(Status.Fail, e.ToString());
                throw;
            }
        }

        [Given(@"the Skill Owner logs into Mars and creates a skill and logs out")]
        public void GivenTheSkillOwnerLogsIntoMarsAndCreatesASkillAndLogsOut()
        {
            loginPageObj.Loginsteps2();

            RunAddListingTest(@"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json");

            IWebElement Signout = driver.FindElement(By.XPath("//button[@class='ui green basic button']"));
            Signout.Click();

        }

        [Given(@"the Requester logs into Mars")]
        public void GivenTheRequesterLogsIntoMars()
        {
            loginPageObj.Loginsteps();

        }

        [When(@"the Requester sends a request to the Skill Owner")]
        public void WhenTheRequesterSendsARequestToTheSkillOwner()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\SearchSkill.json";
            List<SearchSkillModel> searchSkilldata = JsonUtils.ReadJsonData<SearchSkillModel>(jsonDataFile);
            foreach (var item in searchSkilldata)
            {
                string searchString = item.searchString;
                searchSkillsObj.SearchSkill(searchString);
            }

            searchSkillsObj.SendRequest();
        }

        [When(@"the Requester navigates to the Sent Requests page")]
        public void WhenTheRequesterNavigatesToTheSentRequestsPage()
        {
            homepageObj.clickSentrequest();
        }

        [Then(@"the listing should be displayed with the new details")]
        public void ThenTheListingShouldBeDisplayedWithTheNewDetails()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement title = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='QA']")));
            string actualText = title.Text.Trim();
            string expectedText = "QA";
            Assert.That(actualText, Is.EqualTo(expectedText), "The expected message did not appear.");
            TakeScreenshotWithPngFormat();
        }

        [When(@"the Requester sends a request to the Skill Owner and logs out")]
        public void WhenTheRequesterSendsARequestToTheSkillOwnerAndLogsOut()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\SearchSkill.json";
            List<SearchSkillModel> searchSkilldata = JsonUtils.ReadJsonData<SearchSkillModel>(jsonDataFile);
            foreach (var item in searchSkilldata)
            {
                string searchString = item.searchString;
                searchSkillsObj.SearchSkill(searchString);
            }

            searchSkillsObj.SendRequest();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement Signout = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ui green basic button']")));

            Signout.Click();

        }

        [When(@"the Skill Owner logs in and navigates to the Received Requests page")]
        public void WhenTheSkillOwnerLogsInAndNavigatesToTheReceivedRequestsPage()
        {
            loginPageObj.Loginsteps2();
            homepageObj.clickRecievedrequest();
        }

        [When(@"the Requester withdraws the request")]
        public void WhenTheRequesterWithdrawsTheRequest()
        {
            manageRequestsObj.withdrawrequest();
        }

        [Then(@"the request should no longer appear in the Requester's active request list")]
        public void ThenTheRequestShouldNoLongerAppearInTheRequestersActiveRequestList()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait for the element with title 'QA' to be visible
            IWebElement title = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='QA']")));
            string actualTitle = title.Text.Trim();
            string expectedTitle = "QA";

            // Wait for the element with status 'Withdrawn' to be visible
            IWebElement status = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//td[text()='Withdrawn']")));
            string actualStatus = status.Text.Trim();
            string expectedStatus = "Withdrawn";

            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "The expected message did not appear.");
            Assert.That(actualStatus, Is.EqualTo(expectedStatus), "The expected message did not appear.");
            TakeScreenshotWithPngFormat();
        }

        [When(@"the Skill Owner logs into Mars & accepts the request and logs out")]
        public void WhenTheSkillOwnerLogsIntoMarsAcceptsTheRequestAndLogsOut()
        {
            loginPageObj.Loginsteps2();
            manageRequestsObj.acceptrequest();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement Signout = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ui green basic button']")));

            Signout.Click();

        }

        [When(@"the Requester logs into Mars & marks the request as complete")]
        public void WhenTheRequesterLogsIntoMarsMarksTheRequestAsComplete()
        {
            loginPageObj.Loginsteps();
            manageRequestsObj.Completerequest();
        }

        [Then(@"the request status should change to ""([^""]*)"" in the Requester's request list")]
        public void ThenTheRequestStatusShouldChangeToInTheRequestersRequestList(string expectedReview)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            IWebElement title = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='QA']")));
            string actualTitle = title.Text.Trim();
            string expectedTitle = "QA";

            // Wait for the element with status 'Withdrawn' to be visible
            IWebElement Review = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ui positive basic button' and text()='Review']")));
            string actialreview = Review.Text.Trim();
           

            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "The expected message did not appear.");
            Assert.That(actialreview, Is.EqualTo(expectedReview), "The expected message did not appear.");
            TakeScreenshotWithPngFormat();
        }

        [Then(@"Logs out")]
        public void ThenLogsOut()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement Signout = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ui green basic button']")));

            Signout.Click();
            TakeScreenshotWithPngFormat();
        }

        [Then(@"the Skill Owner logs into Mars and complete the status and logs out")]
        public void ThenTheSkillOwnerLogsIntoMarsAndCompleteTheStatusAndLogsOut()
        {
            loginPageObj.Loginsteps2();
            homepageObj.clickRecievedrequest();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement Complete = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ui positive basic button' and text()='Complete']\r\n")));

            Complete.Click();

            IWebElement Signout = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//button[@class='ui green basic button']")));

            Signout.Click();
            TakeScreenshotWithPngFormat();



        }


        [When(@"the Requester provides a review for the completed request")]
        public void WhenTheRequesterProvidesAReviewForTheCompletedRequest()
        {
            loginPageObj.Loginsteps();
            homepageObj.clickSentrequest();
            manageRequestsObj.reviewrequest();
        }

        [Then(@"the review should be saved ""([^""]*)"" message should be displayed")]
        public void ThenTheReviewShouldBeSavedMessageShouldBeDisplayed(string expectedmessage)
        {
            string actualMessage = manageRequestsObj.GetMessage();
            Assert.That(actualMessage, Is.EqualTo(expectedmessage), "The expected error message did not appear.");
            TakeScreenshotWithPngFormat();

        }


        [When(@"the Skill Owner logs into Mars & Skill Owner navigates to the Received Requests page")]
        public void WhenTheSkillOwnerLogsIniMarsSkillOwnerNavigatesToTheReceivedRequestsPage()
        {
            loginPageObj.Loginsteps2();

            homepageObj.clickRecievedrequest();

        }


        [When(@"the Skill Owner accepts the request")]
        public void WhenTheSkillOwnerAcceptsTheRequest()
        {
            manageRequestsObj.acceptrequest();
        }

        [Then(@"the request status should change to ""([^""]*)"" in the Skill Owner's request list")]
        public void ThenTheRequestStatusShouldChangeToInTheSkillOwnersRequestList(string expectedStatus)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait for the element with title 'QA' to be visible
            IWebElement title = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='QA']")));
            string actualTitle = title.Text.Trim();
            string expectedTitle = "QA";

            // Wait for the element with status 'Accepted' to be visible
            IWebElement status = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//td[text()='Accepted']")));
            string actualStatus = status.Text.Trim();
       
            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "The expected message did not appear.");
            Assert.That(actualStatus, Is.EqualTo(expectedStatus), "The expected message did not appear.");
            TakeScreenshotWithPngFormat();
        }

        [When(@"the Skill Owner declines the request")]
        public void WhenTheSkillOwnerDeclinesTheRequest()
        {
            manageRequestsObj.declinerequest();
        }

        [Then(@"the request status should change to ""([^""]*)"" in the Skill Owner request list")]
        public void ThenTheRequestStatusShouldChangeToInTheSkillOwnerRequestList(string expectedStatus)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            // Wait for the element with title 'QA' to be visible
            IWebElement title = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//a[text()='QA']")));
            string actualTitle = title.Text.Trim();
            string expectedTitle = "QA";

            // Wait for the element with status 'Accepted' to be visible
            IWebElement status = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//td[text()='Declined']")));
            string actualStatus = status.Text.Trim();

            Assert.That(actualTitle, Is.EqualTo(expectedTitle), "The expected message did not appear.");
            Assert.That(actualStatus, Is.EqualTo(expectedStatus), "The expected message did not appear.");
            TakeScreenshotWithPngFormat();
        }



    }
}
