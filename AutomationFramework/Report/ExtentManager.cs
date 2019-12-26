using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Report
{
    public class ExtentManager
    {
        private static ExtentReports _extent;
        private static ExtentTest _test;
        private static ExtentHtmlReporter _htmlReporter;
        private static string filePath = "./extentreport.html";
        public static string reportPath;
        public static ExtentReports GetExtent()
        {
            if (_extent != null)
                return _extent; // avoid creating new instance of html file
            _extent = new ExtentReports();
            _extent.AttachReporter(GetHtmlReporter());
            _extent.AddSystemInfo("OS", Environment.OSVersion.ToString());
            _extent.AddSystemInfo("Host Name", Environment.MachineName);
            _extent.AddSystemInfo("User Name", Environment.UserName);

            // extent.AttachReporter(GetHtmlReporter(), GetExtentXReporter());
            return _extent;
        }

        private static ExtentHtmlReporter GetHtmlReporter()
        {
            //To obtain the current solution path/project path

            string pth = System.Reflection.Assembly.GetCallingAssembly().CodeBase;

            string actualPath = pth.Substring(0, pth.LastIndexOf("bin"));

            string projectPath = new Uri(actualPath).LocalPath;



            //Append the html report file to current project path

             reportPath = projectPath + "Reports\\TestRunReport"+DateTime.Now.ToString("MM-dd-yyyy HH-mm-ss") +".html";

            _htmlReporter = new ExtentHtmlReporter(reportPath);
            // make the charts visible on report open
            _htmlReporter.Config.DocumentTitle = "Regression report";
            _htmlReporter.Config.ReportName = "API Test Automation Report";
            _htmlReporter.Config.EnableTimeline = true;
            _htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Standard;

            return _htmlReporter;
        }

        public static ExtentTest CreateTest(string name, string description)
        {
            _test = _extent.CreateTest(name, description);
            return _test;
        }
    }
}
