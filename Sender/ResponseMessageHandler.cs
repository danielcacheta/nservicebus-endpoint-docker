using System;
using System.Threading.Tasks;
using NServiceBus;
using NServiceBus.Logging;

public class ResponseMessageHandler
    : IHandleMessages<ResponseMessage>
{
    static ILog log = LogManager.GetLogger<ResponseMessageHandler>();

    public Task Handle(ResponseMessage message, IMessageHandlerContext context)
    {
        log.Info($"Response received with description: {message.Data} and next iteration: {message.NextIteration}");

        if(message.NextIteration <= 10)
            SendNextMessage(message.NextIteration);

        return Task.CompletedTask;
    }

    private async void SendNextMessage(int nextIteration)
    {
        var newMessage = new RequestMessage
        {
            Id = Guid.NewGuid(),
            Data = "String property value",
            Iteration = nextIteration
        };

        await new MessageSender().SendMessage(newMessage);
    }
}