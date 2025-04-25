using AutomobileRentalManagementAPI.Application.MessageQueue.Interfaces;
using AutomobileRentalManagementAPI.Application.MessageQueue.Publishers.Motorcycle;
using AutomobileRentalManagementAPI.Domain.HttpRepositories;
using AutomobileRentalManagementAPI.Domain.Repositories;
using AutomobileRentalManagementAPI.Domain.Repositories.Base;
using AutomobileRentalManagementAPI.Domain.Repositories.DeliveryPersons;
using AutomobileRentalManagementAPI.Domain.Repositories.Motorcycles;
using AutomobileRentalManagementAPI.Infra.Contexts;
using AutomobileRentalManagementAPI.Infra.Contexts.Impl;
using AutomobileRentalManagementAPI.Infra.HttpRepositories;
using AutomobileRentalManagementAPI.Infra.MessageQueue.RabbitMq;
using AutomobileRentalManagementAPI.Infra.Repositories;
using AutomobileRentalManagementAPI.Infra.Repositories.Base;
using AutomobileRentalManagementAPI.Infra.Repositories.DeliveryPersons;
using AutomobileRentalManagementAPI.Infra.Repositories.Motorcycles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AutomobileRentalManagementAPI.Ioc
{
    public static class DependencyInjection
    {
        private const string ConnectionString = "PostgresConnection";

        public static IServiceCollection AddInfraData(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddContext()
            .AddScoped<IDbConnectionFactory, NpgConnectionFactory>()
            .AddHttpRepositories()
            .AddRepositories()
            .AddPublishers()
            .AddRabbitMq(configuration);

        private static IServiceCollection AddRepositories(this IServiceCollection services) =>
            services
                .AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>))
                .AddScoped(typeof(IMongoRepositoryBase<>), typeof(MongoRepositoryBase<>))
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IMotorcycleRepository, MotorcycleRepository>()
                .AddScoped<IMotorcycleMongoRepository, MotorcycleMongoRepository>()
                .AddScoped<IDeliveryPersonRepository, DeliveryPersonRepository>()
                .AddScoped<ILocationRepository, LocationRepository>();

        private static IServiceCollection AddHttpRepositories(this IServiceCollection services) =>
            services.AddScoped<IBlobHttpRepository, BlobHttpRepository>();

        private static IServiceCollection AddPublishers(this IServiceCollection services) =>
            services
                .AddScoped<IMotorcyclePublisher, MotorcyclePublisher>();

        private static IServiceCollection AddContext(this IServiceCollection services)
        {
            var connectionString = services.BuildServiceProvider()
                .GetRequiredService<IConfiguration>()
                .GetConnectionString(ConnectionString);

            services
                .AddDbContext<RentalDbContext>(options => options.UseNpgsql(connectionString))
                .AddSingleton<MongoDbContextFactory>();

            return services;
        }

        private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration) =>
            services
                .Configure<RabbitMQSettings>(configuration.GetSection("RabbitMQSettings"))
                .AddScoped<IRabbitMqConnection, RabbitMqConnection>();
    }
}