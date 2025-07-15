using GP.Focusi.Core.DTOs.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GP.Focusi.Core.DTOs
{
    public class EditProfileDto
    {
        [Required(ErrorMessage = "Name is Required !")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Age is Required !")]
        [Range(4, 12, ErrorMessage = "Age Must be between 4 to 12 year")] 
        public int Age { get; set; }
        public IFormFile? Picture { get; set; }

    }
}
