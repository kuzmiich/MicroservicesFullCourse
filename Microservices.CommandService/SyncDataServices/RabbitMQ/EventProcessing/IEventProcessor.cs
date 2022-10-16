using System.Threading.Tasks;

namespace Microservices.CommandService.SyncDataServices.RabbitMQ.EventProcessing
{
    public interface IEventProcessor
    {
        Task ProcessEvent(string message);
    }
}