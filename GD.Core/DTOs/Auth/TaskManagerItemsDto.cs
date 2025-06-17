using Google.Apis.Util;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.DTOs.Auth
{
    public class TaskManagerItemsDto
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime date { get; set; } = DateAndTime.DateSerial(01, 01, 01);
        public bool IsDateAndTimeEnded { get; set; }
        public bool IsCompleted { get; set; }
    }
}
