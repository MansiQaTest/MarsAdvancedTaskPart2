using MarsAdvancedTaskPart2.Utils;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using MarsAdvancedTaskPart2.TestModel;
using RazorEngine;

namespace MarsAdvancedTaskPart2.Pages.Components
{
    public class ManageListings : CommonDriver
    {
        Homepage homepageObj;
        LoginPage loginPageObj;
        ManageRequests manageRequestsObj;
        CommonDriver commonDriver;
        private IWebElement ShareSkillButton => driver.FindElement(By.XPath("//div[@class='right item']//a[@class='ui basic green button']"));
        private IWebElement ManageListingButton => driver.FindElement(By.XPath("//div[@class='right item']//a[@class='ui basic green button']"));
        private IWebElement Editicon => driver.FindElement(By.XPath("//button[i[@class='outline write icon']]"));
        private IWebElement viewicon => driver.FindElement(By.XPath("//button[i[@class='eye icon']]"));
        private IWebElement buttonDelete => driver.FindElement(By.XPath("//button[i[@class='remove icon']]"));
        private IWebElement Selectyesoption => driver.FindElement(By.XPath("/html/body/div[2]/div/div[3]/button[2]"));
        private IWebElement title => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input"));
        private IWebElement description => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[2]/div/div[2]/div[1]/textarea"));
        private IWebElement category => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div/select"));
        private IWebElement SubCategory => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div[2]/div[1]/select"));
        private IWebElement tags => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[4]/div[2]/div/div/div/div/input"));
        private IWebElement ServiceSelection1 => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[1]"));
        private IWebElement ServiceSelection2 => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[2]"));
        private IWebElement LocationtypeOnsite => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[1]/div/label"));
        private IWebElement LocationtypeOnline => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[2]/div/label"));
        private IWebElement AvailableDaysStartDate => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[1]/div[2]/input"));
        private IWebElement AvailableDaysEndDate => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[1]/div[2]/input"));
        private IWebElement SkillTradeSkillExchange => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[2]/div/div[1]/div/label"));
        private IWebElement SkillTradeCredit => driver.FindElement(By.XPath("/html/body/div/div/div[1]/div[2]/div/form/div[8]/div[2]/div/div[2]/div/input"));
        private IWebElement SkillExchangeData => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[4]/div/div/div/div/div/input"));
        private IWebElement CreditAmount => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[4]/div/div/input"));
        private IWebElement StatusActive => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[1]/div/label"));
        private IWebElement StatusHidden => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[2]/div/label"));
        private IWebElement WorkSample => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[9]/div/div[2]/section/div/label/div/span/i"));
        private IWebElement Save => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[11]/div/input[1]"));
        private IWebElement Cancel => driver.FindElement(By.XPath("//*[@id=\"service-listing-section\"]/div[2]/div/form/div[11]/div/input[2]"));
        private IWebElement errormessage => driver.FindElement(By.XPath(e_errormessage));
        private IWebElement successmessage => driver.FindElement(By.XPath(e_successmessage));
        private IWebElement message => driver.FindElement(By.XPath(e_message));
        private IWebElement cancelButton => driver.FindElement(By.XPath(e_cancelButton));
        private IWebElement tabOption => driver.FindElement(By.XPath("//a[text()='Manage Listings']"));
        private IWebElement Withdraw => driver.FindElement(By.XPath("//button[contains(@class, 'ui negative basic button') and text()='Withdraw']"));

        private string e_waitForTab = "//a[text()='Manage Listings']";
        private string e_withdraw = "//button[contains(@class, 'ui negative basic button') and text()='Withdraw']";
        private IWebElement Completerequest => driver.FindElement(By.XPath("//button[@class='ui positive basic button' and text()='Complete']"));

