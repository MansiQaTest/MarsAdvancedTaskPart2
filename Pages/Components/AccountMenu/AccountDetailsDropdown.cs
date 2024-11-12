using MarsAdvancedTaskPart2.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages.Components.AccountMenu
{
    public class AccountDetailsDropdown : CommonDriver
    {
        private IWebElement SelectAcountNameDropdown => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span"));
        private IWebElement ChangePassword => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span/div/a[2]"));

        private string e_selectacountnamedropdown = "//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span";
        private string e_changepassword = "//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span/div/a[2]";

        public void ClickChangePassword()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_selectacountnamedropdown, 10);
            SelectAcountNameDropdown.Click();

            WaitUtils.WaitToBeClickable(driver, "XPath", e_changepassword, 10);
            ChangePassword.Click();
        }
    }
}
