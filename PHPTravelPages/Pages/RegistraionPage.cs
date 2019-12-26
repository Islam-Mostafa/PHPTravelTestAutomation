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
   public class RegistraionPage
    {
        private readonly ElementParser _parser = new ElementParser("RegistraionPageRepo.json");
        private readonly ExtentTest _test;

        public RegistraionPage(ExtentTest test)
        {
            _test = test;
        }
      
        public void Enter_FirstName(string firstname)
        {
            var username_locator = _parser.GetElementByName("FirstName");
            //Driver.WebDriver.ScrollToElement(username_locator, 10);
            IWebElement usernametxtbox = Driver.WebDriver.InspectElement(username_locator, _test);
            usernametxtbox.SendKeys(firstname);
        }
        public void Enter_LastName(string lastname)
        {
            var username_locator = _parser.GetElementByName("LastName");
            //Driver.WebDriver.ScrollToElement(username_locator, 10);
            IWebElement usernametxtbox = Driver.WebDriver.InspectElement(username_locator, _test);
            usernametxtbox.SendKeys(lastname);
        }
        public void Enter_MobileNumber(string number)
        {
            var username_locator = _parser.GetElementByName("MobileNumber");
            //Driver.WebDriver.ScrollToElement(username_locator, 10);
            IWebElement usernametxtbox = Driver.WebDriver.InspectElement(username_locator, _test);
            usernametxtbox.SendKeys(number);
        }
        public void Enter_Email(string email)
        {
            var username_locator = _parser.GetElementByName("Email");
            //Driver.WebDriver.ScrollToElement(username_locator, 10);
            IWebElement usernametxtbox = Driver.WebDriver.InspectElement(username_locator, _test);
            usernametxtbox.SendKeys(email);
        }
        public void Enter_Password(string password)
        {
            var username_locator = _parser.GetElementByName("Password");
            //Driver.WebDriver.ScrollToElement(username_locator, 10);
            IWebElement usernametxtbox = Driver.WebDriver.InspectElement(username_locator, _test);
            usernametxtbox.SendKeys(password);
        }
        public void Enter_ConfirmPassword(string confirm_password)
        {
            var username_locator = _parser.GetElementByName("ConfirmPassword");
         //   Driver.WebDriver.ScrollToElement(username_locator, 10);
            IWebElement usernametxtbox = Driver.WebDriver.InspectElement(username_locator, _test);
            usernametxtbox.SendKeys(confirm_password);
        }
        public void Click_SignUp()
        {
            var btn_locator = _parser.GetElementByName("SignUpBtn");
      //      Driver.WebDriver.ScrollToElement(btn_locator, 10);
            IWebElement btn = Driver.WebDriver.InspectElement(btn_locator, _test);
            btn.Click();
        }
    }
}
