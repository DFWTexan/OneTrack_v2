using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OneTrack_v2.DbData;
using OneTrack_v2.Services;
using OneTrak_v2.Server.Services.Email.Templates;
using OneTrak_v2.Services;

var builder = WebApplication.CreateBuilder(args);

// Connect to the database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddScoped<ILdapService, LdapService>();
builder.Services.AddScoped<IAgentService, AgentService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IEmailTemplateService, EmailTemplateService>();
builder.Services.AddScoped<ILicenseInfoService, LicenseInfoService>();
builder.Services.AddScoped<ITicklerMgmtService, TicklerMgmtService>();
builder.Services.AddScoped<IMiscService, MiscService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IUtilityHelpService, UtilityHelpService>();
builder.Services.AddScoped<IWorklistService, WorklistService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IDocumentService, DocumentService>();

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDataContext>(opt =>
    opt.UseSqlServer(connectionString));

// Configure CORS to allow requests from specific origins
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200",
                           "http://localhost:5000",
                           "http://ftwebd201",
                           "http://ftwebq201",
                           "http://ftwebp201")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "OneTrakV2 API", Version = "v1" });
});

var app = builder.Build();

app.UseDefaultFiles();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "OneTrakV2 API V1");
        c.RoutePrefix = "swagger"; // Set Swagger UI at /swagger
    });
}

app.UseStaticFiles();
app.UseRouting();

// Use CORS with the specified policy
app.UseCors("AllowSpecificOrigins");

app.UseAuthorization();

app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();