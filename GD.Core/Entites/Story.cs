using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class Story : BaseEntity<int>
	{
        public string ClassType { get; set; }
		public string StoryName { get; set; }
    }
}
