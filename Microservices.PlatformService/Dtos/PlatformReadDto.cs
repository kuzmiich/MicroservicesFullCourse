using System.ComponentModel.DataAnnotations;

namespace Microservices.PlatformService.Dtos
{
    public class PlatformReadDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = $"Incorrect {nameof(Name)} field, length should be between 4 and 50")]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect Publisher, length should be between 4 and 50")]
        public string Publisher { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect name, length should be between 4 and 50")]
        public string Cost { get; set; }
    }
}
