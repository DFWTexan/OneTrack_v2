using Microsoft.EntityFrameworkCore;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;
using OneTrak_v2.Server.Services.Email.Templates;
using OneTrak_v2.Services;

var builder = WebApplication.CreateBuilder(args);
// Connect to the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<ILicenseInfo, LicenseInfo>();
builder.Services.AddScoped<ITicklerMgmt, TicklerMgmt>();
builder.Services.AddScoped<IMiscService, MiscService>();
builder.Services.AddScoped<IUtilityHelpService, UtilityHelpService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDataContext>(opt =>
    opt.UseSqlServer(connectionString));

// Configure CORS to allow requests from localhost:4200
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost4200", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

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

// Use CORS with the specified policy
app.UseCors("AllowLocalhost4200");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
