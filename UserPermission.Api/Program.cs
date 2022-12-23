using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using UserPermission.Api.Services;
using UserPermission.Repository;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/Debug-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
    .WriteTo.File("logs/Info-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information)
    .WriteTo.File("logs/Error-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error)
    .MinimumLevel.Debug()
    .CreateBootstrapLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = Environment.GetEnvironmentVariable("DBCONNECTION")!;

builder.Services.ConfigureCors();
builder.Services.InjectCustomService();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

builder.Services.AddDbContext<UserPermissionContext>(opt => opt.UseSqlServer(connectionString: connection, sqlServerOptionsAction: op =>
{
    op.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    Environment.SetEnvironmentVariable("URLKAFKA", "localhost:9092");
    Environment.SetEnvironmentVariable("ELASTICSEARCH", "localhost:9200");
    Environment.SetEnvironmentVariable("TOPIC", "operations");
    Environment.SetEnvironmentVariable("DBCONNECTION", @"Server=db;Database=Permission;User=sa;Password=12345;");
}

app.UseSwagger();
app.UseSwaggerUI();


using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<UserPermissionContext>();
    dataContext.Database.Migrate();
}

app.UResponseMiddleware();
app.UseCors("corsapp");
app.UseHttpsRedirection();
app.UseAuthorization();
app.ConfigureExceptionHandler();
app.MapControllers();

app.Run();

public partial class Program { }