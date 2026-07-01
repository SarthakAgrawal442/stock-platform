using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using StockPlatform.API.Database;
using StockPlatform.API.Repositories;
using StockPlatform.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IPythonAiService, PythonAiService>(client =>
    {
        client.BaseAddress = new Uri(builder.Configuration["PythonService:BaseUrl"] ?? "http://localhost:8000");
        client.Timeout = TimeSpan.FromSeconds(10);
    })
    .AddStandardResilienceHandler();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IFinancialRepository, FinancialRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IFinancialService, FinancialService>();

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact", policy =>
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference();

app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();