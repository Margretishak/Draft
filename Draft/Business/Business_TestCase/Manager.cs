using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TestCase
{
    public class Manager
    {
		public static List<Model_TestCase.TestCase> ReadTestCases()
		{
			List<Model_TestCase.TestCase> testCases = Data_TestCase.Manager.ReadTestCases(Data_TestCase.Manager.Source.Excel);

			return testCases;
		}
	}
}
