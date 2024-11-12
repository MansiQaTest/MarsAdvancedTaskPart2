using MarsAdvancedTaskPart2.Utils;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages.Components
{
    public class ManageRequests : CommonDriver
    {
        Homepage homepageObj;
       
        private IWebElement Withdraw => driver.FindElement(By.XPath("//button[contains(@class, 'ui negative basic button') and text()='Withdraw']"));
        private IWebElement Complete => driver.FindElement(By.XPath("//button[@class='ui positive basic button' and text()='Completed']"));
        private IWebElement Accept => driver.FindElement(By.XPath("//button[@type='button' and contains(@class, 'ui primary basic button') and text()='Accept']"));
        private IWebElement Decline => driver.FindElement(By.XPath("//button[@type='button' and contains(@class, 'ui negative basic button') and text()='Decline']"));
        private IWebElement Review => driver.FindElement(By.XPath("//button[@class='ui positive basic button' and text()='Review']"));
        private IWebElement Reviewselect => driver.FindElement(By.XPath("//*[@id=\"communicationRating\"]/i[5]"));
        private IWebElement Save => driver.FindElement(By.XPath("/html/body/div[2]/div/div[2]/div/div[4]/div"));
        private IWebElement message => driver.FindElement(By.XPath(e_message));

        private string e_withdraw = "//button[contains(@class, 'ui negative basic button') and text()='Withdraw']";
        private string e_complete = "//button[@class='ui positive basic button' and text()='Completed']\r\n";
        private string e_accept = "//button[@type='button' and contains(@class, 'ui primary basic button') and text()='Accept']";
        private string e_decline = "//button[@type='button' and contains(@class, 'ui negative basic button') and text()='Decline']";
        private string e_review = "//button[@class='ui positive basic button' and text()='Review']";
        private string e_reviewselect = "//*[@id=\"communicationRating\"]/i[5]";
        private string e_save = "/html/body/div[2]/div/div[2]/div/div[4]/div";
        private string e_message = "//div[@class='ns-box-inner']";
        public ManageRequests()
        {
            homepageObj = new Homepage();
        }

        public void withdrawrequest()
        {
            homepageObj.clickSentrequest();

           WaitUtils.WaitToBeClickable(driver, "XPath", e_withdraw , 10);
           Withdraw.Click();

        }
        public string GetMessage()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_message, 10);
            return message.Text;
        }
        public void declinerequest() 
        {
            homepageObj.clickRecievedrequest();

            WaitUtils.WaitToBeClickable(driver, "XPath", e_decline , 10);
            Decline.Click();


        }
        public void acceptrequest() 
        {
            homepageObj.clickRecievedrequest();

            WaitUtils.WaitToBeClickable(driver, "XPath", e_accept, 10);
            Accept.Click();
        }
        public void Completerequest() 
        {
            homepageObj.clickSentrequest();

            WaitUtils.WaitToBeClickable(driver, "XPath", e_complete, 10);
            Complete.Click();

        }

        public void reviewrequest() 
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_review , 10);
            Review.Click();

            WaitUtils.WaitToBeClickable(driver, "XPath", e_reviewselect, 10);
            Reviewselect.Click();

            WaitUtils.WaitToBeClickable(driver, "XPath", e_save, 10);
            Save.Click();

        }
    }
}
