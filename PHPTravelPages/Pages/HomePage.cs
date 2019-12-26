using AutomationFramework.Browser;
using AutomationFramework.Extensions;
using AutomationFramework.WebElementParser;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHPTravelPages.Pages
{
    public class HomePage
    {
        private readonly ElementParser _parser = new ElementParser("HomePageRepo.json");
        private readonly ExtentTest _test;

        public HomePage(ExtentTest test)
        {
            _test = test;
        }
        public void OpenHomePage()
        {
            Driver.WebDriver.Navigate().GoToUrl("https://www.phptravels.net/");
        }
        public void OpenRegistrationPage()
        {
            Driver.WebDriver.Navigate().GoToUrl("https://www.phptravels.net/register");
        }
        public string Get_LoggedIn_UserName()
        {

            var username_locator = _parser.GetElementByName("UserName");
            Driver.WebDriver.ScrollToElement(username_locator, 10);
            IWebElement usernametxtbox = Driver.WebDriver.InspectElement(username_locator, _test);
            return usernametxtbox.Text.Trim();


        }
    }
}
