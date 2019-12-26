using AutomationFramework.Browser;
using AutomationFramework.Report;
using AventStack.ExtentReports;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHPTravelTests
{
    public class BaseTest
    {
        public static string Url;


        //public static ExtentHtmlReporter htmlReporter;

        protected ExtentTest _test;
        public const int _retrynum = 3;

        protected ExtentReports Extent;
        public static string browsertype;

        [SetUp]
        public void IntializeTest()
        {
            Driver.GetBrowser(Driver.Chrome, Url);
        }
        [TearDown]
        public void TearDownTest()
        {
            //StackTrace details for failed Testcases

            var status = TestContext.CurrentContext.Result.Outcome.Status;

            var stackTrace = "" + TestContext.CurrentContext.Result.StackTrace + "";
            var errorMessage = TestContext.CurrentContext.Result.Message;



            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)

            {

                _test.Log(Status.Fail, status + errorMessage);


                string screenShotPath = Capture(Driver.WebDriver, TestContext.CurrentContext.Test.Name + DateTime.Now.Hour + DateTime.Now.Second);
                //  _test.Log(Status.Fail, stackTrace + errorMessage);
                _test.Log(Status.Fail, "Snapshot below: " + _test.AddScreenCaptureFromPath(screenShotPath));


            }


            //End test report

            //extent.EndTest(test);

            browsertype = Driver.WebDriver.GetType().ToString();
            Driver.CloseBrowser();
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTestsInEntireAssembly()
        {
            Extent = ExtentManager.GetExtent();
            Url = ConfigurationManager.AppSettings["ApplicationURL"];

        }
        [OneTimeTearDown]
        public void RunAfterAnyTestsInInEntireAssembly()
        {
            Extent.AddSystemInfo("Browser", BaseTest.browsertype);
            Extent.AddSystemInfo("Environment URL", Url);
            Extent.Flush();
            //EmailService.EmailServiceClient e = new EmailService.EmailServiceClient("BasicHttpBinding_IEmailService1");
            ////var attachment = new System.Net.Mail.Attachment("your attachment file");
            //byte[] attachment = System.IO.File.ReadAllBytes(ExtentManager.reportPath);

            //string[] emailTo = new string[] { "aamrashad@moj.gov.sa", "hyousef@moj.gov.sa", "mabdallah@moj.gov.sa", "mabuZaid@moj.gov.sa", "msabra@moj.gov.sa", "msenousy@moj.gov.sa" };
            //string[] ccTo = new string[] { "imostafa@moj.gov.sa" };
            //e.SendEmailWithAttachement(emailTo, ccTo, "Test Automation Report " + Url, "Test Automation Report", true, "Test Automation Report", attachment);
        }



        public static string Capture(IWebDriver driver, string screenShotName)
        {
            ITakesScreenshot ts = (ITakesScreenshot)driver;
            Screenshot screenshot = ts.GetScreenshot();
            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string finalpth = pth.Substring(0, pth.LastIndexOf("bin")) + "ErrorScreenshots\\" + screenShotName + ".png";
            string localpath = new Uri(finalpth).LocalPath;
            screenshot.SaveAsFile(localpath, ScreenshotImageFormat.Png);
            return localpath;
        }

    }
}
