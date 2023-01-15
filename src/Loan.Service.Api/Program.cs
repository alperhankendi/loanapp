using Loan.Domain.Repository.Persistence;
using Loan.Service.Api.Modules.LoanApplication;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders().AddSimpleConsole(i => { i.ColorBehavior = Microsoft.Extensions.Logging.Console.LoggerColorBehavior.Enabled;i.SingleLine = true; });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApiServices();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHealthChecks("/health");
app.UseMiddleware<CustomErrorHandlingMiddleware>();
app.UseHttpsRedirection();
app.MapGet("/healthy", () =>{return "OK";}).WithName("GetWeatherForecast").WithOpenApi();
app.MapControllers().WithTags("Loan Application");

app.Run();
public partial class Program{ }
