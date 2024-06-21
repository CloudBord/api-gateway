using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;
using Ocelot.Provider.Kubernetes;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot(builder.Configuration)
    //.AddKubernetes()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    });

builder.Services.AddCors();

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);


var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

//app.UseRouting();
//app.UseEndpoints(_ => { });

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();
app.UseWebSockets();

await app.UseOcelot();
await app.RunAsync();