using System;
using System.Threading;
using System.Threading.Tasks;
using NServiceBus;

class Program
{
    static async Task Main()
    {
        Console.CancelKeyPress += OnExit;

        Console.Title = "Samples.Docker.Sender";

        Console.WriteLine("Use 'docker-compose down' to stop containers.");

        var message = new RequestMessage
        {
            Id = Guid.NewGuid(),
            Data = "String property value",
            Iteration = 1
        };

        await new MessageSender(closingEvent).SendMessage(message);
    }

    static void OnExit(object sender, ConsoleCancelEventArgs args)
    {
        closingEvent.Set();
    }

    static AutoResetEvent closingEvent = new AutoResetEvent(false);
}