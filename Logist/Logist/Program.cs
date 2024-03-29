
using Logist.Data;
using Logist.Data.Usr;
using Logist.Interfaces;
using Logist.Common;
using Logist.Data.LogFolder;
using Logist.Data.MainData;
using Logist.Data.Pages;
using Logist.Settings;

using Microsoft.EntityFrameworkCore;

using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICtrlListname, CtrlListname>();
builder.Services.AddScoped<ICtrlLists, CtrlLists>();
builder.Services.AddScoped<AppStatus>();
builder.Services.AddScoped<CtrlUsers>();
builder.Services.AddScoped<ProgramLogin>();
builder.Services.AddScoped<UserConnectionData>();
builder.Services.AddScoped<CommonLoger>();
builder.Services.AddSingleton<PageSettings>();
builder.Services.AddScoped<CtrlLCust>();
builder.Services.AddScoped<CtrlPageSettings>();
builder.Services.AddSingleton<ICashDictionary, CashDictionary>();
builder.Services.AddSingleton<CtrlUserCond>();

builder.Services.AddDbContextFactory<AppFactory>(
        options => options.UseSqlServer("name=ConnectionStrings:WebApiDatabase"));

builder.Services.AddRazorPages();

builder.Services.AddMudServices(config =>
    {
        config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();
