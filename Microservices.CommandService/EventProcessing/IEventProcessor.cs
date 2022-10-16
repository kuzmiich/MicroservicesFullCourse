namespace Microservices.CommandService.EventProcessing
{
    public interface IEventProcessor
    {
        void ProcessEvent(string message);
    }
}