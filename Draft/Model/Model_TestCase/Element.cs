using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TestCase
{
	public class Element
	{
		public enum ElementTypes
		{
			TextBox = 1,
			Button = 2,
			DropDownList = 4
		}

		public enum ElementCategories {
			Input = 1,
			Expected = 2,
			Actual = 4
		}

		public string ID { get; set; }
		public string Name { get; set; }
		public string CSS { get; set; }
		public string XPath { get; set; }
		public string Value { get; set; }
		public ElementTypes ElementType { get; set; }
		public ElementCategories ElementCategory { get; set; }
	}
}
