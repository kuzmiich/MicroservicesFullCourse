﻿using System.ComponentModel.DataAnnotations;

namespace Microservices.PlatformService.Dtos
{
    public class PlatformCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect name, length should be between 4 and 50")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect name, length should be between 4 and 50")]
        public string Publisher { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect name, length should be between 4 and 50")]
        public string Cost { get; set; }
    }
}
