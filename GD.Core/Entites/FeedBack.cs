using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
	public class FeedBack:BaseEntity<int>
	{
        public int Q1ProgramHelpParent { get; set; }
        public int Q2SuitableActivity { get; set; }
        public int Q3ContentUnderstand {  get; set; }   
        public string Q4behaviurImprovement { get;set; }

        public string Q5ContinueInProgram { get; set; } 

        public string Q6RecomendProgram { get;set; }

        public string Q7MostHelpfulPart {  get; set; }  

        public string? Suggestions { get; set; } = null;

        public DateTimeOffset DateOfCreation { get; set; } = DateTimeOffset.UtcNow;

        public string ChildEmail { get; set; }

    }
}
