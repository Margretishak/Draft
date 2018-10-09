using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_TestCase
{
    public class Manager
    {
		public enum Source {
			Excel = 1
		}

		public static List<Model_TestCase.TestCase> ReadTestCases(Source source)
		{
			List<Model_TestCase.TestCase> testCases = null;

			switch (source)
			{
				case Source.Excel:
					testCases = ExcelMgmt.ReadFromExcel();
					break;
			}

			return testCases;
		}
    }
}
