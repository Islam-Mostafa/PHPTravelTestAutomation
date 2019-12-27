using AutomationFramework.ExcelParser;
using AventStack.ExtentReports;
using NUnit.Framework;
using PHPTravelPages.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHPTravelTests.Tests
{

    class RegistrationPageTests : BaseTest
    {
        string methodname, classname, firstname, lastname, mobilenumber, email, password;
        [SetUp]
        public void Intialize()
        {
            methodname = TestContext.CurrentContext.Test.Name;
            classname = this.GetType().Name;

            firstname = TestCasesParser.GetValueOf("FirstName", methodname, classname);
            lastname = TestCasesParser.GetValueOf("LastName", methodname, classname);
            mobilenumber = TestCasesParser.GetValueOf("MobileNumber", methodname, classname);
            email = TestCasesParser.GetValueOf("Email", methodname, classname);
            password = TestCasesParser.GetValueOf("Password", methodname, classname);

        }
        [Test]
        public void Register_NewUser()
        {
            string randomnumber = DateTime.Now.ToString("yyyymmddHHMMss");

            email = email.Split('@')[0] + randomnumber+"@" + email.Split('@')[1];
            _test = Extent.CreateTest(TestContext.CurrentContext.Test.Name, " ");
            _test.Log(Status.Info, "PHPTravel has been opened");
            HomePage homePage = new HomePage(_test);
            homePage.OpenRegistrationPage();
            RegistraionPage registraionPage = new RegistraionPage(_test);

            registraionPage.Enter_FirstName(firstname);
            registraionPage.Enter_LastName(lastname);
            registraionPage.Enter_MobileNumber(mobilenumber);
            registraionPage.Enter_Email(email);
            registraionPage.Enter_Password(password);
            registraionPage.Enter_ConfirmPassword(password);
            registraionPage.Click_SignUp();

            homePage.OpenHomePage();
            Assert.AreEqual(firstname.ToLower(), homePage.Get_LoggedIn_UserName().ToLower());
        }

        [Test]
        public void add_TwoUsers_With_Same_Email()
        {
            string randomnumber = DateTime.Now.ToString("yyyymmddHHMMss");

            email = email.Split('@')[0] + randomnumber + "@" + email.Split('@')[1];
            _test = Extent.CreateTest(TestContext.CurrentContext.Test.Name, " ");
            _test.Log(Status.Info, "PHPTravel has been opened");
            HomePage homePage = new HomePage(_test);
            homePage.OpenRegistrationPage();
            RegistraionPage registraionPage = new RegistraionPage(_test);

            registraionPage.Enter_FirstName(firstname);
            registraionPage.Enter_LastName(lastname);
            registraionPage.Enter_MobileNumber(mobilenumber);
            registraionPage.Enter_Email(email);
            registraionPage.Enter_Password(password);
            registraionPage.Enter_ConfirmPassword(password);
            registraionPage.Click_SignUp();

            homePage.OpenHomePage();
            Assert.AreEqual(firstname.ToLower(), homePage.Get_LoggedIn_UserName().ToLower());

            homePage.Logout();
            homePage.OpenRegistrationPage();

            registraionPage.Enter_FirstName(firstname);
            registraionPage.Enter_LastName(lastname);
            registraionPage.Enter_MobileNumber(mobilenumber);
            registraionPage.Enter_Email(email);
            registraionPage.Enter_Password(password);
            registraionPage.Enter_ConfirmPassword(password);
            registraionPage.Click_SignUp();

            Assert.AreEqual(registraionPage.Get_ValidationMessage(), "Email Already Exists.");

        }
    }
}
