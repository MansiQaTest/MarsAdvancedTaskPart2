using AventStack.ExtentReports;
using MarsAdvancedTaskPart2.Pages;
using MarsAdvancedTaskPart2.Pages.Components.Profilepage;
using MarsAdvancedTaskPart2.TestModel;
using MarsAdvancedTaskPart2.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using TechTalk.SpecFlow;

namespace MarsAdvancedTaskPart2.StepDefinitions
{
    [Binding, Scope(Feature = "Feature3_ProfileDescription")]
    public class Feature3_ProfileDescriptionStepDefinitions : CommonDriver
    {
        LoginPage loginPageObj;
        Homepage homepageObj;
        ProfileDescription profileDescriptionObj;
        List<string> jsonDataFile;

        public Feature3_ProfileDescriptionStepDefinitions() 
        {
            loginPageObj = new LoginPage(); 
            homepageObj = new Homepage();
            profileDescriptionObj = new ProfileDescription();
        }
        private void RunDescriptionTest(string jsonDataFile)
        {

            try
            {
                List<DescriptionModel> DescriptionData = JsonUtils.ReadJsonData<DescriptionModel>(jsonDataFile);

                foreach (var item in DescriptionData)
                {

                    string description = item.description;                    

                    try
                    {
                        profileDescriptionObj.addDescription(description);
                        
                    }
                    catch (Exception ex)
                    {
                        test.Log(Status.Fail, $"Failed to add description {description}: {ex.Message}");
                        throw; // Ensure the test fails if the description could not be added
                    }

                }
            }
            catch (Exception e)
            {
                test.Log(Status.Fail, e.ToString());
                throw;
            }
        }

        [Given(@"User Logs into Mars & navigates to Profile tab")]
        public void GivenUserLogsIntoMarsNavigatesToProfileTab()
        {
            loginPageObj.Loginsteps();
            homepageObj.clickprofiletab();
        }

        [Given(@"Edit Description Box")]
        public void GivenEditDescriptionBox()
        {
            profileDescriptionObj.ClickEditicon();
        }

        [When(@"the user enters a valid description from the JSON file & clicks the Save button")]
        public void WhenTheUserEntersAValidDescriptionFromTheJSONFileClicksTheButton()
        {
            RunDescriptionTest(@"D:\MarsAdvancedTaskPart2\TestData\AddDescription.json");
        }


