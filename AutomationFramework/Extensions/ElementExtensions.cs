using AutomationFramework.Browser;
using AventStack.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Extensions
{
    public static class ElementExtensions
    {
        public static void EnterText(this IWebElement element, string text, string elementName, ExtentTest test)
        {
            element.Clear();
            test.Log(Status.Info, elementName + " has been cleared");
            for (int i = 0; i < text.Length; i++)
            {
                element.SendKeys(text[i].ToString());
            }
            test.Log(Status.Info, text + " has been entered " + elementName + "field. ");
        }
        public static void EnterTextUsingJs(this IWebElement element, string text, string elementName)
        {
            //element.Clear();
            ((IJavaScriptExecutor)Browser.Driver.WebDriver).ExecuteScript("arguments[0].value='" + text + "';", element);
            Console.WriteLine(text + @" entered in the " + elementName + @" field.");
        }
        public static void EnterTextUsingJsWithOutBackSpace(this IWebElement element, string text, string elementName)
        {
            element.Clear();
            ((IJavaScriptExecutor)Browser.Driver.WebDriver).ExecuteScript("arguments[0].value='" + text + "';", element);
            element.SendKeys(Keys.Space);
            Console.WriteLine(text + @" entered in the " + elementName + @" field.");
        }
        public static void ClickUsingJavaScriptExec(this IWebElement element)
        {
            ((IJavaScriptExecutor)Browser.Driver.WebDriver).ExecuteScript("arguments[0].click();", element);
        }
        public static bool IsDisplayed(this IWebElement element, string elementName)
        {
            bool result;
            try
            {
                result = element.Displayed;
                Console.WriteLine(elementName + @" is Displayed.");
            }
            catch (Exception)
            {
                result = false;
                Console.WriteLine(elementName + @" is not Displayed.");
            }
            return result;
        }
        public static bool IsDisplayed_JavaScript(string locatortype, string locatorvalue)
        {

            string js = "";
            bool result = true;
            switch (locatortype)
            {
                case "xpath":
                    js = "(document.evaluate('" + locatorvalue + "', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue).offsetParent;";
                 result= (((IJavaScriptExecutor)Driver.WebDriver).ExecuteScript(js)!=null);
                    break;
                case "id":
                    js = "(document.getElementById('" + locatorvalue + "')).offsetParent;";
                    result = (((IJavaScriptExecutor)Driver.WebDriver).ExecuteScript(js) != null);
                    break;
            }

            return result;
        }
        public static void ClickOnIt(this IWebElement element, string elementName, ExtentTest test)
        {
            element.Click();
            test.Log(Status.Info, "Clicked on " + elementName);

        }
        public static void ScrollToElement(this IWebElement element, string elementName)
        {
            ((IJavaScriptExecutor)Browser.Driver.WebDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            Console.WriteLine(@" Scrolled To using Java script executor " + elementName);
        }
        public static string Get_ElementValue_UsingJavaScript(this IWebElement element)
        {
           return ((IJavaScriptExecutor)Browser.Driver.WebDriver).ExecuteScript("return arguments[0].value;", element).ToString();
        }

        public static void SelectByText(this IWebElement element, string text, string elementName, ExtentTest test)
        {
            var oSelect = new SelectElement(element);
            WaitForListToBeLoaded(oSelect,20);

            oSelect.SelectByText(text);
            test.Log(Status.Info, @" text selected on " + elementName);
        }
        public static void SelectByIndex(this IWebElement element, int index, string elementName)
        {
            var oSelect = new SelectElement(element);
            oSelect.SelectByIndex(index);
            Console.WriteLine(index + @" index selected on " + elementName);
        }
        public static void SelectByValue(this IWebElement element, string text, string elementName)
        {
            var oSelect = new SelectElement(element);
            oSelect.SelectByValue(text);
            Console.WriteLine(text + @" value selected on " + elementName);
        }
        public static string GetSelectedOption(this IWebElement element, string elementName, ExtentTest test)
        {
            var oSelect = new SelectElement(element);
            var opt = oSelect.SelectedOption.Text;
            test.Log(Status.Info, @" text selected on " + elementName);
            return opt;
        }
        public static void WaitForItToBeVisible(By element, int timeOutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Driver.WebDriver, TimeSpan.FromSeconds(timeOutInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
        }
        public static void WaitForItToBeClickable(By element, int timeOutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Driver.WebDriver, TimeSpan.FromSeconds(timeOutInSeconds));
            wait.Until(ExpectedConditions.ElementToBeClickable(element));
        }
        public static void WaitForListToBeLoaded(SelectElement element, int timeOutInSeconds)
        {
            var wait = new WebDriverWait(Browser.Driver.WebDriver, TimeSpan.FromSeconds(timeOutInSeconds));
            wait.Until(x=>element.Options.Count>0);
        }
        public static void DoubleClickOnIt(this IWebElement element)
        {
            var action = new Actions(Browser.Driver.WebDriver);
            action.DoubleClick(element).Build().Perform();
        }
        public static string GetTextOfIt(this IWebElement element)
        {
            var elementText = "";
            if (element != null)
                elementText = element.Text;

            return elementText;
        }//end method GetTextOfElement
        public static string GetText(this IWebElement e)
        {
            var js = (IJavaScriptExecutor)Browser.Driver.WebDriver;
            var text = js.ExecuteScript("return arguments[0].value", e);
            return text.ToString();
        }
        public static bool CheckDisabled(this IWebElement element)
        {
            return !element.Enabled;
        }
        public static string GetValueOfElement(this IWebDriver driver, By elementLocator, string attribueName)
        {
            //WaitForWebElementToBeVisible(elementLocator);
            var webElement = driver.FindElement(elementLocator);
            var value = webElement?.GetAttribute(attribueName);
            return value;
        } //endmethod
        public static void AttributeContains(By locator, string attribute, string value, int secondsToWait = 30)
        {
            new WebDriverWait(Browser.Driver.WebDriver, new TimeSpan(0, 0, secondsToWait))
               .Until(d => d.FindElement(locator).GetAttribute(attribute) == value);
        }

        public static void SimulateEvent(string dom, string eventq)
        {

            var simnew = "function simulate(element, eventName) { var options = extend(defaultOptions, arguments[2] || {}); var oEvent, eventType = null; for (var name in eventMatchers) { if (eventMatchers[name].test(eventName)) { eventType = name; break; }}" +

                " if (!eventType) throw new SyntaxError('Only HTMLEvents and MouseEvents interfaces are supported');" +

                " if (document.createEvent) { oEvent = document.createEvent(eventType);if (eventType == 'HTMLEvents') { oEvent.initEvent(eventName, options.bubbles, options.cancelable); }" +
                " else { oEvent.initMouseEvent(eventName, options.bubbles, options.cancelable, document.defaultView, options.button, options.pointerX, options.pointerY, options.pointerX, options.pointerY, options.ctrlKey, options.altKey, options.shiftKey, options.metaKey, options.button, element); }" +
                "   element.dispatchEvent(oEvent);}" +


                "else { options.clientX = options.pointerX; options.clientY = options.pointerY; var evt = document.createEventObject(); oEvent = extend(evt, options); element.fireEvent('on' + eventName, oEvent); }" +
                "return element;" +
            "}" +
            "function extend(destination, source) { for (var property in source) destination[property] = source[property]; return destination; }" +
            "var eventMatchers = { 'HTMLEvents': /^(?:load|unload|abort|error|select|change|submit|reset|focus|blur|resize|scroll)$/, 'MouseEvents': /^(?:click|dblclick|mouse(?:down|up|over|move|out))$/ };" +
            "var defaultOptions = { pointerX: 0,pointerY: 0,button: 0,ctrlKey: false,altKey: false,shiftKey: false,metaKey: false,bubbles: true,cancelable: true}; ";


            var strng = " simulate(" + dom + ", " + "'" + eventq + "'" + ");";
            var all = simnew + strng + strng + strng;
            ((IJavaScriptExecutor)Browser.Driver.WebDriver).ExecuteScript(all, null);
        }
        public static IWebElement WaitForItToBeVisibleAndGetElement(By element, int timeOutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(Browser.Driver.WebDriver, TimeSpan.FromSeconds(timeOutInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(element));
            return Browser.Driver.WebDriver.FindElement(element);
        }
        public static void Remove_ReadOnly_Attribute(this IWebElement element,IWebDriver driver)
        {
            string js = "";

            js = "arguments[0].removeAttribute('readonly');";
            ((IJavaScriptExecutor)driver).ExecuteScript(js, element);


        }

        public static void Set_Value_Javascript(string locatortype, string locatorvalue, string value)
        {

            string js = "";
            switch (locatortype)
            {
                case "xpath":
                    js = "(document.evaluate('" + locatorvalue + "', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue).setAttribute('value','" + value + "');";
                    ((IJavaScriptExecutor)Driver.WebDriver).ExecuteScript(js);
                    break;
                case "id":
                    js = "(document.getElementById('" + locatorvalue + "')).setAttribute('value','" + value + "');";
                    ((IJavaScriptExecutor)Driver.WebDriver).ExecuteScript(js);
                    break;
            }
        }


    }
}
