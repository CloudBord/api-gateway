using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);


var app = builder.Build();

//app.UseRouting();
//app.UseEndpoints(_ => { });

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();
app.UseWebSockets();
app.UseOcelot().Wait();

await app.RunAsync();