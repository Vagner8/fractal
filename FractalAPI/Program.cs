using FractalAPI.Data;
using FractalAPI.Services;
using FractalAPI.Services.ControlService;
using FractalAPI.Services.FractalService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(option =>
{
  option.AddPolicy(name: MyAllowSpecificOrigins, policy =>
  {
    policy.WithOrigins("http://localhost:4200")
      .AllowAnyHeader()
      .AllowAnyMethod();
  });
});
builder.Services.AddDbContext<AppDbContext>(option =>
{
  option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
  options.JsonSerializerOptions.WriteIndented = true;
  options.JsonSerializerOptions.MaxDepth = 32;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ExceptionService>();
builder.Services.AddProblemDetails();
builder.Services.AddScoped<IFractalService, FractalService>();
builder.Services.AddScoped<IControlService, ControlService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();
app.UseExceptionHandler();
app.MapControllers();
app.Run();

public partial class Program { }