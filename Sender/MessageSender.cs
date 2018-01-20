using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

internal class MessageSender
{
    private AutoResetEvent _closingEvent;

    public MessageSender()
    {
        _closingEvent = new AutoResetEvent(false);
    }
    
    public MessageSender(AutoResetEvent closingEvent)
    {
        _closingEvent = closingEvent;
    }

    public async Task SendMessage(RequestMessage message)
    {
        var endpointConfiguration = new EndpointConfiguration("Samples.Docker.Sender");
        var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
        transport.ConnectionString("host=rabbitmq");
        transport.UseConventionalRoutingTopology();
        var delayedDelivery = transport.DelayedDelivery();
        delayedDelivery.DisableTimeoutManager();
        endpointConfiguration.EnableInstallers();

        // The RabbitMQ container starts before endpoints but it may
        // take several seconds for the broker to become reachable.
        await RabbitHelper.WaitForRabbitToStart()
            .ConfigureAwait(false);

        var endpointInstance = await Endpoint.Start(endpointConfiguration)
                    .ConfigureAwait(false);

        Console.WriteLine("Sending a message...");

        Console.WriteLine($"Requesting to get data by id: {message.Id:N}");

        await endpointInstance.Send("Samples.Docker.Receiver", message)
            .ConfigureAwait(false);

        Console.WriteLine("Message sent.");

        // Wait until the message arrives.
        _closingEvent.WaitOne();

        await endpointInstance.Stop()
            .ConfigureAwait(false);
    }
}