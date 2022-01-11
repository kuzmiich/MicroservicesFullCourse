using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KuzmichInc.Microservices.PlatformService.ViewModels
{
    public class PlatformViewModel
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Incorrect name, length should be between 5 and 50")]
        public string Name { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Incorrect publisher, length should be between 5 and 50")]
        public string Publisher { get; set; }
    }
}
