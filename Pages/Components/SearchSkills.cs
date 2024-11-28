using MarsAdvancedTaskPart2.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages.Components
{
    public class SearchSkills : CommonDriver
    {
        private IWebElement searchtext => driver.FindElement(By.XPath("//div[@class='ui small icon input search-box']/input[@type='text']"));
        private IWebElement searchclick => driver.FindElement(By.XPath("//div[@class='ui small icon input search-box']/i[@class='search link icon']"));
        private IWebElement ClickLastPage => driver.FindElement(By.XPath("(//button[contains(@class, 'ui button otherPage')])[last()-1]"));
        private IWebElement Clicklastdata => driver.FindElement(By.XPath(" //*[@id=\"service-search-section\"]/div[2]/div/section/div/div[2]/div/div[2]/div/div/div[last()]/div[1]/a[2]/p"));
        private IWebElement ClickSkill => driver.FindElement(By.XPath("//a[@class='service-info']/p[text()='QA']"));
        private IWebElement Writemessage => driver.FindElement(By.XPath("//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[1]/textarea"));
        private IWebElement SendRequestbutton => driver.FindElement(By.XPath(" //*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[3]"));
        private IWebElement SelectYes => driver.FindElement(By.XPath("/html/body/div[4]/div/div[3]/button[1]"));
        // Locator strings for wait conditions
        private string e_searchtext = "//div[@class='ui small icon input search-box']/input[@type='text']";
        private string e_searchclick = "//div[@class='ui small icon input search-box']/i[@class='search link icon']";
        private string e_clicklastpage = "(//button[contains(@class, 'ui button otherPage')])[last()-1]";
        private string e_clicklastdata = "//*[@id=\"service-search-section\"]/div[2]/div/section/div/div[2]/div/div[2]/div/div/div[last()]/div[1]/a[2]/p";
        private string e_clickskill = "//a[@class='service-info']/p[text()='QA']";
        private string e_writemessage = "//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[1]/textarea";
        private string e_sendbutton = "//*[@id=\"service-detail-section\"]/div[2]/div/div[2]/div[2]/div[2]/div/div[2]/div/div[3]";
        private string e_SelectYes = "/html/body/div[4]/div/div[3]/button[1]";
        public void SearchSkill(string searchString)
        {
            
            // Wait for the search box to be visible
            WaitUtils.WaitToBeVisible(driver, "XPath", e_searchtext, 10);

            // Enter the search text and click the search button
            searchtext.SendKeys(searchString);
            searchclick.Click();
            Thread.Sleep(2000);


            WaitUtils.WaitToBeVisible(driver, "XPath", e_clicklastpage, 30);
            ClickLastPage.Click();
            Thread.Sleep(2000);

            WaitUtils.WaitToBeVisible(driver, "XPath", e_clicklastdata, 20);
            WaitUtils.WaitToBeClickable(driver, "XPath", e_clicklastdata, 20);
            Clicklastdata.Click();           


        }

        public void SendRequest()
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", e_writemessage, 10);
            Writemessage.SendKeys("Test");

            WaitUtils.WaitToBeVisible(driver, "XPath", e_sendbutton, 10);
            SendRequestbutton.Click();

            WaitUtils.WaitToBeVisible(driver, "XPath", e_SelectYes, 10);
            SelectYes.Click();

        }

    }
}
