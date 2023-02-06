namespace Microservices.CommandService.Dtos
{
    public class PlatformPublishDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Event { get; set; }

        public string Cost { get; set; }
    }
}