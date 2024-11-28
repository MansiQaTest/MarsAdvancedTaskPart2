using MarsAdvancedTaskPart2.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages
{
    public class Homepage : CommonDriver
    {
        private IWebElement profileTab => driver.FindElement(By.XPath("//a[text()='Profile']"));
        private IWebElement ProfileDropdown => driver.FindElement(By.XPath("//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span/div/a[1]"));

        private IWebElement managelisting => driver.FindElement(By.XPath("//a[text()='Manage Listings']"));
        private IWebElement managerequest => driver.FindElement(By.XPath("//div[text()='Manage Requests']"));
        private IWebElement ReceivedRequests => driver.FindElement(By.XPath("//a[text()='Received Requests']"));
        private IWebElement SentRequests => driver.FindElement(By.XPath("//a[text()='Sent Requests']"));

        private string e_profiledropdown = "//*[@id=\"account-profile-section\"]/div/div[1]/div[2]/div/span/div/a[1]";
        private string e_profileTab = "//a[text()='Profile']";
        private string e_managelisting = "//a[text()='Manage Listings']";
        private string e_managerequest = "//div[text()='Manage Requests']";
        private string e_sentrequest = "//a[text()='Sent Requests']";
        private string e_receivedrequest = "//a[text()='Received Requests']";
        
       
        public void clickprofiletab()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_profileTab, 10);
            profileTab.Click();
        }

        public void clickmanagelisting()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_managelisting, 10);
            managelisting.Click();

        }
        public void clickSentrequest()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_managerequest, 10);
            managerequest.Click();
            WaitUtils.WaitToBeClickable(driver, "XPath", e_sentrequest, 10);
            SentRequests.Click();

        }

        public void clickRecievedrequest()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_managerequest, 10);
            managerequest.Click();
            WaitUtils.WaitToBeClickable(driver, "XPath", e_receivedrequest, 10);
            ReceivedRequests.Click();

        }



    }
}
