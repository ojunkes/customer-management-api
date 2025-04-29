using Customers.Management.Domain.Messages;
using MassTransit;

namespace Customers.Management.Consumer.Consumers;

public class ZipCodeMessageConsumer : IConsumer<ZipCodeMessage>
{
    public ZipCodeMessageConsumer()
    {
    }

    public Task Consume(ConsumeContext<ZipCodeMessage> context)
    {
        var zipCode = context.Message.ZipCode;

        Console.WriteLine($"[Worker] CEP recebido: {zipCode}");

        return Task.CompletedTask;
    }
}
