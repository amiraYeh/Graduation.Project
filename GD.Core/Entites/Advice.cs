﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class Advice:BaseEntity<int>
	{
        public string ClassType { get; set; }
		public string Content { get; set; }
    }
}
