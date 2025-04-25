using AutomobileRentalManagementAPI.Application;
using AutomobileRentalManagementAPI.CrossCutting.Validation;
using AutomobileRentalManagementAPI.Ioc;
using AutomobileRentalManagementAPI.WebApi.Middlewares;
using AutomobileRentalManagementAPI.Worker.Consumers.Motorcycle;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DependencyInjection.AddInfraData(builder.Services, builder.Configuration);

builder.Services.AddHostedService<MotorcycleConsumer>();
builder.Services.AddHostedService<MotorcycleNotificationConsumer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(ApplicationLayer).Assembly,
        typeof(Program).Assembly
    );
});

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
