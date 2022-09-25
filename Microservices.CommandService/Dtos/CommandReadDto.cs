using System.ComponentModel.DataAnnotations;

namespace Microservices.CommandService.Dtos
{
    public class CommandReadDto
    {
        public int Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Incorrect 'PlatformId', length should be between 1 and int.MaxValue")]
        public int PlatformId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect 'HowToDoActivity', length should be between 4 and 50")]
        public string HowToDoActivity { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Incorrect 'CommandLine', length should be between 4 and 50")]
        public string CommandLine { get; set; }
    }
}