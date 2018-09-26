using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_TestCase
{
    public class TestCase
    {
		#region attributes
		private List<ExecStep> _ExecutionSteps;
		#endregion

		#region properties
		public string Name { get; set; }
		public string Description { get; set; }

		public List<ExecStep> ExecutionSteps {
			get
			{
				return this._ExecutionSteps;
			}
		}
		#endregion

		public TestCase()
		{
			this._ExecutionSteps = new List<ExecStep>();
		}
	}
}
