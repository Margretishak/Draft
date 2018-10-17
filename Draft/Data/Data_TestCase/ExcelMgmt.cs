using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model_TestCase;

namespace Data_TestCase
{
	internal class ExcelMgmt
	{
		public static List<TestCase> ReadFromExcel()
		{
			List<TestCase> testCases;

			using (var package = new ExcelPackage())
			{
				var xlFile = System.IO.File.Open(Properties.Settings.Default.ExcelFileName, System.IO.FileMode.Open);
				package.Load(xlFile);

				testCases = readTestCaseSheet(package);

				readExecDataSheet(package, testCases);

				readElementSheet(package, testCases);
			}

			return testCases;
		}

		private static bool readData(ExcelPackage package, int sheetNo, int r, int c, out string data)
		{

			data = string.Empty;

			Object obj = package.Workbook.Worksheets[sheetNo].Cells[r, c].Value;

			if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
			{
				data = obj.ToString();
				return true;
			}
			else
			{
				return false;
			}

		}

		private static List<TestCase> readTestCaseSheet(ExcelPackage package)
		{
			List<TestCase> testCases = new List<TestCase>();

			int r = 2;

			while (true)
			{
				TestCase testCase = new TestCase();

				string data = string.Empty;

				bool exist = readData(package, 1, r, 1, out data);
				if (exist)
				{
					testCase.Name = data;
				}
				else
				{
					break;
				}

				readData(package, 1, r, 2, out data);
				testCase.Description = data;

				readData(package, 1, r, 3, out data);
				testCase.Browser = (TestCase.BrowserType)Enum.Parse(typeof(TestCase.BrowserType), data);

				testCases.Add(testCase);

				r++;
			}

			return testCases;
		}

		private static List<TestCase> readExecDataSheet(ExcelPackage package, List<TestCase> testCases)
		{
			int r = 2;

			while (true)
			{
				ExecStep data = new ExecStep();
				string value = string.Empty;
				bool exist = readData(package, 2, r, 1, out value);

				if (exist)
				{
					data.ID = value;
				}
				else
				{
					break;
				}

				exist = readData(package, 2, r, 2, out value);

				if (!exist)
				{
					r++;
					continue;
				}

				TestCase testCase = testCases.SingleOrDefault(t => t.Name.ToLower() == value.ToLower());
				if (testCase == null || string.IsNullOrWhiteSpace(testCase.Name))
				{
					r++;
					continue;
				}

				exist = readData(package, 2, r, 3, out value);

				if (exist)
				{
					data.ExecDataType = (ExecStep.ExecType)Enum.Parse(typeof(ExecStep.ExecType), value);
				}

				readData(package, 2, r, 4, out value);
				data.URL = value;


				//TODO: read username and password

				readData(package, 2, r, 7, out value);

				int intWait = 0;
				int.TryParse(value, out intWait);
				data.WaitPeriod = intWait;

				testCase.ExecutionSteps.Add(data);

				r++;
			}

			return testCases;
		}

		private static List<TestCase> readElementSheet(ExcelPackage package, List<TestCase> testCases)
		{
			int r = 2;

			while (true)
			{
				Element element = new Element();

				string value = string.Empty;
				bool exist = readData(package, 3, r, 1, out value);

				if (!exist)
				{
					break;
				}

				var execStep = (from t in testCases
								from step in t.ExecutionSteps
								where step.ID == value
								select step).SingleOrDefault();

				//if step ID not exist in step sheet 2
				if (execStep == null || execStep.ID == string.Empty)
				{
					continue;
				}

				exist = readData(package, 3, r, 2, out value);

				if (exist)
				{
					element.ElementCategory = (Element.ElementCategories)Enum.Parse(typeof(Element.ElementCategories), value);
				}
				else
				{
					continue;
				}

				readData(package, 3, r, 3, out value);
				element.ID = value;

				readData(package, 3, r, 4, out value);
				element.Name = value;

				readData(package, 3, r, 5, out value);
				element.CSS = value;

				readData(package, 3, r, 6, out value);
				element.XPath = value;

				//if no identifier exist do not proceed
				if (string.IsNullOrEmpty(element.ID) && string.IsNullOrEmpty(element.Name) && string.IsNullOrEmpty(element.CSS) && string.IsNullOrEmpty(element.XPath))
				{
					r++;
					continue;
				}

				readData(package, 3, r, 7, out value);
				element.Value = value;

				readData(package, 3, r, 8, out value);
				element.ElementType = (Element.ElementTypes)Enum.Parse(typeof(Element.ElementTypes), value);

				//add element to its respective category in the step
				switch (element.ElementCategory)
				{
					case Element.ElementCategories.Input:
						execStep.InputElements.Add(element);
						break;
					case Element.ElementCategories.Expected:
						execStep.ExpectedElements.Add(element);
						break;
				}

				r++;
			}

			return testCases;
		}
	}
}
