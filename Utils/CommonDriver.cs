using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using System;
using System.IO;
using NUnit.Framework;
using MarsAdvancedTaskPart2.Pages;
using MarsAdvancedTaskPart2.Pages.Components.Profilepage;
using MarsAdvancedTaskPart2.Pages.Components;
using MarsAdvancedTaskPart2.TestModel;

namespace MarsAdvancedTaskPart2.Utils
{

    public class CommonDriver
    {
        CommonDriver commonDriverObj;
        LoginPage loginPageObj;
        ProfileTabEducation educationObj;
        ProfileTabCertifications certificateObj;
        ManageListings manageListingsObj;

        public static IWebDriver driver;
        public static ExtentReports extent;
        public static ExtentTest test;
        public static List<string> EducationDataToCleanUp { get; set; } = new List<string>();
        public static List<string> CertificateDataToCleanUp { get; set; } = new List<string>();
        public static List<string> ManageListingToCleanUp { get; set; } = new List<string>();



        [BeforeScenario]
        public void BeforeScenario()
        {
            var sparkReporter = new ExtentSparkReporter(@"D:\MarsAdvancedTaskPart2\Reports\extentReport.html");
            extent = new ExtentReports();
            extent.AttachReporter(sparkReporter);

            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:5000/Home");

            loginPageObj = new LoginPage();
            educationObj = new ProfileTabEducation();
            certificateObj = new ProfileTabCertifications();
            manageListingsObj = new ManageListings();

            // Create test entry in the report
            var testName = TestContext.CurrentContext.Test.Name;
            test = extent.CreateTest(testName);
           
        }

        public static void TakeScreenshotWithPngFormat()
        {
            try
            {
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(@"D:\MarsAdvancedTaskPart2\ScreenShot\Screenshot." + System.Drawing.Imaging.ImageFormat.Png);


                // Add screenshot to ExtentReports

            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred while taking screenshot: {e.Message}");
            }
        }

        [AfterScenario]
        public void AfterScenario()
        {
            try
            {
                // Take a screenshot on failure
                if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
                {
                    TakeScreenshotWithPngFormat();
                }

                // Cleanup the Certificate test data
                foreach (var certificatename in CertificateDataToCleanUp)
                {
                    try
                    {
                        certificateObj.DeleteTestData(certificatename);
                        test.Log(Status.Info, $"Deleted certificate name '{certificatename}' from the UI.");
                    }
                    catch (Exception cleanupEx)
                    {
                        test.Log(Status.Fail, $"Failed to delete certificate name during cleanup: {cleanupEx.Message}");
                    }
                }
                foreach (var title in ManageListingToCleanUp)
                {
                    try
                    {
                        ManageListingsModel DeleteShareSkill = new ManageListingsModel
                        {
                            Title = title // Assign the current title from the loop
                        };

                        manageListingsObj.DeleteManageListing(DeleteShareSkill);

                        test.Log(Status.Info, $"Deleted listing with title '{DeleteShareSkill.Title}' from the UI.");

                    }
                    catch (Exception cleanupEx)
                    {
                        test.Log(Status.Fail, $"Failed to delete certificate name during cleanup: {cleanupEx.Message}");
                    }
                }

                // Cleanup the Education test data
                foreach (var degree in EducationDataToCleanUp)
                {
                    try
                    {
                        educationObj.DeleteTestData(degree);
                        test.Log(Status.Info, $"Deleted degree '{degree}' from the UI.");
                    }
                    catch (Exception cleanupEx)
                    {
                        test.Log(Status.Fail, $"Failed to delete degree during cleanup: {cleanupEx.Message}");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occurred during cleanup: {e.Message}");
            }
            finally
            {
                // Ensure driver is properly quit and disposed
                if (driver != null)
                {
                    driver.Quit();
                    driver.Dispose();
                }

                // Finalize the report
                extent.Flush();
            }
        }
    }
    
}
