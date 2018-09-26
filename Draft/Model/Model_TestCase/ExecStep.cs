using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TestCase
{
	public class ExecStep
	{
		public enum ExecType
		{
			Navigate = 1,
			Input = 2,
			Click = 4
		}

		public string ID { get; set; }
		public string URL { get; set; }
		public int WaitPeriod { get; set; }

		public ExecType ExecDataType { get; set; }

		public List<Element> InputElements { get; set; }
		public List<Element> ExpectedElements { get; set; }
		public List<Element> ActualElements { get; set; }
	}
}
