using AventStack.ExtentReports;
using MarsAdvancedTaskPart2.Utils;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages.Components.Profilepage
{
    public class ProfileDescription : CommonDriver
    {
        private IWebElement editDescription => driver.FindElement(By.XPath("//span[@class='button']//i[@class='outline write icon']"));
        private IWebElement AddDescription => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/div[1]/textarea"));
        private IWebElement Savebutton => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/button"));
        private IWebElement errormessage => driver.FindElement(By.XPath(e_errormessage));
        private IWebElement characterCounterElement => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/div[2]/p"));
        private IWebElement successmessage => driver.FindElement(By.XPath(e_successmessage));
        private IWebElement message => driver.FindElement(By.XPath(e_message));
        private IWebElement Descriptiondata => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/div/span"));
        private string e_editdescription = "//span[@class='button']//i[@class='outline write icon']";
        private string e_adddescription = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/div[1]/textarea";
        private string e_savebutton = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/button";
        private string e_descriptiondata = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/div/span";
        private string e_errormessage = "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']";
        private string e_successmessage = "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']";
        private string e_message = "//div[@class='ns-box-inner']";
        private string e_characterCounterElement = "//*[@id=\"account-profile-section\"]/div/section[2]/div/div/div/div[3]/div/div/form/div/div/div[2]/div[2]/p";
        public void ClickEditicon()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_editdescription, 10);
            editDescription.Click();
        }

        public void addDescription(string description)
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_adddescription, 10);
            AddDescription.Clear();
            AddDescription.SendKeys(description);

            WaitUtils.WaitToBeClickable(driver, "XPath", e_savebutton, 10);
            Savebutton.Click();

        }
        public void ClickSave()
        {
           
            WaitUtils.WaitToBeClickable(driver, "XPath", e_savebutton, 10);
            Savebutton.Click();

        }
        public string GetErrorMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_errormessage, 1);
            return errormessage.Text;

        }
        public string GetSuccessMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_successmessage, 10);
            return successmessage.Text;
        }
        public string GetMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_message, 10);
            return message.Text;
        }
        public string GetDescription()
        {

            try
            {

                WaitUtils.WaitToBeVisible(driver, "XPath", e_descriptiondata, 10);
                string descriptionText = Descriptiondata.Text;
                return Descriptiondata.Text;
            }
            catch (Exception)
            {
                return "locator not found";
            }
        }

        public void TypesCharactersInTheDescription()
        {
            // Clear the text area before typing
            WaitUtils.WaitToBeClickable(driver, "XPath", e_adddescription, 10);
            AddDescription.Clear();

            // Simulate typing characters into the description box (e.g., 200 characters)
            string inputText = new string('a', 200); // Replace with the number of characters you want to test
            AddDescription.SendKeys(inputText);

            // Log the action
            test.Log(Status.Info, $"Typed {inputText.Length} characters into the description box");
        }
        public void CharacterCounterWithTheRemainingCharacters()
        {
            try
            {
                int maxCharacterLimit = 600;

                // Check if AddDescription element is properly initialized
                if (AddDescription != null)
                {
                    // Get the initial text and length before typing
                    string initialText = AddDescription.GetAttribute("value");
                    int initialTextLength = initialText.Length;
                    int expectedInitialRemainingCharacters = maxCharacterLimit - initialTextLength;

                    // Wait for the character counter element to be visible
                    WaitUtils.WaitToBeVisible(driver, "XPath", e_characterCounterElement, 10);

                    // Get the current value of the character counter
                    string remainingCharactersText = characterCounterElement.Text;
                    string numericPart = System.Text.RegularExpressions.Regex.Match(remainingCharactersText, @"\d+").Value;

                    if (int.TryParse(numericPart, out int actualInitialRemainingCharacters))
                    {
                        Assert.That(actualInitialRemainingCharacters == expectedInitialRemainingCharacters,
                            $"Initial character counter is incorrect. Expected: {expectedInitialRemainingCharacters}, but got: {actualInitialRemainingCharacters}");

                        test.Log(Status.Pass, $"Initial character counter is correct. Remaining characters: {actualInitialRemainingCharacters}");
                    }
                    else
                    {
                        throw new FormatException($"Unable to parse remaining characters: '{remainingCharactersText}'");
                    }
                }
            }
            catch (Exception ex)
            {
                test.Log(Status.Fail, $"An error occurred while verifying the character counter: {ex.Message}. StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        public string GetScreenshot()
        {
            var file = ((ITakesScreenshot)driver).GetScreenshot();
            var img = file.AsBase64EncodedString;
            return img;
        }
    }
}
