﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.DTOs
{
	public class AddTaskManagerItemsDto
	{
		//public int Id {  get; set; }
		public string Name { get; set; }
		public DateTime date { get; set; } = DateAndTime.DateSerial(01, 01, 01);
		public bool IsDateAndTimeEnded { get; set; } 
		public bool IsCompleted { get; set; }

	}
}
