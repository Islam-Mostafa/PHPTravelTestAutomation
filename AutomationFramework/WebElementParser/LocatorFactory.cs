using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.WebElementParser
{
    public class LocatorFactory
    {
        public static By CreateLocator(WebElement element)
        {
            By by = null;
            switch (element.Locator)
            {
                case LocatorTypes.Id:
                    by = By.Id(element.Value);
                    break;
                case LocatorTypes.Name:
                    by = By.Name(element.Value);
                    break;
                case LocatorTypes.Xpath:
                    by = By.XPath(element.Value);
                    break;
                case LocatorTypes.CssSelector:
                    by = By.CssSelector(element.Value);
                    break;
                case LocatorTypes.LinkText:
                    by = By.LinkText(element.Value);
                    break;
                case LocatorTypes.PartialLinkText:
                    by = By.PartialLinkText(element.Value);
                    break;
                case LocatorTypes.TagName:
                    by = By.TagName(element.Value);
                    break;
                case LocatorTypes.ClassName:
                    by = By.ClassName(element.Value);
                    break;
                default:
                    break;
            }
            return by;
        }
    }
}
