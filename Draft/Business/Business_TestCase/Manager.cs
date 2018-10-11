using Model_TestCase;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_TestCase
{
    public class Manager
    {
		public static List<Model_TestCase.TestCase> TestCases { get; set; }

		public static List<Model_TestCase.TestCase> ReadTestCases()
		{
			Manager.TestCases = Data_TestCase.Manager.ReadTestCases(Data_TestCase.Manager.Source.Excel);

			return Manager.TestCases;
		}

		public static bool RunTestCases()
		{
			foreach (var item in Manager.TestCases)
			{
				runTestCase(item);
			}

			return true;
		}

		private static void runTestCase(Model_TestCase.TestCase testCase)
		{
			IWebElement query = null;

			using (IWebDriver driver = new ChromeDriver())
			{
				foreach (var step in testCase.ExecutionSteps)
				{
					switch (step.ExecDataType)
					{
						case Model_TestCase.ExecStep.ExecType.Navigate:
							driver.Navigate().GoToUrl(step.URL);
							break;
						case Model_TestCase.ExecStep.ExecType.Input:
							query = executeInputStep(driver, step);
							break;
						case Model_TestCase.ExecStep.ExecType.Click:
							query.Submit();
							break;
						case Model_TestCase.ExecStep.ExecType.Assert:
							var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(step.WaitPeriod));
							wait.Until(d => d.Title.StartsWith("linkdev", StringComparison.OrdinalIgnoreCase));
							break;
						default:
							break;
					}
				}
				

				

				
				Console.WriteLine("Page title is: " + driver.Title);
			}
		}

		private static IWebElement executeInputStep(IWebDriver driver, ExecStep step)
		{
			IWebElement query = null;

			foreach (var element in step.InputElements)
			{
				if (element.ElementType != Element.ElementTypes.TextBox)
				{
					continue;
				}

				query = findElementByIdentifier(driver, element);

				query.SendKeys(element.Value);
			}

			return query;
		}

		private static IWebElement findElementByIdentifier(IWebDriver driver, Element element)
		{
			IWebElement uiElement = null;

			if (!String.IsNullOrEmpty(element.ID))
			{
				uiElement = driver.FindElement(By.Id(element.ID.Trim()));
			}
			else if (!String.IsNullOrEmpty(element.XPath))
			{
				uiElement = driver.FindElement(By.XPath(element.XPath.Trim()));
			}
			else if (!String.IsNullOrEmpty(element.Name))
			{
				uiElement = driver.FindElement(By.Name(element.Name.Trim()));
			}
			else if (!String.IsNullOrEmpty(element.CSS))
			{
				uiElement = driver.FindElement(By.CssSelector(element.CSS.Trim()));
			}

			return uiElement;
		}
	}
}
