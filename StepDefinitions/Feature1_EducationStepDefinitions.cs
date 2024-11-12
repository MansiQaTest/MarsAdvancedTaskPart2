using AventStack.ExtentReports;
using MarsAdvancedTaskPart2.Pages;
using MarsAdvancedTaskPart2.Pages.Components.Profilepage;
using MarsAdvancedTaskPart2.TestModel;
using MarsAdvancedTaskPart2.Utils;
using System;
using System.Diagnostics.Metrics;
using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsAdvancedTaskPart2.StepDefinitions
{
    [Binding, Scope(Feature = "Feature1_Education")]
    public class Feature1_EducationStepDefinitions : CommonDriver
    {
        private readonly ScenarioContext _scenarioContext;
        ProfileTabEducation educationobj;
        LoginPage loginPageObj;
        List<string> EducationDataToCleanUp;
        List<string> jsonDataFile;

        public Feature1_EducationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            loginPageObj = new LoginPage();
            educationobj = new ProfileTabEducation();
            EducationDataToCleanUp = new List<string>();
        }
        private void RunEducationTest(string jsonDataFile)
        {
            educationobj.ClickAnyTab("Education");
           
            try
            {

                List<EducationModel> educationData = JsonUtils.ReadJsonData<EducationModel>(jsonDataFile);
                foreach (var item in educationData)
                {

                    string country = item.Country;
                    string university = item.University;
                    string title = item.Title;
                    string degree = item.Degree;
                    string gradYr = item.GraduationYear;

                    try
                    {
                        educationobj.CreateEducation(country, university, title, degree, gradYr);
                        // Add the degree to the list for cleanup if added successfully
                        EducationDataToCleanUp.Add(degree);
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
        private void RunDeleteEducationTest(string jsonDataFile)
        {
            educationobj.ClickAnyTab("Education");
            
            try
            {
                List<EducationModel> educationData = JsonUtils.ReadJsonData<EducationModel>(jsonDataFile);
                foreach (var item in educationData)
                {
                    string degree = item.Degree;

                    try
                    {
                        educationobj.DeleteTestData(degree);

                    }
                    catch (Exception ex)
                    {
                        test.Log(Status.Fail, $"Failed to delete education to degree {degree}: {ex.Message}");
                        throw; // Ensure the test fails if the education could not be delete
                    }
                }
            }
            catch (Exception e)
            {
                test.Log(Status.Fail, e.ToString());
                throw;
            }
        }

        [Given(@"User Logs into Mars")]
        public void GivenUserLogsIntoMars()
        {
            loginPageObj.Loginsteps();
        }

        [Given(@"User navigates to Education tab")]
        public void GivenUserNavigatesToEducationTab()
        {
            educationobj.ClickAnyTab("Education");
        }

        [When(@"User adds a new Education record from the JSON file")]
        public void WhenUserAddsANewEducationRecordFromTheJSONFile()
        {
            RunEducationTest(@"D:\MarsAdvancedTaskPart2\TestData\AddEducation.json");
            
        }

        [Then(@"the new record should be successfully created based on JSON data")]
        public void ThenTheNewRecordShouldBeSuccessfullyCreatedBasedOnJSONData()
        {
            string degree = EducationDataToCleanUp.First();
            try
            {
                string addedEducation = educationobj.GetEducation();
                Console.WriteLine($"Expected Degree: {degree}");
                Console.WriteLine($"Actual Degree: {addedEducation}");
                Assert.That(addedEducation == degree, "Actual Educationname and Expected Educationname do not match");
                if (string.IsNullOrEmpty(addedEducation))
                {
                    test.Log(Status.Fail, $"Test failed with exception: {addedEducation}");
                    Assert.Fail($"Expected Educationname was not added. Actual Educationname: '{addedEducation}'");
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
                // Clean up test data
                CommonDriver.EducationDataToCleanUp.Add(degree);
            }
        }

        [When(@"User adds a new Education record from the JSON file where name is empty")]
        public void WhenUserAddsANewEducationRecordFromTheJSONFileWhereNameIsEmpty()
        {
            RunEducationTest(@"D:\MarsAdvancedTaskPart2\TestData\AddEducationDatawithempty.json");
        }

        [Then(@"Error message ""([^""]*)"" should be displayed")]
        public void ThenErrorMessageShouldBeDisplayed(string expectedErrorMessage)
        {
            string degree = EducationDataToCleanUp.First();

            try
            {
               
                string actualMessage = educationobj.GetMessage();
                Assert.That(actualMessage, Is.EqualTo(expectedErrorMessage), $"Expected '{expectedErrorMessage}', but got '{actualMessage}'.");

                if (string.IsNullOrEmpty(actualMessage))
                {
                    test.Log(Status.Fail, $"Test failed with empty error message.");
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
                // Cleanup step: Add degree back to the cleanup list
                CommonDriver.EducationDataToCleanUp.Add(degree);
            }
        }


        [When(@"User tries to add the same Education record again")]
        public void WhenUserTriesToAddTheSameEducationRecordAgain()
        {
            RunEducationTest(@"D:\MarsAdvancedTaskPart2\TestData\AddEducationWithDuplicateEntry.json");
        }

        [When(@"User adds multiple Education records from the JSON file")]
        public void WhenUserAddsMultipleEducationRecordsFromTheJSONFile()
        {
            RunEducationTest(@"D:\MarsAdvancedTaskPart2\TestData\CreateEduMultipleData.json");

        }

        [Then(@"All Education records should be successfully created")]
        public void ThenAllEducationRecordsShouldBeSuccessfullyCreated()
        {

            List<EducationModel> degrees = JsonConvert.DeserializeObject<List<EducationModel>>(File.ReadAllText(@"D:\MarsAdvancedTaskPart2\TestData\CreateEduMultipleData.json"));
            List<string> expectedDegrees = degrees.Select(c => c.Degree).ToList();

            // Wait for the degrees to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var actualDegrees = new List<string>();

            for (int i = 0; i < expectedDegrees.Count; i++)
            {
                // Construct the XPath for each degree dynamically
                string degreeXPath = $"//div[@data-tab='third']//tbody[{i + 1}]/tr/td[4]";

                try
                {
                    // Wait for each degree element to be visible
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(degreeXPath)));

                    // Get the actual degree text
                    IWebElement degreeElement = driver.FindElement(By.XPath(degreeXPath));
                    actualDegrees.Add(degreeElement.Text.Trim());
                }
                catch (NoSuchElementException)
                {
                    // Log if an element is not found
                    test.Log(Status.Fail, $"Degree element at index {i + 1} not found using XPath: {degreeXPath}");
                    Assert.Fail($"Degree element at index {i + 1} not found.");
                }
                catch (Exception ex)
                {
                    // Log any other exception
                    test.Log(Status.Fail, $"Exception occurred while getting degree element: {ex.Message}");
                    throw;
                }
            }

            // Compare actual degrees with expected degrees
            foreach (var degree in expectedDegrees)
            {
                if (!actualDegrees.Contains(degree))
                {
                    test.Log(Status.Fail, $"Expected degree '{degree}' was not found in the UI. Actual degrees: {string.Join(", ", actualDegrees)}");
                    Assert.Fail($"Expected degree '{degree}' was not found. Actual degrees: {string.Join(", ", actualDegrees)}");
                }
            }

            test.Log(Status.Pass, "All degrees were successfully created and verified.");
            TakeScreenshotWithPngFormat();

            foreach (var degree in expectedDegrees)
            {
                try
                {
                    CommonDriver.EducationDataToCleanUp.Add(degree);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        [When(@"User adds multiple Educations records from the JSON file with invalid input")]
        public void WhenUserAddsMultipleEducationsRecordsFromTheJSONFileWithInvalidInput()
        {
            RunEducationTest(@"D:\MarsAdvancedTaskPart2\TestData\CreateEducationDatawithInvalidinput.json");
        }

     
        [When(@"User deletes the Education record from the JSON file")]
        public void WhenUserDeletesTheEducationRecordFromTheJSONFile()
        {
            RunDeleteEducationTest(@"D:\MarsAdvancedTaskPart2\TestData\DeleteEduDataWhichisinthelist.json");
        }


        [Then(@"the Education record should be successfully deleted")]
        public void ThenTheEducationRecordShouldBeSuccessfullyDeleted()
        {
            try
            {

                var degreesAfterDeletion = educationobj.GetEducation();
                foreach (var degree in CommonDriver.EducationDataToCleanUp)
                {
                    if (degreesAfterDeletion.Contains(degree))
                    {
                        test.Log(Status.Fail, $"Degree '{degree}' was not deleted successfully.");
                        Assert.Fail($"Degree '{degree}' was not deleted from the UI.");
                    }
                }

                test.Log(Status.Pass, "degree were successfully deleted.");
                TakeScreenshotWithPngFormat();
                EducationDataToCleanUp.Clear();
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw; // Ensure the test fails if an exception occurs
            }
            finally
            {
                if (CommonDriver.EducationDataToCleanUp == null || !CommonDriver.EducationDataToCleanUp.Any())
                {
                    test.Log(Status.Info, "No data to clean up.");
                }
                else
                {
                    try
                    {
                        foreach (var degree in CommonDriver.EducationDataToCleanUp)
                        {
                            test.Log(Status.Info, $"Attempting to delete degree: {degree}");
                        }
                    }
                    catch (Exception cleanupEx)
                    {
                        test.Log(Status.Fail, $"Failed during cleanup operation: {cleanupEx.Message}");
                    }
                }

            }
        }

        [When(@"User deletes a Education record that is not in the list from the JSON file")]
        public void WhenUserDeletesAEducationRecordThatIsNotInTheListFromTheJSONFile()
        {
            RunDeleteEducationTest(@"D:\MarsAdvancedTaskPart2\TestData\DeleteEduDataWhichisnotinthelist.json");

        }


        [Then(@"the Education record should not be deleted")]
        public void ThenTheEducationRecordShouldNotBeDeleted()
        {
            try
            {
                // Add initial education data
                string degree = EducationDataToCleanUp.First();

                // Capture the list of degrees before attempting to delete non-existent data
                var degreesBeforeDeletionAttempt = educationobj.GetEducation(); // Assume this method retrieves all degrees from the UI
                               
                // Capture the list of degrees after the deletion attempt
                var degreesAfterDeletionAttempt = educationobj.GetEducation();

                // Verify that the list of degrees remains unchanged
                Assert.That(degreesAfterDeletionAttempt, Is.EquivalentTo(degreesBeforeDeletionAttempt), "The list of degrees should remain unchanged when attempting to delete non-existent data.");
                educationobj.DeleteTestData(degree);
                // Log success and take a screenshot
                test.Log(Status.Pass, "Non-existent education data was not deleted, as expected.");
                TakeScreenshotWithPngFormat();
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw; // Ensure the test fails if an exception occurs
            }
        }
    }
}
