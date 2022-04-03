using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Education.Service.DTOs.Groups
{
    public class GroupForCreationDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid TeacherId { get; set; }
    }
}
