using ATMApp.Data;
using ATMApp.Data.Repositories;
using ATMApp.Domain;
using ATMApp.Domain.BankAccount;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddEnvironmentVariables(prefix: "ATM_");
builder.Services.Configure<AppSettings>(builder.Configuration);

var logger = LoggerFactory.Create(config =>
{
    config.AddConsole();
    config.AddDebug();
    config.AddConfiguration(builder.Configuration.GetSection("Logging"));
}).CreateLogger("Program");

var appSettings = builder.Configuration.Get<AppSettings>();
logger.LogInformation("appsettings = {0}", appSettings);

if (appSettings == null)
    throw new ArgumentNullException("AppSettings is null");

if (string.IsNullOrWhiteSpace(appSettings.connectionStrings.SqlConnectionString))
    throw new ArgumentNullException("SQL connection string is required");

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(appSettings.connectionStrings.SqlConnectionString),
    ServiceLifetime.Singleton);

// Add services to the container.
builder.Services.AddScoped<IValidator<EntityKind>, EntityKindValidator>();
builder.Services.AddScoped<IValidator<Account>, AccountValidator>();
builder.Services.AddScoped<IValidator<Transaction>, TransactionValidator>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(isOriginAllowed: _ => true)
        .AllowCredentials()
        );
});

var app = builder.Build();

try
{
    using var scope = app.Services.CreateScope();
    var databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    databaseContext.Database.SetConnectionString(appSettings.connectionStrings.SqlConnectionString);
    databaseContext.Database.Migrate();
}
catch (Exception ex)
{
    logger.LogError(ex, "There was problem running DB migrations. Error: {0}", ex.Message);
    throw;
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
