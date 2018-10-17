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
		public enum BrowserType
		{
			Chrome = 1,
			IE = 2
		}
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

		public bool Succeeded { get; set; }
		public string Comment { get; set; }
		public BrowserType Browser { get; set; } 
		#endregion

		public TestCase()
		{
			this._ExecutionSteps = new List<ExecStep>();
		}
	}
}
