using AventStack.ExtentReports;
using MarsAdvancedTaskPart2.Pages;
using MarsAdvancedTaskPart2.Pages.Components;
using MarsAdvancedTaskPart2.TestModel;
using MarsAdvancedTaskPart2.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RazorEngine;
using System;
using TechTalk.SpecFlow;

namespace MarsAdvancedTaskPart2.StepDefinitions
{
    [Binding, Scope(Feature = "Feature5_ManageListing")]
    public class Feature5_ManageListingsStepDefinitions : CommonDriver
    {
        LoginPage loginPageObj;
        Homepage homepageObj;
        ManageListings manageListingsObj;
        SearchSkills searchSkillsObj;
        List<string> ManageListingDataToCleanUp;

        public Feature5_ManageListingsStepDefinitions() 
        {
            loginPageObj = new LoginPage();
            homepageObj = new Homepage();
            manageListingsObj = new ManageListings();
            searchSkillsObj = new SearchSkills();
            ManageListingDataToCleanUp = new List<string>();

        }

        [Given(@"User Logs into Mars")]
        public void GivenUserLogsIntoMars()
        {
            loginPageObj.Loginsteps();
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
        private void RunUpdateListingTest(string jsonDataFile)
        {

            try
            {

                List<ManageListingsModel> EditSkillData = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
                foreach (ManageListingsModel EditManageListing in EditSkillData)
                {
                    try
                    {
                        manageListingsObj.UpdateManageListing(EditManageListing);
                        ManageListingDataToCleanUp.Add(EditManageListing.Title);

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
        private void RunDeleteListingTest(string jsonDataFile)
        {

            try
            {
                List<ManageListingsModel> DeleteShareSkill = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
                foreach (ManageListingsModel DeleteManageListing in DeleteShareSkill)
                {

                    try
                    {
                        manageListingsObj.DeleteManageListing(DeleteManageListing);

                    }
                    catch (Exception ex)
                    {
                        test.Log(Status.Fail, $"Failed to delete skill");
                        throw; // Ensure the test fails if the skill could not be delete
                    }
                }
            }
            catch (Exception e)
            {
                test.Log(Status.Fail, e.ToString());
                throw;
            }
        }
        [Given(@"User navigates to ManageListing page")]
        public void GivenUserNavigatesToManageListingPage()
        {
            homepageObj.clickmanagelisting();
        }

        [When(@"the user add new listing data from json file")]
        public void WhenTheUserAddNewListingDataFromJsonFile()
        {
            RunAddListingTest(@"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json");

        }

        [When(@"the user modifies the listing details with valid data from json file")]
        public void WhenTheUserModifiesTheListingDetailsWithValidDataFromJsonFile()
        {
            RunUpdateListingTest(@"D:\MarsAdvancedTaskPart2\TestData\EditManageListing.json");
        }

        [Then(@"the listing should be updated with the new details")]
        public void ThenTheListingShouldBeUpdatedWithTheNewDetails()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json";
            List<ManageListingsModel> AddSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
            ManageListingsModel addShareSkill = AddSkilldata.FirstOrDefault();

            string jsonDataFile2 = @"D:\MarsAdvancedTaskPart2\TestData\EditManageListing.json";
            List<ManageListingsModel> EditSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile2);
            ManageListingsModel EditShareSkill = EditSkilldata.LastOrDefault();

            try
            {
                string editedTitle = manageListingsObj.GetList();

                // Log the expected and actual skill title values for debugging purposes
                Console.WriteLine($"Expected Skill Title: {EditShareSkill.Title}");
                Console.WriteLine($"Actual Skill Title: {editedTitle}");

                // Assert that the actual skill title matches the expected title
                Assert.That(editedTitle, Is.EqualTo(EditShareSkill.Title), "Actual skill title and Expected skill title do not match");

                // Assert that the added skill is not null or empty (to ensure that it was successfully added)
                Assert.That(!string.IsNullOrEmpty(editedTitle), $"Expected skill title was not added. Actual skill title: '{editedTitle}'");

                // If all assertions succeed, log the test as passed
                test.Log(Status.Pass, "Listing updated successfully with the new skill details.");
            }
            catch (WebDriverTimeoutException ex)
            {
                test.Log(Status.Fail, $"Timed out waiting for the message: {ex.Message}");
                throw;
            }

            catch (Exception ex)
            {
                // Log any other unexpected exception
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                TakeScreenshotWithPngFormat();
                throw;
            }
            finally
            {

                try
                {
                    if (!string.IsNullOrEmpty(EditShareSkill.Title))
                    {

                        CommonDriver.ManageListingToCleanUp.Add(EditShareSkill.Title);


                    }
                    if (!string.IsNullOrEmpty(addShareSkill.Title))
                    {
                        CommonDriver.ManageListingToCleanUp.Add(addShareSkill.Title);

                    }
                }
                catch (Exception cleanupEx)
                {
                    test.Log(Status.Fail, $"Failed to add title during cleanup: {cleanupEx.Message}");
                }
            }

        }

        [When(@"the user modifies the listing details with invalid data from json file")]
        public void WhenTheUserModifiesTheListingDetailsWithInvalidDataFromJsonFile()
        {
            RunUpdateListingTest(@"D:\MarsAdvancedTaskPart2\TestData\EditManageListingwithinvalid.json");
        }

        [Then(@"an error message ""([^""]*)"" should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed(string expectedMessage)
        {           

            try
            {
                string actualMessage = manageListingsObj.GetMessage();
                Assert.That(actualMessage, Is.EqualTo(expectedMessage), $"Expected '{expectedMessage}', but got '{actualMessage}'.");

                if (string.IsNullOrEmpty(actualMessage))
                {
                    test.Log(Status.Fail, $"Test failed with empty message.");
                    Assert.Fail("Expected message was not displayed.");
                }
                else
                {
                    test.Log(Status.Pass, "Test passed successfully");
                    TakeScreenshotWithPngFormat();
                }
            }
              catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw;
            }
            finally
            {
                homepageObj.clickmanagelisting();

                string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json";
                List<ManageListingsModel> AddSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
                ManageListingsModel addShareSkill = AddSkilldata.FirstOrDefault();

                string jsonDataFile2 = @"D:\MarsAdvancedTaskPart2\TestData\EditManageListingwithempty.json";
                List<ManageListingsModel> EditSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile2);
                ManageListingsModel EditShareSkill = EditSkilldata.LastOrDefault();

                string jsonDataFile3 = @"D:\MarsAdvancedTaskPart2\TestData\EditManageListingwithempty.json";
                List<ManageListingsModel> EditSkilldata1 = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile3);
                ManageListingsModel EditShareSkill1 = EditSkilldata1.LastOrDefault();

                try
                {
                    if (!string.IsNullOrEmpty(EditShareSkill1.Title))
                    {

                        CommonDriver.ManageListingToCleanUp.Add(EditShareSkill1.Title);


                    }
                    if (!string.IsNullOrEmpty(EditShareSkill1.Title))
                    {

                        CommonDriver.ManageListingToCleanUp.Add(EditShareSkill1.Title);


                    }
                    if (!string.IsNullOrEmpty(addShareSkill.Title))
                    {
                        CommonDriver.ManageListingToCleanUp.Add(addShareSkill.Title);

                    }
                }
                catch (Exception cleanupEx)
                {
                    test.Log(Status.Fail, $"Failed to add title during cleanup: {cleanupEx.Message}");
                }
            }
        }

        [When(@"the user modifies the listing details with empty data from json file")]
        public void WhenTheUserModifiesTheListingDetailsWithEmptyDataFromJsonFile()
        {
            RunUpdateListingTest(@"D:\MarsAdvancedTaskPart2\TestData\EditManageListingwithempty.json");
        }

        [When(@"the user views a listing from the listing table")]
        public void WhenTheUserViewsAListingFromTheListingTable()
        {
            manageListingsObj.view();
        }

        [Then(@"the listing details should be displayed correctly")]
        public void ThenTheListingDetailsShouldBeDisplayedCorrectly()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json";
            List<ManageListingsModel> AddSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
            ManageListingsModel addShareSkill = AddSkilldata.FirstOrDefault();

            try
            {
                IWebElement Verifytitle = driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[1]/div[1]/div[2]/h1/span"));
                string addedTitle = Verifytitle.Text;

                // Log the expected and actual skill title values for debugging purposes
                Console.WriteLine($"Expected Skill Title: {addShareSkill.Title}");
                Console.WriteLine($"Actual Skill Title: {addedTitle}");

                // Assert that the actual skill title matches the expected title
                Assert.That(addedTitle, Is.EqualTo(addShareSkill.Title), "Actual skill title and Expected skill title do not match");

                // Assert that the added skill is not null or empty (to ensure that it was successfully added)
                Assert.That(!string.IsNullOrEmpty(addedTitle), $"Expected skill title was not added. Actual skill title: '{addedTitle}'");

                // If all assertions succeed, log the test as passed
                test.Log(Status.Pass, "The listing details should be displayed correctly");
            }
            catch (WebDriverTimeoutException ex)
            {
                test.Log(Status.Fail, $"Timed out waiting for the message: {ex.Message}");
                throw;
            }

            catch (Exception ex)
            {
                // Log any other unexpected exception
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                TakeScreenshotWithPngFormat();
                throw;
            }
            finally
            {
                homepageObj.clickmanagelisting();

                try
                {
                    if (!string.IsNullOrEmpty(addShareSkill.Title))
                    {
                        CommonDriver.ManageListingToCleanUp.Add(addShareSkill.Title);
                    }
                }
                catch (Exception cleanupEx)
                {
                    test.Log(Status.Fail, $"Failed to add title during cleanup: {cleanupEx.Message}");
                }
            }
        }

        [When(@"the user deletes an existing listing from the listing table")]
        public void WhenTheUserDeletesAnExistingListingFromTheListingTable()
        {
            RunDeleteListingTest(@"D:\MarsAdvancedTaskPart2\TestData\DeleteManageListingWhichisinthelist.json");
        }

        [Then(@"the listing should be removed from the listing table")]
        public void ThenTheListingShouldBeRemovedFromTheListingTable()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\DeleteManageListingWhichisinthelist.json";
            List<ManageListingsModel> deleteSkillData = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
            ManageListingsModel deletedData = deleteSkillData.FirstOrDefault();

            try
            {
               
                // Verify that the degree is no longer present
                var skillAfterDeletion = manageListingsObj.GetList();
                foreach (var title in CommonDriver.ManageListingToCleanUp)
                {
                    if (skillAfterDeletion.Contains(title))
                    {
                        test.Log(Status.Fail, $"title '{title}' was not deleted successfully.");
                        Assert.Fail($"title '{title}' was not deleted from the UI.");
                    }
                }

                test.Log(Status.Pass, "degree were successfully deleted.");
                TakeScreenshotWithPngFormat();
               
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw; // Ensure the test fails if an exception occurs
            }
        }

