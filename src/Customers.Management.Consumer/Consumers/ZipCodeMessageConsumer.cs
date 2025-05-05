using Customers.Management.Domain.Entities;
using Customers.Management.Domain.Interfaces.Adapters;
using Customers.Management.Domain.Interfaces.Repositories;
using Customers.Management.Domain.Messages;
using Customers.Management.Domain.Responses;
using MassTransit;

namespace Customers.Management.Consumer.Consumers;

public class ZipCodeMessageConsumer : IConsumer<ZipCodeMessage>
{
    private readonly ILogger<ZipCodeMessageConsumer> _logger;
    private readonly IAddressRepository _addressRepository;
    private readonly IViaCepAdapter _viaCepAdapter;

    public ZipCodeMessageConsumer(
        ILogger<ZipCodeMessageConsumer> logger,
        IAddressRepository addressRepository,
        IViaCepAdapter viaCepAdapter)
    {
        _logger = logger;
        _addressRepository = addressRepository;
        _viaCepAdapter = viaCepAdapter;
    }

    public async Task Consume(ConsumeContext<ZipCodeMessage> context)
    {
        var zipCode = context.Message.ZipCode;
        _logger.LogInformation("[Worker] CEP recebido: {zipCode}", zipCode);

        var existingAddress = await _addressRepository.GetByZipCodeAsync(zipCode, context.CancellationToken);
        if (!UpdateAddress(existingAddress))
        {
            _logger.LogInformation("[Worker] CEP já atualizado na base de CEP: {zipCode}", zipCode);
            return;
        }

        var addressResponse = await _viaCepAdapter.GetAddressByZipCodeAsync(zipCode, context.CancellationToken);
        if (addressResponse?.Cep == null)
        {
            _logger.LogWarning("[Worker] Não foi possível obter o endereço para o CEP: {zipCode}", zipCode);
            return;
        }

        if (existingAddress != null)
        {
            existingAddress.UpdateFrom(addressResponse);
            await _addressRepository.Update(existingAddress, context.CancellationToken);
        }
        else
        {
            var address = addressResponse.ToEntity();
            await _addressRepository.InsertAsync(address, context.CancellationToken);
        }
        await _addressRepository.CommitAsync();
    }

    private bool UpdateAddress(Address? address)
    {
        if (address == null)
            return true;

        var lastUpdated = address.ModifiedAt ?? address.CreatedAt;
        return (DateTimeOffset.UtcNow - lastUpdated).TotalDays > 365;
    }
}
