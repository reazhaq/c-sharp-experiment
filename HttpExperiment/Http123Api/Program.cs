using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using System.Security.Cryptography.X509Certificates;

var certificate = CertificateLoader.LoadFromStoreCert("localhost", StoreName.My.ToString(), StoreLocation.CurrentUser, allowInvalid: false);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Configure the HTTP request pipeline.
builder.WebHost
    .UseKestrel()
    .UseQuic()
    .ConfigureKestrel((context, options) =>
    {
        options.ListenAnyIP(5001, listenOptions =>
        {
            listenOptions.Protocols = HttpProtocols.Http1AndHttp2AndHttp3;
            listenOptions.UseHttps(httpsOptions =>
            {
                httpsOptions.ServerCertificate = certificate;
            });
        });
    });

var app = builder.Build();

app.UseRouting();
app.MapGet("/", async http =>
{
    await http.Response.WriteAsync($"request was made using {http.Request.Protocol}");
});

await app.RunAsync();
