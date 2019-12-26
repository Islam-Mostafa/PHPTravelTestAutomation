using Excel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace AutomationFramework.ExcelParser
{
    public class TestCasesParser
    {
        public static string GetValueOf(string key, string methodName, string className)
        {
            var testData = ParseTestDataFromExcel(className, methodName);
            try
            {
                return testData[key];
            }
            catch
            {
                return null;
            }


        }//end method

        public static ArrayList GetAllValuesOf(string key, string methodName, string className)
        {
            ArrayList splittedTestData = new ArrayList();
            var testData = ParseTestDataFromExcel(className, methodName);

            if (testData[key] != null)
            {
                string commaSeparatedTestData = testData[key];
                splittedTestData.AddRange(commaSeparatedTestData.Split(','));

            }//endif

            return splittedTestData;

        }//end method

        public static Dictionary<string, string> ParseTestDataFromExcel(string className, string requiredMethodName)
        {
            var directory = AppDomain.CurrentDomain.RelativeSearchPath ??
                                  AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(directory, "Data", "Data.xlsx");


            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            stream.Dispose();
            excelReader.IsFirstRowAsColumnNames = true;

            DataTable requiredWorkSheet = excelReader.AsDataSet().Tables[className];
            Dictionary<string, string> testCaseData = new Dictionary<string, string>();

            //The actual test data starts from the 9th row in the excel sheet (The above rows are for test reference)
            //Due to the setting that First Row As Coulmn Names, the data is considered to start from the 8th row 
            //As we are zero-based, the index is 7
            for (int rowCounter = 0; rowCounter < requiredWorkSheet.Rows.Count; rowCounter++)
            {
                //The method name is the second column in the sheet (column index is 1 for zero-based array)
                string inspectedMethodName = requiredWorkSheet.Rows[rowCounter].ItemArray[0].ToString();

                if (inspectedMethodName == requiredMethodName)
                {
                    //The actual data starts from the thrid column, index is 2
                    for (int coulmnCounter = 1; coulmnCounter < requiredWorkSheet.Columns.Count; coulmnCounter++)
                    {
                        //if this cell is not empty (it has data in it)
                        if (requiredWorkSheet.Rows[rowCounter].ItemArray[coulmnCounter].ToString() != string.Empty)
                        {
                            //Add the value of this cell to the Data Dictionary
                            // Key is the Column Header (Control Name), the value is the value located in the cell
                            testCaseData.Add(requiredWorkSheet.Columns[coulmnCounter].ColumnName, requiredWorkSheet.Rows[rowCounter].ItemArray[coulmnCounter].ToString());

                        }//enndif

                    }//endfor

                    //exit the loop as you already found the required method
                    //so there is no need to loop on the rest of the rows
                    break;

                }//endif

            }//endfor

            return testCaseData;

        }//end method ParseTestDataFromExcel
        public static List<Dictionary<string, string>> ParseTestDataFromExcel_Multiple(string className, string requiredMethodName)
        {
            var directory = AppDomain.CurrentDomain.RelativeSearchPath ??
                                  AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(directory, "Data", "Data.xlsx");


            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            stream.Dispose();
            excelReader.IsFirstRowAsColumnNames = true;
            List<Dictionary<string, string>> multiple_rows = new List<Dictionary<string, string>>();

            DataTable requiredWorkSheet = excelReader.AsDataSet().Tables[className];

            for (int rowCounter = 0; rowCounter < requiredWorkSheet.Rows.Count; rowCounter++)
            {
                //The method name is the second column in the sheet (column index is 1 for zero-based array)
                string inspectedMethodName = requiredWorkSheet.Rows[rowCounter].ItemArray[0].ToString();
                Dictionary<string, string> testCaseData = new Dictionary<string, string>();

                if (inspectedMethodName == requiredMethodName)
                {
                    for (int coulmnCounter = 1; coulmnCounter < requiredWorkSheet.Columns.Count; coulmnCounter++)
                    {
                        if (requiredWorkSheet.Rows[rowCounter].ItemArray[coulmnCounter].ToString() != string.Empty)
                        {
                                testCaseData.Add(requiredWorkSheet.Columns[coulmnCounter].ColumnName, requiredWorkSheet.Rows[rowCounter].ItemArray[coulmnCounter].ToString());

                        }

                    }
                    multiple_rows.Add(testCaseData);
                }

            }

            return multiple_rows;

        }

        public static List<Dictionary<string, string>> ParseTestDataFromExcel_Multiple2(string className)
        {
            var directory = AppDomain.CurrentDomain.RelativeSearchPath ??
                                  AppDomain.CurrentDomain.BaseDirectory;
            var filePath = Path.Combine(directory, "Data", "Data.xlsx");


            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            stream.Dispose();
            excelReader.IsFirstRowAsColumnNames = true;
            List<Dictionary<string, string>> multiple_rows = new List<Dictionary<string, string>>();

            DataTable requiredWorkSheet = excelReader.AsDataSet().Tables[className];

            for (int rowCounter = 0; rowCounter < requiredWorkSheet.Rows.Count; rowCounter++)
            {
                //The method name is the second column in the sheet (column index is 1 for zero-based array)
                string inspectedMethodName = requiredWorkSheet.Rows[rowCounter].ItemArray[0].ToString();
                Dictionary<string, string> testCaseData = new Dictionary<string, string>();

                
                    for (int coulmnCounter = 0; coulmnCounter < requiredWorkSheet.Columns.Count; coulmnCounter++)
                    {
                        if (requiredWorkSheet.Rows[rowCounter].ItemArray[coulmnCounter].ToString() != string.Empty && requiredWorkSheet.Rows[rowCounter].ItemArray[coulmnCounter].ToString() != "")
                        {
                            testCaseData.Add(requiredWorkSheet.Columns[coulmnCounter].ColumnName, requiredWorkSheet.Rows[rowCounter].ItemArray[coulmnCounter].ToString());

                        }

                    }
                if (testCaseData.Count!=0)
                {
                    multiple_rows.Add(testCaseData);
                }
                 
                

            }

            return multiple_rows;

        }

    }
}
