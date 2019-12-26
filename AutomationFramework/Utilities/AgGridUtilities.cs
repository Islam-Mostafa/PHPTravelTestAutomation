using AutomationFramework.Extensions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomationFramework.Utilities
{
    public class AgGridUtilities
    {
        public static string GetDataFromGrid()
        {
            var gridData = Browser.Driver.WebDriver.ExecuteScriptUsingJs(
                "arr =[]; document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.api.forEachNode(function(n){arr.push(n.data);}); return JSON.stringify(arr)");
            return gridData;
        }
        public static void UpdateColumn(string colName, string colValue)
        {
            Browser.Driver.WebDriver.ExecuteScriptUsingJs("document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.api.forEachNode(function(n){n.data." + colName + "=" + colValue + ";});");
            RefreshGridView();
        }
        public static void ResetSorting()
        {
            Browser.Driver.WebDriver.ExecuteScriptUsingJs("document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.api.setSortModel([]);");
            RefreshGridView();
        }

        public static void UpdateColumnForSpecificRow(string rowNumber, string colValue, string colName)
        {
            Browser.Driver.WebDriver.ExecuteScriptUsingJs("document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.api.rowModel.rowsToDisplay[" + rowNumber + "].data." + colName + "=" + colValue + "");
            RefreshGridView();
        }
        public static void RefreshGridView()
        {
            Browser.Driver.WebDriver.ExecuteScriptUsingJs("document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.api.refreshView()");
        }
        public static void OpenRow(int rowNumber)
        {
            var firstColumnLocator = "div.ag-body-container>div>div:nth-child(0)";
            firstColumnLocator = firstColumnLocator.Replace("0", rowNumber.ToString());
            Browser.Driver.WebDriver.ScrollToElement(By.CssSelector(firstColumnLocator), 30);
            Browser.Driver.WebDriver.FindElement(By.CssSelector(firstColumnLocator)).Click();
        }
        public static string GetDataFromGrid(string gridCssLocator)
        {
            var gridData = Browser.Driver.WebDriver.ExecuteScriptUsingJs(
                "arr =[]; document.querySelector('" + gridCssLocator + "').au.controller.viewModel.gridOptions.api.forEachNode(function(n){arr.push(n.data);}); return JSON.stringify(arr)");
            return gridData;
        }
        public static void ScrollToSpecificRow(int rowNumber)
        {
            Browser.Driver.WebDriver.ExecuteScriptUsingJs("document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.api.ensureIndexVisible(" + rowNumber + ")");
        }
        public static void ScrollToSpecificColumn(string colName)
        {
            Browser.Driver.WebDriver.ExecuteScriptUsingJs(
                 "document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.api.ensureColumnVisible(" + colName + ")");
        }
        public static List<string> GetGridColumns()
        {
            var gridColumns = Browser.Driver.WebDriver.ExecuteScriptUsingJs(
                "colDefs =[]; document.querySelector('ag-grid-aurelia').au.controller.viewModel.gridOptions.columnApi.getAllDisplayedColumns().forEach(function(element) {colDefs.push(element.colDef.headerName)}); return JSON.stringify(colDefs)");
            var result = gridColumns.Replace("\"", "").Replace("[", "").Replace("]", "").Split(',').ToList();
            return result;
        }
}
}
