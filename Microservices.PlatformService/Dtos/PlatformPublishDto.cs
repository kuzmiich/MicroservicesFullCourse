using System.ComponentModel.DataAnnotations;

namespace Microservices.PlatformService.Dtos
{
    public class PlatformPublishDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect 'Name', length should be between 4 and 50")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect 'Event', length should be between 4 and 50")]
        public string Event { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect 'Cost', length should be between 4 and 100")]

        public string Cost { get; set; }
    }
}