using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;

var builder = WebApplication.CreateBuilder(args);
// Connect to the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IUtilityHelpService, UtilityHelpService>();
builder.Services.AddScoped<IMiscService, MiscService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDataContext>(opt =>
    opt.UseSqlServer(connectionString));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
