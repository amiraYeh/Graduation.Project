using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class TaskManagerItems:BaseEntity<int>
	{
		public string Name { get; set; }
		public bool IsCompleted { get; set; }
		public DateTime date { get; set; } = DateTime.Now;
		public string ChildEmail { get; set; }
		public string? TaskManagerId { get; set; }
		public TaskManager? TaskManager { get; set; }
		public bool IsDateAndTimeEnded { get; set; } 


	}
}
