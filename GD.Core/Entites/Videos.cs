using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class Videos : BaseEntity<int>
    {
        public string ClassType { get; set; }
		public string VideoName { get; set; }
        public int Score { get; set; } = 0;
		public int CorrectAnswers { get; set; } = 0;
		public int Questions { get; set; } = 0;
    }
}