        [When(@"the user deletes data which is not in the listing table")]
        public void WhenTheUserDeletesDataWhichIsNotInTheListingTable()
        {
            RunDeleteListingTest(@"D:\MarsAdvancedTaskPart2\TestData\DeleteManageListingWhichisnotinthelist.json");

        }

        [Then(@"the listing should not be removed from the listing table")]
        public void ThenTheListingShouldNotBeRemovedFromTheListingTable()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json";
            List<ManageListingsModel> AddSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
            ManageListingsModel addShareSkill = AddSkilldata.FirstOrDefault();

            try
            {

                string jsonDataFile2 = @"D:\MarsAdvancedTaskPart2\TestData\DeleteManageListingWhichisnotinthelist.json";
                List<ManageListingsModel> deleteSkillData = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile2);
                ManageListingsModel deletedData = deleteSkillData.FirstOrDefault();


                // Capture the list of certificate before attempting to delete non-existent data
                var skillBeforeDeletion = manageListingsObj.GetList(); // Assume this method retrieves all certificate from the UI


                // Capture the list of certificate after the deletion attempt
                var skillAfterDeletionAttempt = manageListingsObj.GetList();

                // Verify that the list of certificate remains unchanged
                Assert.That(skillAfterDeletionAttempt, Is.EquivalentTo(skillBeforeDeletion), "The list of ce` should remain unchanged when attempting to delete non-existent data.");
               
