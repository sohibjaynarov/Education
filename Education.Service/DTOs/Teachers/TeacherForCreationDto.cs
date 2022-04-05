using Education.Service.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.DTOs.Teachers
{
    public class TeacherForCreationDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [MaxFileSize(500)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile Image { get; set; }
    }
}
