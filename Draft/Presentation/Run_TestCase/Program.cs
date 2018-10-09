using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Run_TestCase
{
	class Program
	{
		static void Main(string[] args)
		{
			Start();

			Console.Read();
		}

		private static void Start()
		{
			List<Model_TestCase.TestCase> testCases = Business_TestCase.Manager.ReadTestCases();

			foreach (var item in testCases)
			{
				Console.WriteLine($"Test Case Name: {item.Name}");
			}
		}
	}
}
