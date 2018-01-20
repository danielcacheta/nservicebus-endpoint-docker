using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

public class RequestMessageHandler
    : IHandleMessages<RequestMessage>
{
    static ILog log = LogManager.GetLogger<RequestMessageHandler>();

    public Task Handle(RequestMessage message, IMessageHandlerContext context)
    {
        log.Info($"Request received with description: {message.Data} and iteration: {message.Iteration}");

        var response = new ResponseMessage
        {
            Id = message.Id,
            Data = message.Data,
            NextIteration = message.Iteration+1
        };
        return context.Reply(response);
    }
}