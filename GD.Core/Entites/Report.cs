using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.Entites
{
    public class Report:BaseEntity<int>
    {
        public DateOnly ReportDate {  get; set; } 
        public int MonthlyScore { get; set; }
        public string ChildEmail { get; set; }
        public string ActivitesBreakdowns { get; set; }
        public string HighLights {  get; set; }
        public string Tasks { get; set; }
        public string BehaviorNotes { get; set; }
        public string Recommendations {get; set; }
        public DateTime CreationDate { get; set; }  



    }
}
