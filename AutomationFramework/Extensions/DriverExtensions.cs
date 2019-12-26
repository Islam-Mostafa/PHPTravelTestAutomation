using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Extensions
{
    public static class DriverExtensions
    {
        public static bool WaitForJStoLoad(this IWebDriver driver)
        {
            var isDocumentReady = ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
            return isDocumentReady;
        }
        public static void SwitchToDefaultContent(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        public static void WaitAndSwitchToFrame(this IWebDriver driver, int timeOutInSecond, By frameLocator)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSecond));
            WaitForJStoLoad(driver);
            wait.Until(ExpectedConditions.FrameToBeAvailableAndSwitchToIt((frameLocator)));
        }

        public static string ExecuteScriptUsingJs(this IWebDriver driver, string script)
        {
            var x = ((IJavaScriptExecutor)driver).ExecuteScript(script);
            if (x != null)
                return x.ToString();
            return null;
        }
        public static void WaitForAjax(this IWebDriver driver, int timeOutInSecond)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSecond));
            wait.Until(d =>
            {
                var javaScriptExecutor = d as IJavaScriptExecutor;
                return javaScriptExecutor != null && (bool)javaScriptExecutor.ExecuteScript("return jQuery.active == 0");
            });
        }
        public static IWebElement InspectElement(this IWebDriver driver, By elementLocator, ExtentTest test)
        {

            ElementExtensions.WaitForItToBeVisible(elementLocator, 60);
            //ElementExtensions.WaitForItToBeClickable(elementLocator, 60);

            var element = driver.FindElement(elementLocator);
            test.Log(Status.Info, elementLocator + " has been inspected");
            return element;
        }
        public static IWebElement InspectElement(this IWebDriver driver, By elementLocator, int timeOutInSeconds, ExtentTest test)
        {

            ElementExtensions.WaitForItToBeVisible(elementLocator, timeOutInSeconds);
            //ElementExtensions.WaitForItToBeClickable(elementLocator, 60);

            var element = driver.FindElement(elementLocator);
            test.Log(Status.Info, elementLocator + " has been inspected");
            return element;
        }
        public static void ScrollHorizontalLeft(this IWebDriver driver, By element)
        {
            var javaScriptExecutor = driver as IJavaScriptExecutor;
            // javaScriptExecutor.ExecuteScript("window.scrollBy(-3000,0)");
            var e = driver.FindElement(element);
            javaScriptExecutor?.ExecuteScript("window.scrollLeft -=500", e);
        }
        public static void ScrollHorizontalRight(this IWebDriver driver, By element)
        {
            var javaScriptExecutor = driver as IJavaScriptExecutor;
            // javaScriptExecutor.ExecuteScript("window.scrollBy(-3000,0)");
            driver.FindElement(element);
            javaScriptExecutor?.ExecuteScript("$('div.demo' ).scrollLeft( 300 );");
        }
        public static ReadOnlyCollection<IWebElement> InspectElements(this IWebDriver driver, By elementLocator, ExtentTest test)
        {
            var elements = driver.FindElements(elementLocator);
            test.Log(Status.Info, elementLocator + " has been inspected");
            return elements;
        }
        public static void CloseBrowser(this IWebDriver driver)
        {
            driver.Quit();
            driver = null;
        }

        public static void CloseCurrentTab(this IWebDriver driver)
        {
            driver.Close();
        }
        public static IWebElement ScrollToElement(this IWebDriver driver, By elementLocator, int timeOutInSeconds)
        {

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds));
            var jse = (IJavaScriptExecutor)driver;
        
            // presence in DOM
            wait.Until(ExpectedConditions.ElementExists(elementLocator)); //FIXME: was: presenceOfElementLocated
            wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(elementLocator));
            wait.Until(ExpectedConditions.ElementToBeClickable(elementLocator)); 

            var element = driver.FindElement(elementLocator);
            // scrolling
            jse.ExecuteScript("arguments[0].scrollIntoView(true);", element);

            return element;
        }
        public static void FocusOnElement(this IWebDriver driver, By elementLocator, int timeOutInSeconds)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds));
            var jse = (IJavaScriptExecutor)driver;
            // presence in DOM
            wait.Until(ExpectedConditions.ElementExists(elementLocator)); //FIXME: was: presenceOfElementLocated
            var element = driver.FindElement(elementLocator);
            ///comving to element
            Actions action = new Actions(driver);
            action.MoveToElement(element).Click().Build().Perform();
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            var jse = (IJavaScriptExecutor)driver;
            // scrolling
            jse.ExecuteScript("window.scrollBy(0,500)");
        }


        public static void SwitchToAnotherWindowHandle(this IWebDriver driver, string winHandleBefore)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));

            // Switch to new window opened

            foreach (string winHandle in driver.WindowHandles)
            {
                if (!winHandle.Equals(winHandleBefore))
                  wait.Until(d=> driver.SwitchTo().Window(winHandle));
            }
        }
        public static void MaximizeBrowser(this IWebDriver driver)
        {
            try
            {
                driver.Manage().Window.Maximize();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        } //end method MaximizeBrowser
        public static string GetValueOfElement(this IWebDriver driver, By elementLocator)
        {
            //WaitForWebElementToBeVisible(elementLocator);
            var webElement = driver.FindElement(elementLocator);
            var value = webElement?.GetAttribute("title");
            return value;
        } //endmethod

        public static bool IsDisplayed(this IWebDriver driver ,By element, string elementName)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            bool result;
            try
            {
                wait.Until(ExpectedConditions.InvisibilityOfElementLocated(element));
                result = false;
                Console.WriteLine(elementName + @" is not Displayed.");
            }
            catch (Exception)
            {
                result = true;
                Console.WriteLine(elementName + @" is  Displayed.");
            }
            return result;
        }

    }
}

