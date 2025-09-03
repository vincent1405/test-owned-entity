using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using test_owned_entity;

HostApplicationBuilder builder = new HostApplicationBuilder(args);

builder.Configuration.AddEnvironmentVariables()
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false);

builder.Services.AddDbContext<OrderContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("TestDB")
        ?? throw new InvalidOperationException("Connection string 'TestDB' not found.");
    options.UseSqlServer(connectionString, sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure();
    });
});

using IHost host = builder.Build();

using IServiceScope serviceScope = host.Services.CreateScope();
IServiceProvider serviceProvider = serviceScope.ServiceProvider;
OrderContext dbContext = serviceProvider.GetRequiredService<OrderContext>();
dbContext.Database.EnsureCreated();
Console.WriteLine("Database created successfully.");
dbContext.Database.Migrate();
Console.WriteLine("Database migrated successfully.");

await host.RunAsync();