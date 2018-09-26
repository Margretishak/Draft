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
		public static void ReadFromExcel()
		{
			using (var package = new ExcelPackage())
			{
				var xlFile = System.IO.File.Open(Properties.Settings.Default.ExcelFileName, System.IO.FileMode.Open);
				package.Load(xlFile);

				List<TestCase> testCases = readTestCaseSheet(package);

				readExecDataSheet(package, testCases);
			}
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

				if (exist)
				{
					r++;
					continue;
				}

				TestCase testCase = testCases.SingleOrDefault(t => t.Name.ToLower() == value.ToLower());
				if (string.IsNullOrWhiteSpace(testCase.Name))
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


				if (execStep.ID == string.Empty)
				{
					continue;
				}

				//TODO: complete the read from sheet3

				r++;
			}

			return testCases;
		}
	}
}
