using GrandNodeOrderIntegration;
using GrandNodeOrderIntegration.Services;
using GrandNodeOrderIntegration.Services.Interfaces;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();

        // service registiration
        services.AddSingleton<ITrendyolService, TrendyolService>();
        services.AddSingleton<IGrandNodeService, GrandNodeService>();

        // auto mapper registiration
        services.AddAutoMapper(typeof(Program).Assembly);
    })
    .Build();

await host.RunAsync();
