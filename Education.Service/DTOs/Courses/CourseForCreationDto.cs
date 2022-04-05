using Education.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Education.Service.Attributes;

namespace Education.Service.DTOs.Courses
{
    public class CourseForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public Subject Subject { get; set; }
        [Required]
        public ushort Duration { get; set; }
        [Required]
        public Guid TeacherId { get; set; }
        [Required]
        [MaxFileSize(500)]
        [AllowedExtensions(new string[] { ".mp4", ".3gp" })]
        public IFormFile Video { get; set; }
    }
}