        private IWebElement ManageListingList => driver.FindElement(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]"));
        private string e_completerequest = "//button[@class='ui positive basic button' and text()='Complete']";
        private string e_errormessage = "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-error ns-show']";
        private string e_titleXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[1]/div/div[2]/div/div[1]/input";
        private string e_successmessage = "//div[@class='ns-box ns-growl ns-effect-jelly ns-type-success ns-show']";
        private string e_shareskillbutton = "//div[@class='right item']//a[@class='ui basic green button']";
        private string e_category = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div/select";
        private string e_subcategory = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[3]/div[2]/div/div[2]/div[1]/select";
        private string e_tagsXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[4]/div[2]/div/div/div/div/input";
        private string e_ServiceSelection1XPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[2]";
        private string e_ServiceSelection2XPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[5]/div[2]/div[1]/div[2]";
        private string e_shareskilllist = "//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr[1]/td[3]";
        private string e_LocationtypeOnlineXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[2]/div/label";
        private string e_LocationtypeOnsiteXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[6]/div[2]/div/div[1]/div/label";
        private string e_SkillTradeCredit = "/html/body/div/div/div[1]/div[2]/div/form/div[8]/div[2]/div/div[2]/div/input";
        private string e_creditamount = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[4]/div/div/input";
        private string e_SkillTradeSkillExchangeXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[2]/div/div[1]/div/label";
        private string e_SkillExchangeDataXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[8]/div[4]/div/div/div/div/div/input";
        private string e_StatusActiveXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[1]/div/label";
        private string e_StatusHiddenXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[10]/div[2]/div/div[2]/div/label";
        private string e_edit = "//button[i[@class='outline write icon']]";
        private string e_AvailableDaysStartDateXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[1]/div[2]/input";
        private string e_AvailableDaysEndDateXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[7]/div[2]/div/div[1]/div[2]/input";
        private string e_SaveXPath = "//*[@id=\"service-listing-section\"]/div[2]/div/form/div[11]/div/input[1]";
        private string e_message = "//div[@class='ns-box-inner']";
        private string e_viewicon = "//button[i[@class='eye icon']]";
        private string e_buttonDelete = "//button[i[@class='remove icon']]";
        private string e_selectyesoption = "/html/body/div[2]/div/div[3]/button[2]";
        private string e_cancelButton = "//input[@value='Cancel']";

