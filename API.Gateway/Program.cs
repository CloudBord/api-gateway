using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;
using Ocelot.Provider.Kubernetes;

var builder = WebApplication.CreateBuilder(args);

if(builder.Environment.EnvironmentName != "Production")
{
    builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });
}
else
{
    builder.Services.AddOcelot(builder.Configuration)
    .AddKubernetes()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });
}


builder.Services.AddCors();

builder.Configuration.AddJsonFile($"ocelot.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: false);

var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseWebSockets();

await app.UseOcelot();
await app.RunAsync();