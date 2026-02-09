using Microsoft.EntityFrameworkCore;
using Severstal.Api.Endpoints;
using Severstal.Api.MIddleware;
using Severstal.Application.Mapping;
using Severstal.Application.Rolls.Commands.AddRoll;
using Severstal.Domain.Interfaces;
using Severstal.Infrastructure.Data;
using Severstal.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");


builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();



builder.Services.AddDbContextFactory<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseNpgsql(connectionString,
        m => m.MigrationsAssembly("Severstal.Infrastructure")),
    contextLifetime: ServiceLifetime.Scoped,
    optionsLifetime: ServiceLifetime.Singleton
);

builder.Services.AddScoped<IRollCrudRepository, RollCrudRepository>();
builder.Services.AddScoped<IRollStatisticsRepository, RollStatisticsRepository>();

builder.Services.AddMediatR( cfg => cfg.RegisterServicesFromAssemblyContaining<AddRollCommand>());
builder.Services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapperProfile>());


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var maxTries = 10;
    for (int i = 0; i < maxTries; i++)
    {
        try
        {
            context.Database.Migrate();
        }
        catch
        {
            if (i == maxTries - 1)
                break;
            await Task.Delay(5000);
        }
    }
}


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<GlobalExpceptionMiddleware>();
app.UseHttpsRedirection();
app.AddRollsEndpoints();


app.Run();

