using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Microservices.PlatformService.Dtos
{
    public class PlatformRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Incorrect name, length should be between 2 and 50")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Incorrect name, length should be between 2 and 50")]
        public string Publisher { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Incorrect name, length should be between 2 and 50")]
        public string Cost { get; set; }
    }
}
