using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class Videos
	{
		public Videos() { }

		//public Videos(string? classType)
		//{
		//	ClassType = classType;
		//}

		//public decimal Duration { get; set; }
		//public string? ClassType { get; set; }
		public int Score { get; set; } = 0;
		public int CorrectAnswers { get; set; } = 0;
		public int Questions { get; set; } = 0;
    }
}
