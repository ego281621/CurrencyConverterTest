using CurrencyConverterTest.Core.Entities;
using CurrencyConverterTest.Repository.RequestLogRepository;
using CurrencyConverterTest.Repository.UserRepository;
using CurrencyConverterTest.Services.CurrencyService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.Development.json", optional: true);
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddScoped<IRequestLogRepository, RequestLogRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();

builder.Services.AddDbContext<CurrencyTestDbContext>
    (options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddCors();

//// configure basic authentication 
builder.Services.AddAuthentication("BasicAuthentication").
    AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>
    ("BasicAuthentication", null);

builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<LoggingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.UseCors(x => x
             .AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader());

app.Run();
