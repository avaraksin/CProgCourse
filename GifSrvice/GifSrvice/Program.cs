using GifSrvice.BussinessLogik;
using GifSrvice.Controllers;
using GifSrvice.Interface;
using Microsoft.Extensions.Configuration;
using GifSrvice.Data;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

//builder.Services.AddDbContext<>();

builder.Services.AddScoped<ICurrencyRates, CurrencyRates>();
builder.Services.AddScoped<IGif, Gif>();

builder.Services.AddHttpContextAccessor();

//builder.Services.AddSingleton<ICurrencyRates, CurrencyRates>();
//builder.Services.AddSingleton<IGif, Gif>();



//var confBuilder = new ConfigurationBuilder().AddJsonFile("json.json"); 
//IConfiguration AppConfiguration = confBuilder.Build();
builder.Services.Configure<GifServiceSettings>(builder.Configuration.GetSection("GifServiceSettings"));
builder.Services.Configure<Currency>(builder.Configuration.GetSection("Currency"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



app.Run();
