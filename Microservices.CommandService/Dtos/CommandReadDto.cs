using System.ComponentModel.DataAnnotations;

namespace Microservices.CommandService.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        
        [Required]
        [MinLength(1, ErrorMessage = $"Incorrect {nameof(PlatformId)} parameter, length should be less than 1")]
        public int PlatformId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = $"Incorrect {nameof(HowToDoActivity)} parameter, length should be between 4 and 100")]
        public string HowToDoActivity { get; set; }

        [Required]
        [StringLength(1000, MinimumLength = 4, ErrorMessage = $"Incorrect ${nameof(CommandLine)} parameter, length should be between 4 and 1000")]
        public string CommandLine { get; set; }
    }
}