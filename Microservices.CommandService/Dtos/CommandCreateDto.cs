using System.ComponentModel.DataAnnotations;

namespace Microservices.CommandService.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        [StringLength(200, MinimumLength = 4, ErrorMessage = "Incorrect 'HowToDoActivity', length should be between 4 and 200")]
        public string HowToDoActivity { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Incorrect 'CommandLine', length should be between 4 and 100")]
        public string CommandLine { get; set; }
    }
}