                // Log success and take a screenshot
                test.Log(Status.Pass, "Non-existent Certificate data was not deleted, as expected.");
                TakeScreenshotWithPngFormat();
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw; // Ensure the test fails if an exception occurs
            }
            finally
            {                
                try
                {
                    if (!string.IsNullOrEmpty(addShareSkill.Title))
                    {
                        CommonDriver.ManageListingToCleanUp.Add(addShareSkill.Title);
                    }
                }
                catch (Exception cleanupEx)
                {
                    test.Log(Status.Fail, $"Failed to add title during cleanup: {cleanupEx.Message}");
                }
            }

        }

        [When(@"the user disables the toggle for the listing")]
        public void WhenTheUserDisablesTheToggleForTheListing()
        {
            Thread.Sleep(2000);
            manageListingsObj.TestToggleCheckbox();
            Thread.Sleep(5000);

        }

        [Then(@"the toggle should be in the disabled state")]
        public void ThenTheToggleShouldBeInTheDisabledState()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json";
            List<ManageListingsModel> AddSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
            ManageListingsModel addShareSkill = AddSkilldata.FirstOrDefault();

            try
            {
                // Locate the toggle (checkbox) element using an appropriate locator (in this case, By.Name)
                IWebElement checkbox = driver.FindElement(By.Name("isActive"));

                // Verify if the checkbox is unchecked (disabled)
                bool isChecked = checkbox.Selected;

                // Assert that the checkbox is not selected (disabled)
                if (isChecked)
                {
                    throw new Exception("Toggle is in the enabled state, but it should be disabled.");
                }
                else
                {
                    Console.WriteLine("Toggle is correctly in the disabled state.");
                }

                // Alternatively, use NUnit assertion if you're using NUnit
                // Assert.IsFalse(isChecked, "The toggle should be in the disabled state, but it's enabled.");
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw; // Ensure the test fails if an exception occurs
            }
            finally
            {
                try
                {
                    if (!string.IsNullOrEmpty(addShareSkill.Title))
                    {
                        CommonDriver.ManageListingToCleanUp.Add(addShareSkill.Title);
                    }
                }
                catch (Exception cleanupEx)
                {
                    test.Log(Status.Fail, $"Failed to add title during cleanup: {cleanupEx.Message}");
                }
            }

        }

        [When(@"the user enables the toggle for the listing")]
        public void WhenTheUserEnablesTheToggleForTheListing()
        {
            manageListingsObj.TestToggleCheckbox();
            Thread.Sleep(2000);
           
           
        }

        [Then(@"the toggle should be in the enabled state")]
        public void ThenTheToggleShouldBeInTheEnabledState()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json";
            List<ManageListingsModel> AddSkilldata = JsonUtils.ReadJsonData<ManageListingsModel>(jsonDataFile);
            ManageListingsModel addShareSkill = AddSkilldata.FirstOrDefault();

            try
            {
                IWebElement checkbox = driver.FindElement(By.Name("isActive"));

                // Verify if the checkbox is checked (enabled)
                bool isChecked = checkbox.Selected;

                // Assert that the checkbox is selected (enabled)
                if (!isChecked)
                {
                    throw new Exception("Toggle is in the disabled state, but it should be enabled.");
                }
                else
                {
                    Console.WriteLine("Toggle is correctly in the enabled state.");
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw; // Ensure the test fails if an exception occurs
            }
            finally
            {
                try
                {
                    if (!string.IsNullOrEmpty(addShareSkill.Title))
                    {
                        CommonDriver.ManageListingToCleanUp.Add(addShareSkill.Title);
                    }
                }
                catch (Exception cleanupEx)
                {
                    test.Log(Status.Fail, $"Failed to add title during cleanup: {cleanupEx.Message}");
                }
            }
        }

        [Given(@"User logs into Mars and navigates to the Profile tab")]
        public void GivenUserLogsIntoMarsAndNavigatesToTheProfileTab()
        {
            loginPageObj.Loginsteps2();
        }

        [Given(@"User creates ShareSkill data and logs out from Mars")]
        public void GivenUserCreatesShareSkillDataAndLogsOutFromMars()
        {
            RunAddListingTest(@"D:\MarsAdvancedTaskPart2\TestData\AddManagelistingData.json");
             
            IWebElement Signout = driver.FindElement(By.XPath("//button[@class='ui green basic button']"));
            Signout.Click();
        }

        [When(@"User logs into Mars with Different user")]
        public void WhenUserLogsIntoMarsWithDifferentUser()
        {
            loginPageObj.Loginsteps();
        }

        [When(@"User searches for the skill and navigates to the skill details page")]
        public void WhenUserSearchesForTheSkillAndNavigatesToTheSkillDetailsPage()
        {
            string jsonDataFile = @"D:\MarsAdvancedTaskPart2\TestData\SearchSkill.json";
            List<SearchSkillModel> searchSkilldata = JsonUtils.ReadJsonData<SearchSkillModel>(jsonDataFile);
            foreach (var item in searchSkilldata)
            {
                string searchString = item.searchString;
                searchSkillsObj.SearchSkill(searchString);
            }

        }

        [When(@"User writes a message to the skill owner and sends the request")]
        public void WhenUserWritesAMessageToTheSkillOwnerAndSendsTheRequest()
        {
             searchSkillsObj.SendRequest();
        }

        [Then(@"Confirmation message ""([^""]*)"" should be displayed")]
        public void ThenConfirmationMessageShouldBeDisplayed(string expectedtext)
        {

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement confirmationMessage = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//h3[contains(., 'Requested')]")));

            // Get the actual text from the element
            string actualText = confirmationMessage.Text.Trim();


            // Assert that the actual text matches the expected text ("Requested")
            //Assert.That(expectedtext, actualText, $"Expected confirmation message '{expectedtext}' but found '{actualText}'");
            Assert.That(actualText, Is.EqualTo(expectedtext), "The expected message did not appear.");
                       
        }

    }
}
