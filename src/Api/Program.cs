using Andreani.Arq.Observability.Extensions;
using Andreani.Arq.WebHost.Extension;
using Microsoft.AspNetCore.Builder;
using desafio_backend.Application.Boopstrap;
using desafio_backend.Infrastructure.Boopstrap;
using Andreani.Arq.AMQStreams.Extensions;
using Andreani.Scheme.Onboarding;
using desafio_backend.Services;
using MongoDB.Driver.Core.Events;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAndreaniWebHost(args);
builder.Services.ConfigureAndreaniServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddKafka(builder.Configuration)
    .ToProducer<Pedido>("PedidoCreado")
    .ToConsumer<EventSubscriber, Pedido>("PedidoAsignado")
    .Build();
builder.Host.AddObservability();

var app = builder.Build();

app.UseObservability();
app.ConfigureAndreani();

app.Run();
