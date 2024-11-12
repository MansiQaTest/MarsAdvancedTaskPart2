using MarsAdvancedTaskPart2.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages.Components.Profilepage
{
    public class ProfileMenuTab : CommonDriver
    {
        private IWebElement EducationTab => driver.FindElement(By.XPath("//a[text()='Education']"));
        private IWebElement CertificationsTab => driver.FindElement(By.XPath("//a[text()='Certifications']"));

        private string e_educationtab = "//a[text()='Education']";
        private string e_certificationstab = "//a[text()='Certifications']";

        public void ClickEducationTab()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_educationtab, 10);
            EducationTab.Click();
        }
        public void ClickCertificationsTab()
        {
            WaitUtils.WaitToBeClickable(driver, "XPath", e_certificationstab, 10);
            CertificationsTab.Click();
        }
    }
}
