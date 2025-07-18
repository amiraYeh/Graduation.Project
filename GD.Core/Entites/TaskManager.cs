﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class TaskManager:BaseEntity<string>
	{
		public TaskManager(string id)
		{
			ID = id;
		}
		public TaskManager() { }

        public string ChildMail { get; set; }
        public int TaskManagerScore { get; set; } = 0; 
        public List<TaskManagerItems> Items { get; set; }

		public int ItemsCount { get; set; } = 0;
		public DateTimeOffset CreationDate { get; set; } = DateTimeOffset.UtcNow;



    }
}
