using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.DTOs
{
	public class FeedBackDto
	{
		[Required(ErrorMessage = "This Field is Required")]
		public int Q1Answer { get; set; }
		[Required(ErrorMessage = "This Field is Required")]
		public int Q2Answer { get; set; }
		[Required(ErrorMessage = "This Field is Required")]
		public int Q3Answer { get; set; }

		[Required(ErrorMessage = "This Field is Required")]
		public string Q4Answer { get; set; }

		[Required(ErrorMessage = "This Field is Required")]
		public string Q5Answer { get; set; }

		[Required(ErrorMessage = "This Field is Required")]
		public string Q6Answer { get; set; }

		[Required(ErrorMessage = "This Field is Required")]
		public string Q7Answer { get; set; }

		public string? Suggestions { get; set; } 
	}
}
