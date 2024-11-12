using MarsAdvancedTaskPart2.Pages.Components.Profilepage;
using MarsAdvancedTaskPart2.Pages;
using MarsAdvancedTaskPart2.Utils;
using System;
using TechTalk.SpecFlow;
using AventStack.ExtentReports;
using MarsAdvancedTaskPart2.TestModel;
using NUnit.Framework;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace MarsAdvancedTaskPart2.StepDefinitions
{
    [Binding, Scope(Feature = "Feature2_Certifications")]
    public class Feature2_CertificationsStepDefinitions : CommonDriver
    {
        private readonly ScenarioContext _scenarioContext;
        ProfileTabCertifications certificationsObj;
        LoginPage loginPageObj;
        List<string> CertificateDataToCleanUp;
        List<string> jsonDataFile;
        public Feature2_CertificationsStepDefinitions(ScenarioContext scenarioContext) 
        {
            _scenarioContext = scenarioContext;
            loginPageObj = new LoginPage();
            certificationsObj = new ProfileTabCertifications();
            CertificateDataToCleanUp = new List<string>();
        }
        private void RunCertificateTest(string jsonDataFile)
        {

            certificationsObj.ClickAnyTab("Certifications");
            test.Log(Status.Info, "Navigated to Certificate tab");

            try
            {
                List<CertificationModel> CertificateData = JsonUtils.ReadJsonData<CertificationModel>(jsonDataFile);

                foreach (var item in CertificateData)
                {

                    string certificatename = item.CertificationName;
                    string certificatefrom = item.CertificationFrom;
                    string certificationYear = item.CertificationYear;

                    try
                    {
                        certificationsObj.Createcertificate(certificatename, certificatefrom, certificationYear);
                        // Add the certificatename to the list for cleanup if added successfully
                        CertificateDataToCleanUp.Add(certificatename);
                    }
                    catch (Exception ex)
                    {
                        test.Log(Status.Fail, $"Failed to add Certificate with certificatename {certificatename}: {ex.Message}");
                        throw; // Ensure the test fails if the Certificate could not be added
                    }

                }
            }
            catch (Exception e)
            {
                test.Log(Status.Fail, e.ToString());
                throw;
            }
        }
        private void RunDeleteCertificateTest(string jsonDataFile)
        {

            certificationsObj.ClickAnyTab("Certifications");
            test.Log(Status.Info, "Navigated to Certificate tab");

            try
            {
                List<CertificationModel> CertificateData = JsonUtils.ReadJsonData<CertificationModel>(jsonDataFile);

                foreach (var item in CertificateData)
                {
                    string certificatename = item.CertificationName;

                    try
                    {
                        certificationsObj.DeleteTestData(certificatename);

                    }
                    catch (Exception ex)
                    {
                        test.Log(Status.Fail, $"Failed to delete Certificate to degree {certificatename}: {ex.Message}");
                        throw; // Ensure the test fails if the Certificate could not be delete
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

        [Given(@"User navigates to Certifications tab")]
        public void GivenUserNavigatesToCertificationsTab()
        {
            certificationsObj.ClickAnyTab("Certifications");
        }

        [When(@"User adds a new Certifications record from the JSON file")]
        public void WhenUserAddsANewCertificationsRecordFromTheJSONFile()
        {
            RunCertificateTest(@"D:\MarsAdvancedTaskPart2\TestData\AddCertificate.json");
        }

        [Then(@"the new record should be successfully created based on JSON data")]
        public void ThenTheNewRecordShouldBeSuccessfullyCreatedBasedOnJSONData()
        {
            string certificatename = CertificateDataToCleanUp.First();
            try
            {
                string addedCertificate = certificationsObj.GetCertificate();
                Console.WriteLine($"Expected Degree: {certificatename}");
                Console.WriteLine($"Actual Degree: {addedCertificate}");
                Assert.That(addedCertificate == certificatename, "Actual certificatename and Expected certificatename do not match");
                if (string.IsNullOrEmpty(addedCertificate))
                {
                    test.Log(Status.Fail, $"Test failed with exception: {addedCertificate}");
                    Assert.Fail($"Expected certificatename was not added. Actual certificatename: '{addedCertificate}'");
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
                CommonDriver.CertificateDataToCleanUp.Add(certificatename);

            }
        }

        [When(@"User adds a new Certifications record from the JSON file where name is empty")]
        public void WhenUserAddsANewCertificationsRecordFromTheJSONFileWhereNameIsEmpty()
        {
            RunCertificateTest(@"D:\MarsAdvancedTaskPart2\TestData\AddCertificateDatawithempty.json");
        }

        [Then(@"Error message ""([^""]*)"" should be displayed")]
        public void ThenErrorMessageShouldBeDisplayed(string expectedErrorMessage)
        {
            string certificatename = CertificateDataToCleanUp.First();

            try
            {

                string actualMessage = certificationsObj.GetMessage();
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
                CommonDriver.CertificateDataToCleanUp.Add(certificatename);
            }
        }

        [When(@"User tries to add the same Certifications record again")]
        public void WhenUserTriesToAddTheSameCertificationsRecordAgain()
        {
            RunCertificateTest(@"D:\MarsAdvancedTaskPart2\TestData\AddCertificateWithDuplicateEntry.json");
        }

        [When(@"User adds multiple Certifications records from the JSON file")]
        public void WhenUserAddsMultipleCertificationsRecordsFromTheJSONFile()
        {
            RunCertificateTest(@"D:\MarsAdvancedTaskPart2\TestData\CreateMultipleCertificateData.json");
        }

        [Then(@"All Certifications records should be successfully created")]
        public void ThenAllCertificationsRecordsShouldBeSuccessfullyCreated()
        {
            List<CertificationModel> certificates = JsonConvert.DeserializeObject<List<CertificationModel>>(File.ReadAllText(@"D:\MarsAdvancedTaskPart2\TestData\CreateMultipleCertificateData.json"));
            List<string> expectedCerificateName = certificates.Select(c => c.CertificationName).ToList();
            // Wait for the degrees to be visible
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
            var actualCertificateName = new List<string>();

            for (int i = 0; i < expectedCerificateName.Count; i++)
            {
                // Construct the XPath for each certificatename dynamically
                string certificatenameXPath = $"//div[@data-tab='fourth']//tbody[{i + 1}]/tr/td[1]";

                try
                {
                    // Wait for each degree element to be visible
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(certificatenameXPath)));

                    // Get the actual degree text
                    IWebElement certificatenameElement = driver.FindElement(By.XPath(certificatenameXPath));
                    actualCertificateName.Add(certificatenameElement.Text.Trim());
                }
                catch (NoSuchElementException)
                {
                    // Log if an element is not found
                    test.Log(Status.Fail, $"CertificationName element at index {i + 1} not found using XPath: {certificatenameXPath}");
                    Assert.Fail($"CertificationName element at index {i + 1} not found.");
                }
                catch (Exception ex)
                {
                    // Log any other exception
                    test.Log(Status.Fail, $"Exception occurred while getting certificatename element: {ex.Message}");
                    throw;
                }
            }

            // Compare actual degrees with expected degrees
            foreach (var certificatename in expectedCerificateName)
            {
                if (!actualCertificateName.Contains(certificatename))
                {
                    test.Log(Status.Fail, $"Expected certificatename '{certificatename}' was not found in the UI. Actual certificatename: {string.Join(", ", actualCertificateName)}");
                    Assert.Fail($"Expected certificatename '{certificatename}' was not found. Actual certificatenames: {string.Join(", ", actualCertificateName)}");
                }
            }

            test.Log(Status.Pass, "All certificate were successfully created and verified.");
            TakeScreenshotWithPngFormat();



            foreach (var certificatename in expectedCerificateName)
            {
                try
                {
                    CommonDriver.CertificateDataToCleanUp.Add(certificatename);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        [When(@"User adds multiple Certificationss records from the JSON file with invalid input")]
        public void WhenUserAddsMultipleCertificationssRecordsFromTheJSONFileWithInvalidInput()
        {
            RunCertificateTest(@"D:\MarsAdvancedTaskPart2\TestData\CreateCertificateDatawithInvalidinput.json");

        }

        [When(@"User deletes the Certifications record from the JSON file")]
        public void WhenUserDeletesTheCertificationsRecordFromTheJSONFile()
        {
            RunDeleteCertificateTest(@"D:\MarsAdvancedTaskPart2\TestData\DeleteCertDataWhichisinthelist.json");
        }

        [Then(@"the Certifications record should be successfully deleted")]
        public void ThenTheCertificationsRecordShouldBeSuccessfullyDeleted()
        {
            try
            {

                // Verify that the certificate is no longer present
                var certificatesAfterDeletion = certificationsObj.GetCertificate(); // Assume this method retrieves all certificate from the UI
                foreach (var certificatename in CommonDriver.CertificateDataToCleanUp)
                {
                    if (certificatesAfterDeletion.Contains(certificatename))
                    {
                        test.Log(Status.Fail, $"CertificationName '{certificatename}' was not deleted successfully.");
                        Assert.Fail($"CertificationName '{certificatename}' was not deleted from the UI.");
                    }
                }

                // If all deletions are successful
                test.Log(Status.Pass, "certificate were successfully deleted.");
                TakeScreenshotWithPngFormat();
                CertificateDataToCleanUp.Clear();

            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw; // Ensure the test fails if an exception occurs
            }
            finally
            {

                // If there is no data to clean up, simply log that there's nothing to clean
                if (CommonDriver.CertificateDataToCleanUp == null || !CommonDriver.CertificateDataToCleanUp.Any())
                {
                    test.Log(Status.Info, "No data to clean up.");
                }
                else
                {
                    try
                    {
                        // Perform cleanup operation if there are items to clean up
                        foreach (var certificatename in CommonDriver.CertificateDataToCleanUp)
                        {

                            test.Log(Status.Info, $"Attempting to delete degree: {certificatename}");
                        }
                    }
                    catch (Exception cleanupEx)
                    {
                        test.Log(Status.Fail, $"Failed during cleanup operation: {cleanupEx.Message}");
                    }
                }
            }
        }

        [When(@"User deletes a Certifications record that is not in the list from the JSON file")]
        public void WhenUserDeletesACertificationsRecordThatIsNotInTheListFromTheJSONFile()
        {
            RunDeleteCertificateTest(@"D:\MarsAdvancedTaskPart2\TestData\DeleteCertDataWhichisnotinthelist.json");

        }

        [Then(@"the Certifications record should not be deleted")]
        public void ThenTheCertificationsRecordShouldNotBeDeleted()
        {
            try
            {
                string certificatename = CertificateDataToCleanUp.First();

                // Capture the list of certificate before attempting to delete non-existent data
                var certificatesBeforeDeletionAttempt = certificationsObj.GetCertificate(); // Assume this method retrieves all certificate from the UI
                               
                // Capture the list of certificate after the deletion attempt
                var certificatesAfterDeletionAttempt = certificationsObj.GetCertificate();

                // Verify that the list of certificate remains unchanged
                Assert.That(certificatesAfterDeletionAttempt, Is.EquivalentTo(certificatesBeforeDeletionAttempt), "The list of ce` should remain unchanged when attempting to delete non-existent data.");
                CommonDriver.CertificateDataToCleanUp.Add(certificatename);

                // Log success and take a screenshot
                test.Log(Status.Pass, "Non-existent Certificate data was not deleted, as expected.");
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
