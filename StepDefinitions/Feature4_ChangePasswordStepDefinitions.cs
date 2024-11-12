using AventStack.ExtentReports;
using MarsAdvancedTaskPart2.Pages;
using MarsAdvancedTaskPart2.Pages.Components.AccountMenu;
using MarsAdvancedTaskPart2.TestModel;
using MarsAdvancedTaskPart2.Utils;
using System;
using TechTalk.SpecFlow;
using NUnit.Framework;
using Newtonsoft.Json;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;


namespace MarsAdvancedTaskPart2.StepDefinitions
{
    [Binding, Scope(Feature = "Feature4_ChangePassword")]
    public class Feature4_ChangePasswordStepDefinitions : CommonDriver
    {
        LoginPage loginPageObj;
        AccountDetailsDropdown AccountDetailsDropdownObj;
        ChangePassword changePasswordObj;

        public Feature4_ChangePasswordStepDefinitions()
        {
            loginPageObj = new LoginPage();
            AccountDetailsDropdownObj = new AccountDetailsDropdown();
            changePasswordObj = new ChangePassword();
        
        }
        private void RunChangePasswordTest(string jsonDataFile)
        {
                       

            try
            {
                List<ChangePasswordModel> changepasswordData = JsonUtils.ReadJsonData<ChangePasswordModel>(jsonDataFile);

                foreach (var item in changepasswordData)
                {
                    string currentpassword = item.oldPassword;
                    string newpassword = item.newPassword;
                    string confirmpassword = item.confirmPassword;

                    try
                    {
                        changePasswordObj.ChangethePassword(currentpassword, newpassword, confirmpassword);

                    }
                    catch (Exception ex)
                    {
                        test.Log(Status.Fail, $"Failed to change password");
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


        [Given(@"User Logs into Mars")]
        public void GivenUserLogsIntoMars()
        {
            loginPageObj.Loginsteps();

        }

        [Given(@"User navigates to ChangePassword page")]
        public void GivenUserNavigatesToChangePasswordPage()
        {
            AccountDetailsDropdownObj.ClickChangePassword();
        }

        [When(@"the user enters the current password as ValidCurrentPassword, new password as ValidNewPassword, confirms the new password ValidNewPassword & the user clicks the Save button")]
        public void WhenTheUserEntersTheCurrentPasswordAsValidCurrentPasswordNewPasswordAsValidNewPasswordConfirmsTheNewPasswordValidNewPasswordTheUserClicksTheSaveButton()
        {
            RunChangePasswordTest(@"D:\MarsAdvancedTaskPart2\TestData\ChangePasswordwithvaliddata.json");
        }

        [Then(@"a success message ""([^""]*)"" should be displayed")]
        public void ThenASuccessMessageShouldBeDisplayed(string expectedMessage)
        {
           try
            {

                string actualMessage = changePasswordObj.GetMessage();
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
        }

        [When(@"the user enters the current password as InvalidCurrentPassword, new password as ValidNewPassword, confirms the new password ValidNewPassword & the user clicks the Save button")]
        public void WhenTheUserEntersTheCurrentPasswordAsInvalidCurrentPasswordNewPasswordAsValidNewPasswordConfirmsTheNewPasswordValidNewPasswordTheUserClicksTheSaveButton()
        {
            RunChangePasswordTest(@"D:\MarsAdvancedTaskPart2\TestData\Changepasswordwithanincorrectcurrentpassword.json");

        }

        [Then(@"an error message ""([^""]*)"" should be displayed")]
        public void ThenAnErrorMessageShouldBeDisplayed(string expectedMessage)
        {
            {
                try
                {

                    string actualMessage = changePasswordObj.GetMessage();
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
            }
        }

        [When(@"the user enters the current password as ValidCurrentPassword, new password as NewPassword, confirms the new password NewPassword & the user clicks the Save button")]
        public void WhenTheUserEntersTheCurrentPasswordAsValidCurrentPasswordNewPasswordAsNewPasswordConfirmsTheNewPasswordNewPasswordTheUserClicksTheSaveButton()
        {
            RunChangePasswordTest(@"D:\MarsAdvancedTaskPart2\TestData\Changepasswordwithnewpasswordandconfirmpasswordmismatch.json");
        }

        [When(@"the user leaves the current password, new password, and confirm password fields empty & the user clicks the Save button")]
        public void WhenTheUserLeavesTheCurrentPasswordNewPasswordAndConfirmPasswordFieldsEmptyTheUserClicksTheSaveButton()
        {
            RunChangePasswordTest(@"D:\MarsAdvancedTaskPart2\TestData\Changepasswordwithempty.json");
        }

        [When(@"the user enters the current password as ValidCurrentPassword, new password as ValidCurrentPassword, confirms the new password ValidCurrentPassword & the user clicks the Save button")]
        public void WhenTheUserEntersTheCurrentPasswordAsValidCurrentPasswordNewPasswordAsValidCurrentPasswordConfirmsTheNewPasswordValidCurrentPasswordTheUserClicksTheSaveButton()
        {
            RunChangePasswordTest(@"D:\MarsAdvancedTaskPart2\TestData\Changepasswordwiththenewpasswordbeingthesameasthecurrentpassword.json");
        }

        [When(@"the user enters the current password as ValidCurrentPassword, new password as invalid, confirms the new password invalid & the user clicks the Save button")]
        public void WhenTheUserEntersTheCurrentPasswordAsValidCurrentPasswordNewPasswordAsInvalidConfirmsTheNewPasswordInvalidTheUserClicksTheSaveButton()
        {           
            RunChangePasswordTest(@"D:\MarsAdvancedTaskPart2\TestData\Changepasswordwithinvaliddata.json");
        }      

       
    }
}