        [Then(@"The description should be saved successfully based on JSON data")]
        public void ThenTheDescriptionShouldBeSavedSuccessfullyBasedOnJSONData()
        {
            try
            {
                
                List<DescriptionModel> descriptionData = JsonUtils.ReadJsonData<DescriptionModel>(@"D:\MarsAdvancedTaskPart2\TestData\AddDescription.json");

                foreach (var item in descriptionData)
                {
                    
                    string expectedDescription = item.description;

                    
                    string actualDescription = profileDescriptionObj.GetDescription();

                    Console.WriteLine($"Expected Description: {expectedDescription}");
                    Console.WriteLine($"Actual Description: {actualDescription}");

                    // Compare the actual description with the expected description from JSON
                    Assert.That(actualDescription == expectedDescription, "Actual description and Expected description do not match");

                    if (string.IsNullOrEmpty(actualDescription))
                    {
                        test.Log(Status.Fail, $"Test failed with exception: {actualDescription}");
                        Assert.Fail($"Expected description was not added. Actual description: '{actualDescription}'");
                    }
                    else
                    {
                        test.Log(Status.Pass, "Test passed successfully");
                        TakeScreenshotWithPngFormat();
                    }
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        [When(@"the user enters an empty description from the JSON file & clicks the Save button")]
        public void WhenTheUserEntersAnEmptyDescriptionFromTheJSONFile()
        {
            RunDescriptionTest(@"D:\MarsAdvancedTaskPart2\TestData\AddDescriptionwithempty.json");
        }

        [Then(@"an error message ""([^""]*)"" should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed(string expectedErrorMessage)
        {
            try
            {

                string actualMessage = profileDescriptionObj.GetMessage();
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
        }

        [When(@"the user enters a description with the Maximum Character Limit from the JSON file & clicks the Save button")]
        public void WhenTheUserEntersADescriptionWithExactlyCharactersFromTheJSONFile()
        {
            RunDescriptionTest(@"D:\MarsAdvancedTaskPart2\TestData\AddDescriptionwithMaxlimit.json");
        }
        
        [Then(@"Description should be saved successfully based on JSON data")]
        public void ThenDescriptionShouldBeSavedSuccessfullyBasedOnJSONData()
        {
            try
            {

                List<DescriptionModel> descriptionData = JsonUtils.ReadJsonData<DescriptionModel>(@"D:\MarsAdvancedTaskPart2\TestData\AddDescriptionwithMaxlimit.json");

                foreach (var item in descriptionData)
                {

                    string expectedDescription = item.description;


                    string actualDescription = profileDescriptionObj.GetDescription();

                    Console.WriteLine($"Expected Description: {expectedDescription}");
                    Console.WriteLine($"Actual Description: {actualDescription}");

                    // Compare the actual description with the expected description from JSON
                    Assert.That(actualDescription == expectedDescription, "Actual description and Expected description do not match");

                    if (string.IsNullOrEmpty(actualDescription))
                    {
                        test.Log(Status.Fail, $"Test failed with exception: {actualDescription}");
                        Assert.Fail($"Expected description was not added. Actual description: '{actualDescription}'");
                    }
                    else
                    {
                        test.Log(Status.Pass, "Test passed successfully");
                        TakeScreenshotWithPngFormat();
                    }
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }


        [When(@"the user enters a description with exceed character limit from the JSON file")]
        public void WhenTheUserEntersADescriptionWithExceedCharacterLimitFromTheJSONFile()
        {
            RunDescriptionTest(@"D:\MarsAdvancedTaskPart2\TestData\AddDescriptionwithmorethanlimit.json");

        }

        [Then(@"Maximum Characters should be add in Description box")]
        public void ThenMaximumCharactersShouldBeAddInDescriptionBox()
        {
            try
            {
                // Retrieve the actual saved description from the profile
                string actualDescription = profileDescriptionObj.GetDescription();

                // Assert that the length of the actual description does not exceed 600 characters
                int maxCharacterLimit = 600;

                Console.WriteLine($"Actual description length: {actualDescription.Length}");
                Console.WriteLine($"Actual description: {actualDescription}");

                // Check if the actual description is truncated to 600 characters
                Assert.That(actualDescription.Length <= maxCharacterLimit,
                    $"The description exceeds the maximum character limit. Actual length: {actualDescription.Length}");

                if (actualDescription.Length == maxCharacterLimit)
                {
                    test.Log(Status.Pass, "Description is correctly truncated to the maximum allowed characters (600).");
                    TakeScreenshotWithPngFormat();
                }
                else if (actualDescription.Length < maxCharacterLimit)
                {
                    test.Log(Status.Pass, "Description is saved with less than the maximum allowed characters.");
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An error occurred while verifying the maximum character limit: {ex.Message}");
                throw;
            }
        }


        [Given(@"User enters a valid description from the JSON file & clicks the Save button")]
        public void GivenUserEntersAValidDescriptionFromTheJSONFileClicksTheSaveButton()
        {
            RunDescriptionTest(@"D:\MarsAdvancedTaskPart2\TestData\AddDescription.json");
        }


        [When(@"The user clicks the Save button again without Changes")]
        public void WhenTheUserClicksTheButtonAgainWithoutChanges()
        {
            profileDescriptionObj.ClickEditicon();
            profileDescriptionObj.ClickSave();
        }

        [Then(@"Last Saved Description should be displayed without changes")]
        public void ThenLastSavedDescriptionShouldBeDisplayedWithoutChanges()
        {
            try
            {

                List<DescriptionModel> descriptionData = JsonUtils.ReadJsonData<DescriptionModel>(@"D:\MarsAdvancedTaskPart2\TestData\AddDescription.json");

                foreach (var item in descriptionData)
                {

                    string expectedDescription = item.description;


                    string actualDescription = profileDescriptionObj.GetDescription();

                    Console.WriteLine($"Expected Description: {expectedDescription}");
                    Console.WriteLine($"Actual Description: {actualDescription}");

                    // Compare the actual description with the expected description from JSON
                    Assert.That(actualDescription == expectedDescription, "Actual description and Expected description do not match");

                    if (string.IsNullOrEmpty(actualDescription))
                    {
                        test.Log(Status.Fail, $"Test failed with exception: {actualDescription}");
                        Assert.Fail($"Expected description was not added. Actual description: '{actualDescription}'");
                    }
                    else
                    {
                        test.Log(Status.Pass, "Test passed successfully");
                        TakeScreenshotWithPngFormat();
                    }
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An unexpected error occurred: {ex.Message}");
                throw;
            }
        }

        [When(@"the user enters a description from the JSON file & clicks the Save button")]
        public void WhenTheUserEntersADescriptionFromTheJSONFileClicksTheSaveButton()
        {
            RunDescriptionTest(@"D:\MarsAdvancedTaskPart2\TestData\AddDescriptionwithHTMLScript.json");

        }


        [When(@"the user types characters in the description")]
        public void WhenTheUserTypesCharactersInTheDescription()
        {
            profileDescriptionObj.TypesCharactersInTheDescription();
        }

        [Then(@"the character counter should update with the remaining characters")]
        public void ThenTheCharacterCounterShouldUpdateWithTheRemainingCharacters()
        {
            profileDescriptionObj.CharacterCounterWithTheRemainingCharacters();

        }
    }
}
