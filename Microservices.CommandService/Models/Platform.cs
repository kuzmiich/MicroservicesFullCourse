using System.Collections.Generic;

namespace Microservices.CommandService.Models
{
    public class Platform
    {
        public int Id { get; set; }

        public int ExternalPlatformId { get; set; }

        public string Name { get; set; }

        public ICollection<Command> Commands { get; set; } = new List<Command>();
    }
}