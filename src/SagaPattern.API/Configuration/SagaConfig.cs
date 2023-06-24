using NServiceBus;

namespace SagaPattern.API.Configuration;

public static class SagaConfig
{
    public static IServiceCollection AddsagaConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var endpointConfiguration = new EndpointConfiguration(configuration["NServiceBus:EndpointName"]);
        endpointConfiguration.UseTransport<LearningTransport>();
        endpointConfiguration.UsePersistence<LearningPersistence>();
        endpointConfiguration.SendFailedMessagesTo(configuration["NServiceBus:SendFailedMessagesTo"]);
        endpointConfiguration.AuditProcessedMessagesTo(configuration["NServiceBus:AuditProcessedMessagesTo"]);

        var endpointInstance = NServiceBus.Endpoint.Start(endpointConfiguration).GetAwaiter().GetResult();
        services.AddSingleton<IMessageSession>(endpointInstance);

        return services;
    }
}

