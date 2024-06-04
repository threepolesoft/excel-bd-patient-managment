using API.DbContexts;
using API.Repository;
using API.Repository.Interface;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IToken, TokenBusiness>();
builder.Services.AddScoped<IUserService, UserServiceBusiness>();
builder.Services.AddScoped<INCD, NCDBusiness>();
builder.Services.AddScoped<IAllergies, AllergiesBusiness>();
builder.Services.AddScoped<IPatient, PatientBusiness>();
builder.Services.AddScoped<INCDDetails, NCDDetailsBusiness>();
builder.Services.AddScoped<IAllergiesDetails, AllergiesDetailsBusiness>();

IConfiguration configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection_Dev");


if (connectionString == null)
{
    throw new ApplicationException("DefaultConnection is not set");
}

builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));


// Register HttpClient with custom certificate validation
//builder.Services.AddHttpClient("MySecureClient")
//    .ConfigurePrimaryHttpMessageHandler(() =>
//    {
//        var handler = new HttpClientHandler();
//        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
//        {
//            return ValidateServerCertificate(cert, chain, errors);
//        };
//        return handler;
//    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.InitializeDatabaseAsync().ConfigureAwait(false);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static bool ValidateServerCertificate(X509Certificate2 certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
{
    if (sslPolicyErrors == SslPolicyErrors.None)
        return true;

    // Custom certificate validation logic here
    var trustedThumbprint = "YOUR_TRUSTED_CERTIFICATE_THUMBPRINT";
    return certificate.Thumbprint.Equals(trustedThumbprint, StringComparison.OrdinalIgnoreCase);
}