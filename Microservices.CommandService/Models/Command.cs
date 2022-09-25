namespace Microservices.CommandService.Models
{
    public class Command
    {
        public int Id { get; set; }

        public int PlatformId { get; set; }
        
        public string HowToDoActivity { get; set; }

        public string CommandLine { get; set; }

        public Platform Platform { get; set; }
    }
}