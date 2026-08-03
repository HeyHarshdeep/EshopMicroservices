using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//add service to the container

builder.Services
    .AddApplicationServices()
    .AddInfrastructureService(builder.Configuration)
    .AddApiServices();

var app = builder.Build();

//configure the http pipeline
app.UseApiServices();

app.Run();