        public ManageListings()
        {
            homepageObj = new Homepage();
            loginPageObj = new LoginPage();
            manageRequestsObj = new ManageRequests();
            commonDriver = new CommonDriver();
        }
        public void view()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_viewicon, 10);
            viewicon.Click();
        }
        public void CreateShareSkill(ManageListingsModel AddShareSkill)
        {

            // Wait for and click the 'Add New' button
            WaitUtils.WaitToBeClickable(driver, "XPath", e_shareskillbutton, 10);
            ShareSkillButton.Click();

            // Enter Skill Details
            WaitUtils.WaitToBeClickable(driver, "XPath", e_titleXPath, 10);
            title.SendKeys(AddShareSkill.Title);

            description.SendKeys(AddShareSkill.Description);

            // Select Category
            WaitUtils.WaitToBeClickable(driver, "XPath", e_category, 10);
            category.SendKeys(AddShareSkill.Category);
            category.SendKeys(Keys.Enter); // Press Enter to select category

            // Select Subcategory
            WaitUtils.WaitToBeClickable(driver, "XPath", e_subcategory, 10);
            SubCategory.Click();
            SubCategory.SendKeys(AddShareSkill.SubCategory);
            SubCategory.SendKeys(Keys.Enter); // Press Enter to select subcategory

            // Enter Tags
            WaitUtils.WaitToBeClickable(driver, "XPath", e_tagsXPath, 10);
            tags.Click();
            tags.SendKeys(AddShareSkill.Tags);
            tags.SendKeys(Keys.Enter); // Press Enter to submit tags

            // Select Service Type
            if (AddShareSkill.ServiceType == ServiceSelection1.Text)
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_ServiceSelection1XPath, 10);
                ServiceSelection1.Click();
            }
            else
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_ServiceSelection2XPath, 10);
                ServiceSelection2.Click();
            }

            // Select Location Type
            if (AddShareSkill.LocationType == LocationtypeOnline.Text)
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_LocationtypeOnlineXPath, 10);
                LocationtypeOnline.Click();
            }
            else
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_LocationtypeOnsiteXPath, 10);
                LocationtypeOnsite.Click();
            }

            // Set Available Dates
            WaitUtils.WaitToBeClickable(driver, "XPath", e_AvailableDaysStartDateXPath, 10);
            AvailableDaysStartDate.Click();
            AvailableDaysStartDate.SendKeys(AddShareSkill.StartDate);

            WaitUtils.WaitToBeClickable(driver, "XPath", e_AvailableDaysEndDateXPath, 10);
            AvailableDaysEndDate.Click();
            AvailableDaysEndDate.SendKeys(AddShareSkill.EndDate);
            Thread.Sleep(1000);

            // Select Skill Trade or Credit
            if (AddShareSkill.SkillTrade == "Skill-exchange")
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_SkillTradeSkillExchangeXPath, 10);
                SkillTradeSkillExchange.Click();
                WaitUtils.WaitToBeVisible(driver, "XPath", e_SkillExchangeDataXPath, 10);
                SkillExchangeData.Click();
                SkillExchangeData.SendKeys(AddShareSkill.SkillExchange);
                SkillExchangeData.SendKeys(Keys.Enter);
            }
            else
            {
                //WaitUtils.WaitToBeVisible(driver, "XPath", e_SkillTradeCredit, 10);
                //aitUtils.WaitToBeClickable(driver, "XPath", e_SkillTradeCredit, 20);
                SkillTradeCredit.Click();
                WaitUtils.WaitToBeVisible(driver, "XPath", e_creditamount, 20);
                CreditAmount.Click();
                CreditAmount.SendKeys(AddShareSkill.Credit);
            }

            // Set Status
            if (AddShareSkill.Active == StatusActive.Text)
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_StatusActiveXPath, 10);
                StatusActive.Click();
            }
            else
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_StatusHiddenXPath, 10);
                StatusHidden.Click();
            }

            // Save
            WaitUtils.WaitToBeClickable(driver, "XPath", e_SaveXPath, 10);
            Save.Click();
        }
        public void UpdateManageListing(ManageListingsModel EditManageListing)
        {
            // homepageObj.clickmanagelisting();
            // Wait for any popups to disappear
            //WaitForPopupToDisappear();

            // Wait for and click the 'Add New' button
            WaitUtils.WaitToBeClickable(driver, "XPath", e_edit, 10);
            Editicon.Click();

            // Enter Skill Details
            WaitUtils.WaitToBeClickable(driver, "XPath", e_titleXPath, 10);
            title.Clear();
            title.SendKeys(EditManageListing.Title);
            description.Clear();
            description.SendKeys(EditManageListing.Description);

            // Select Category
            WaitUtils.WaitToBeClickable(driver, "XPath", e_category, 10);
            category.SendKeys(EditManageListing.Category);
            category.SendKeys(Keys.Enter); // Press Enter to select category

            // Select Subcategory
            WaitUtils.WaitToBeClickable(driver, "XPath", e_subcategory, 10);
            SubCategory.Click();
            SubCategory.SendKeys(EditManageListing.SubCategory);
            SubCategory.SendKeys(Keys.Enter); // Press Enter to select subcategory

            // Enter Tags
            WaitUtils.WaitToBeClickable(driver, "XPath", e_tagsXPath, 10);
            tags.Click();
            tags.Clear();
            tags.SendKeys(EditManageListing.Tags);
            tags.SendKeys(Keys.Enter); // Press Enter to submit tags

            // Select Service Type
            if (EditManageListing.ServiceType == ServiceSelection1.Text)
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_ServiceSelection1XPath, 10);

                ServiceSelection1.Click();
            }
            else
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_ServiceSelection2XPath, 10);

                ServiceSelection2.Click();
            }

            // Select Location Type
            if (EditManageListing.LocationType == LocationtypeOnline.Text)
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_LocationtypeOnlineXPath, 10);
                LocationtypeOnline.Click();
            }
            else
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_LocationtypeOnsiteXPath, 10);
                LocationtypeOnsite.Click();
            }

            // Set Available Dates
            WaitUtils.WaitToBeClickable(driver, "XPath", e_AvailableDaysStartDateXPath, 10);
            AvailableDaysStartDate.Click();
            AvailableDaysStartDate.SendKeys(EditManageListing.StartDate);

            WaitUtils.WaitToBeClickable(driver, "XPath", e_AvailableDaysEndDateXPath, 10);
            AvailableDaysEndDate.Click();
            AvailableDaysEndDate.SendKeys(EditManageListing.EndDate);


            // Select Skill Trade or Credit
            if (EditManageListing.SkillTrade == "Skill-exchange")
            {
                try
                {
                    WaitUtils.WaitToBeClickable(driver, "XPath", e_SkillTradeSkillExchangeXPath, 50);
                    SkillTradeSkillExchange.Click();
                    WaitUtils.WaitToBeVisible(driver, "XPath", e_SkillExchangeDataXPath, 10);
                    SkillExchangeData.Click();
                    SkillExchangeData.Clear();
                    SkillExchangeData.SendKeys(EditManageListing.SkillExchange);
                    SkillExchangeData.SendKeys(Keys.Enter);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error interacting with SkillTradeSkillExchange: " + ex.Message);
                    // Optionally, throw the exception or handle it as needed
                }
            }
            else
            {
                // Select Credit option
                WaitUtils.WaitToBeClickable(driver, "XPath", e_SkillTradeCredit, 10);
                SkillTradeCredit.Click();
                WaitUtils.WaitToBeVisible(driver, "XPath", e_creditamount, 20);
                CreditAmount.Click();
                CreditAmount.Clear();
                CreditAmount.SendKeys(EditManageListing.Credit);
            }

            // Set Status
            if (EditManageListing.Active == StatusActive.Text)
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_StatusActiveXPath, 10);
                StatusActive.Click();
            }
            else
            {
                WaitUtils.WaitToBeClickable(driver, "XPath", e_StatusHiddenXPath, 10);
                StatusHidden.Click();
            }

            // Save
            WaitUtils.WaitToBeClickable(driver, "XPath", e_SaveXPath, 10);
            Save.Click();

            //WaitForPopupToDisappear();

            // cancelButton.Click();
        }

        public void ToggleCheckbox(By checkboxLocator)
        {
            // Find the checkbox element
            IWebElement checkbox = driver.FindElement(checkboxLocator);

            // Check the current state (enabled or disabled) using .Selected
            bool isChecked = checkbox.Selected;

            // Print the current state for debugging purposes
            Console.WriteLine("Checkbox is currently: " + (isChecked ? "Checked (Enabled)" : "Unchecked (Disabled)"));

            // If the checkbox is not checked (disabled), click it to enable
            if (!isChecked)
            {
                checkbox.Click();
                Console.WriteLine("Checkbox has been enabled.");
            }
            else
            {
                // If the checkbox is checked (enabled), click it to disable
                checkbox.Click();
                Console.WriteLine("Checkbox has been disabled.");
            }
        }

        public void TestToggleCheckbox()
        {
            // Assuming driver is already initialized and navigated to the correct page
            By checkboxLocator = By.Name("isActive"); // Locating the checkbox by the "name" attribute

            // Call the method to toggle the checkbox
            ToggleCheckbox(checkboxLocator);
        }

        public string GetList()
        {
            //Get last record language text
            try
            {

                WaitUtils.WaitToBeVisible(driver, "XPath", e_shareskilllist, 10);
                return ManageListingList.Text;
            }
            catch (Exception)
            {
                return "locator not found";
            }
        }



        public string GetErrorMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_errormessage, 3);
            return errormessage.Text;
        }
        public string GetSuccessMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_successmessage, 3);
            return successmessage.Text;
        }

        public string GetMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_message, 10);
            return message.Text;
        }
        public void DeleteManageListing(ManageListingsModel DeleteShareSkill)
        {
            IWebElement signOutButton = driver.FindElement(By.XPath("//button[@class='ui green basic button']"));
            signOutButton.Click();
            loginPageObj.Loginsteps2();
                homepageObj.clickmanagelisting();
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                bool isShareSkillFound = false;

                while (true)
                {
                    try
                    {
                        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr")));

                        var rows = driver.FindElements(By.XPath("//*[@id=\"listing-management-section\"]/div[2]/div[1]/div[1]/table/tbody/tr"));

                    foreach (var row in rows)
                    {

                        var titleCell = row.FindElement(By.CssSelector("tr td:nth-child(3)"));

                        if (titleCell.Text.Equals(DeleteShareSkill.Title, StringComparison.OrdinalIgnoreCase))
                        {
                            isShareSkillFound = true;
                            buttonDelete.Click();
                            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
                                    By.XPath("/html/body/div[2]/div/div[3]/button[2]")));
                            Selectyesoption.Click();

                            bool isErrorMessageDisplayed = false;
                            try
                            {
                                isErrorMessageDisplayed = true;
                                WaitUtils.WaitToBeVisible(driver, "XPath", e_errormessage, 5);
                            }
                            catch (WebDriverTimeoutException)
                            {
                                // No error message, confirm the deletion
                                
                                Thread.Sleep(3000);
                                break;
                            }
                            if (isErrorMessageDisplayed)
                            {
                                HandleErrorScenario();
                                
                                break;
                            }
                            
                        }
                    }

                        if (!isShareSkillFound)
                        {
                            break;
                        }
                    }
                    catch (NoSuchElementException)
                    {
                        // No more delete buttons found, break the loop
                        break;
                    }
                    catch (WebDriverTimeoutException)
                    {
                        // Delete button not found within wait time, break the loop
                        break;
                    }

                }
            }

        public void CleanlistingData()
        {
            IWebElement signOutButton = driver.FindElement(By.XPath("//button[@class='ui green basic button']"));
            signOutButton.Click();
            loginPageObj.Loginsteps2();
            homepageObj.clickmanagelisting();

            while (true)
            {
                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));

                    WaitUtils.WaitToBeVisible(driver, "XPath", e_buttonDelete, 10);
                    // Find the delete button for the last record
                    buttonDelete.Click();
                    wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/div/div[3]/button[2]")));
                    Selectyesoption.Click();
                    bool isErrorMessageDisplayed = false;
                    try
                    {
                        isErrorMessageDisplayed = true;
                        WaitUtils.WaitToBeVisible(driver, "XPath", e_errormessage, 5);
                    }
                    catch (WebDriverTimeoutException)
                    {
                        // No error message, confirm the deletion

                        Thread.Sleep(3000);
                        break;
                    }
                    if (isErrorMessageDisplayed)
                    {
                        HandleErrorScenario();

                        break;
                    }
                }
                catch (NoSuchElementException)
                {
                    // Break the loop if no more delete buttons are found
                    break;
                }
                catch (WebDriverTimeoutException)
                {
                    // Break the loop if the delete button is not found within the wait time
                    break;
                }
            }
        }


        public void ClickAnyTab(string tab)
        {
            //Wait for tabs to be visible
            WaitUtils.WaitToBeVisible(driver, "XPath", e_waitForTab, 3);

            //Click on specified tab
            tabOption.Click();
        }


        private void HandleErrorScenario()
        {
            try
            {
                // Sign out the current user and log in as the second user
            commonDriver.SignOut();
            loginPageObj.Loginsteps();

            // Navigate to the 'Sent Requests' page and withdraw the request
            homepageObj.clickSentrequest();

            try
            {
                // Attempt to withdraw the request
                WaitUtils.WaitToBeVisible(driver, e_withdraw, 5); // Assuming e_withdraw is the XPath for the withdraw button
                var withdrawButton = driver.FindElement(By.XPath(e_withdraw)); // Adjust XPath as necessary
                if (withdrawButton.Displayed)
                {
                    manageRequestsObj.withdrawrequest(); // Assuming this method handles the withdrawal
                    Console.WriteLine("Request successfully withdrawn.");
                }
                else
                {
                    Console.WriteLine("No withdrawable request found.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Withdraw button not found. No request to withdraw.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during withdrawal: {ex.Message}");
            }

            // Sign out the second user
                commonDriver.SignOut();
            }
            catch (Exception ex)
            {
                   Console.WriteLine($"Error during sign-out or secondary login steps: {ex.Message}");
            }
             
    
            try
            {
        
                  loginPageObj.Loginsteps2();
                  homepageObj.clickmanagelisting();

        
                  WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                  WaitUtils.WaitToBeVisible(driver, "XPath", e_buttonDelete, 10); // Assuming e_buttonDelete is the delete button XPath

        
                  buttonDelete.Click();

    
                  wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/div/div[3]/button[2]")));
                  Selectyesoption.Click();

       
                  bool isErrorMessageDisplayed = false;
            try
            {
                  WaitUtils.WaitToBeVisible(driver, "XPath", e_errormessage, 5); // Assuming e_errormessage is the XPath for the error message
                  isErrorMessageDisplayed = true;
            }
            catch (WebDriverTimeoutException)
            {
                  // No error message, continue with deletion
                  Thread.Sleep(3000); // Allow for deletion to complete
                  return; // Deletion successful, exit the method
            }

                  // If an error message is displayed, handle it
            if (isErrorMessageDisplayed)
            {
                  Console.WriteLine("Error message displayed. Handling the error.");
                  HandleErrorScenario2(); // Assuming HandleErrorScenario2 is the method that handles the error scenario
            }
        }
            catch (Exception ex)
            {
                      Console.WriteLine($"Error during deletion steps: {ex.Message}");
            }
        }

        private void HandleErrorScenario2()
        {
            commonDriver.SignOut();
            // Log in as the second user
            loginPageObj.Loginsteps2();

            // Navigate to manage requests
            homepageObj.clickRecievedrequest();

            // Withdraw the request
            manageRequestsObj.Completerequestofreciver();
           
            homepageObj.clickmanagelisting();
            // Perform the delete operation again
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            WaitUtils.WaitToBeVisible(driver, "XPath", e_buttonDelete, 10);
            // Find the delete button for the last record
            buttonDelete.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/div/div[3]/button[2]")));
            Selectyesoption.Click();
            Thread.Sleep(6000);
            bool isErrorMessageDisplayed = false;
            try
            {
                WaitUtils.WaitToBeVisible(driver, "XPath", e_errormessage, 5); // Assuming e_errormessage is the XPath for the error message
                isErrorMessageDisplayed = true;
            }
            catch (WebDriverTimeoutException)
            {
                // No error message, continue with deletion
                Thread.Sleep(3000); // Allow for deletion to complete
                return; // Deletion successful, exit the method
            }

            // If an error message is displayed, handle it
            if (isErrorMessageDisplayed)
            {
                Console.WriteLine("Error message displayed. Handling the error.");
                HandleErrorScenario3(); // Assuming HandleErrorScenario2 is the method that handles the error scenario
            }
        }
        private void HandleErrorScenario3()
        {
            commonDriver.SignOut();
            loginPageObj.Loginsteps();
            homepageObj.clickSentrequest();
            manageRequestsObj.Completed();
            commonDriver.SignOut();

            // Log in as the second user
            loginPageObj.Loginsteps2();

            // Navigate to manage requests
            homepageObj.clickRecievedrequest(); 
            try
            {
                // Attempt to withdraw the request
                WaitUtils.WaitToBeVisible(driver, e_completerequest, 5); // Assuming e_withdraw is the XPath for the withdraw button
                var withdrawButton = driver.FindElement(By.XPath(e_completerequest)); // Adjust XPath as necessary
                if (withdrawButton.Displayed)
                {
                    manageRequestsObj.Completerequestofreciver(); // Assuming this method handles the withdrawal
                    Console.WriteLine("Request successfully withdrawn.");
                }
                else
                {
                    Console.WriteLine("No withdrawable request found.");
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Withdraw button not found. No request to withdraw.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred during withdrawal: {ex.Message}");
            }

      
            homepageObj.clickmanagelisting();
            // Perform the delete operation again
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            WaitUtils.WaitToBeVisible(driver, "XPath", e_buttonDelete, 10);
            // Find the delete button for the last record
            buttonDelete.Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[2]/div/div[3]/button[2]")));
            Selectyesoption.Click();
            Thread.Sleep(6000);
       }


        private void WaitForPopupToDisappear()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//div[@class='ns-box-inner']")));
            }
            catch (WebDriverTimeoutException)
            {
                // Handle the case where the popup does not disappear within the timeout
                Assert.Fail("The popup did not disappear within the expected time.");
            }
        }
    }
}
