using MarsAdvancedTaskPart2.Utils;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsAdvancedTaskPart2.Pages
{
    public class LoginPage : CommonDriver
    {
        private IWebElement signinButton => driver.FindElement(By.XPath("//a[text()='Sign In']"));
        private IWebElement Username => driver.FindElement(By.XPath("//input[@name='email']"));
        private IWebElement Password => driver.FindElement(By.XPath("//input[@name='password']"));
        private IWebElement loginButton => driver.FindElement(By.XPath("//button[text()='Login']"));

        private string e_signin = "//a[text()='Sign In']";

        public void LoginAction(IWebDriver driver, string email, string password)
        {
            WaitUtils.WaitToBeVisible(driver, "XPath", "//a[text()='Sign In']", 100);
            signinButton.Click();
            Username.SendKeys(email);
            Password.SendKeys(password);
            loginButton.Click();

        }
        public void Loginsteps()
        {
            string loginFile = @"D:\MarsAdvancedTaskPart2\TestData\login.json";
            List<TestModel.TestCaseData> testCases = JsonUtils.ReadJsonData<TestModel.TestCaseData>(loginFile);


            foreach (var testCase in testCases)
            {
                if (testCase.TestCase == "LoginData")
                {
                    var loginData = testCase.Data;
                    string email = loginData.Email;
                    string password = loginData.Password;
                    LoginAction(driver, email, password);

                }
            }
        }
        public void Loginsteps2()
        {
            string loginFile = @"D:\MarsAdvancedTaskPart2\TestData\login2.json";
            List<TestModel.TestCaseData> testCases = JsonUtils.ReadJsonData<TestModel.TestCaseData>(loginFile);


            foreach (var testCase in testCases)
            {
                if (testCase.TestCase == "LoginData")
                {
                    var loginData = testCase.Data;
                    string email = loginData.Email;
                    string password = loginData.Password;
                    LoginAction(driver, email, password);

                }
            }
        }
    }
}
