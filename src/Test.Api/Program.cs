using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Reec.Inspection;
using Reec.Inspection.Extensions;
using Reec.Inspection.SqlServer;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
string outputTem = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz } {RequestId,13} [{Level:u3}] {Message:lj} {Properties} {NewLine}{Exception}";

ConfigurationManager configuration = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;
var connection = configuration.GetConnectionString("Default");

Log.Logger = new LoggerConfiguration()
                 .Enrich.FromLogContext()
                 .WriteTo.Console(outputTemplate: outputTem)
                 .ReadFrom.Configuration(configuration)
                 .CreateBootstrapLogger();

builder.Host
    .UseSerilog((context, services, configuration) => configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console(outputTemplate: outputTem));

configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(o => o.AddPolicy("All", builder =>
    builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
));

builder.Services.AddReecException<DbContextSqlServer>(options =>
{
    options.UseSqlServer(connection,
         x => x.MigrationsAssembly(typeof(DbContextSqlServer).Namespace)
        );
}
, new ReecExceptionOptions
{
    ApplicationName = "Test",
    EnableMigrations = false,
    EnableProblemDetails = true    
}
);




try
{
    Log.Information("Inicio de aplicación Api Interno...");
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseReecException<DbContextSqlServer>();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
    Log.Information("Deteniendo limpiamente la aplicación Api Interno...");
}
catch (Exception ex)
{
    Log.Fatal(ex, "Se produjo una excepción no controlada durante el arranque...");
}
finally
{
    Log.CloseAndFlush();
}