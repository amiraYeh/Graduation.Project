using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class ParentTest : BaseEntity<int>
	{
		public int DistractionRatio { get; set; }
		public string ChildEmail { get; set; }

		public DateTimeOffset DateOfSubmited { get; set; } = DateTimeOffset.UtcNow;
	}
}
