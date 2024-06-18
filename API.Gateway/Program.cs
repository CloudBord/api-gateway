using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;
using Ocelot.Provider.Kubernetes;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddOcelot(builder.Configuration)
//    .AddKubernetes()
//    .AddCacheManager(x =>
//    {
//        x.WithDictionaryHandle();
//    });

//builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Configuration.AddJsonFile($"ocelot.Development.json", optional: true, reloadOnChange: true);

var app = builder.Build();

//app.UseRouting();
//app.UseEndpoints(_ => { });

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();
app.UseWebSockets();
app.UseOcelot().Wait();

await app.RunAsync();