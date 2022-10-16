using System.ComponentModel.DataAnnotations;

namespace Microservices.CommandService.Dtos
{
    public class GenericEventDto
    {
        [Required]
        public string Event { get; set; }
    }
}