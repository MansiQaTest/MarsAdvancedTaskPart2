using MarsAdvancedTaskPart2.Utils;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages.Components.AccountMenu
{
    public class ChangePassword : CommonDriver
    {
        private IWebElement CurrentPassword => driver.FindElement(By.XPath("//input[@placeholder='Current Password']"));
        private IWebElement NewPassword => driver.FindElement(By.XPath("//input[@placeholder='New Password']"));
        private IWebElement ConfirmPassword => driver.FindElement(By.XPath("//input[@placeholder='Confirm Password']"));
        private IWebElement Savebutton => driver.FindElement(By.XPath("/html/body/div[4]/div/div[2]/form/div[4]/button"));
        private IWebElement message => driver.FindElement(By.XPath(e_message));
        private IWebElement cancel => driver.FindElement(By.XPath("/html/body/div[4]/div"));

        private string e_currentpassword = "//input[@placeholder='Current Password']";
        private string e_newpassword = "//input[@placeholder='New Password']";
        private string e_confirmpassword = "//input[@placeholder='Confirm Password']";
        private string e_save = "/html/body/div[4]/div/div[2]/form/div[4]/button";
        private string e_message = "//div[@class='ns-box-inner']";
        private string e_cancel = "/html/body/div[4]/div";


        public void ChangethePassword(string currentpassword, string newpassword, string confirmpassword)
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_currentpassword, 10);
            CurrentPassword.SendKeys(currentpassword);

            WaitUtils.WaitToBeVisible(driver, "XPath", e_newpassword, 10);
            NewPassword.SendKeys(newpassword);

            WaitUtils.WaitToBeVisible(driver, "XPath", e_confirmpassword, 10);
            ConfirmPassword.SendKeys(confirmpassword);

            WaitUtils.WaitToBeClickable(driver, "XPath", e_save, 20);
            Savebutton.Click();
        }       
        public string GetMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_message, 10);
            return message.Text;
        }
    }
}
