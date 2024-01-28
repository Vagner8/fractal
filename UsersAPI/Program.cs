using Microsoft.EntityFrameworkCore;
using UsersAPI.Data;
using UsersAPI.Lib.ExceptionHandling;
using UsersAPI.Lib.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins("http://localhost:4200");
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<NotMatchMiddleware>();

ApplyMigration();

app.Run();

void ApplyMigration()
{
    using var scope = app.Services.CreateScope();
    var _db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    if (_db.Database.GetPendingMigrations().Any())
    {
        _db.Database.Migrate();
    }
}