using Microsoft.AspNetCore.Diagnostics;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

//Add Services to the Container



var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddLogging();

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

if(builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogIntialData>();
}    



builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Configure the HTTP request pipeline

app.MapCarter();

app.UseExceptionHandler(options => { });

app.Run();
