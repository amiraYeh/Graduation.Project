using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class ChildClass : BaseEntity<int>
	{
        public string ChildEmail { get; set; }
		private string? Type { get; set; }
		public int ClassScore { get; set; } = 0;
		public Games Game { get; set; }
		public Videos Video { get; set; }

        //public int? AdviceId { get; set; }
        public ICollection<Advice> Advice { get; set; }
		//public int? StoryId { get; set; }
		public ICollection<Story> Story { get; set; }

	}
}
