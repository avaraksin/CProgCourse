
using Logist.Data;
using Logist.Data.Usr;
using Logist.Interfaces;
using Logist.Common;
using Microsoft.EntityFrameworkCore;

using MudBlazor.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ICtrlListname, CtrlListname>();
builder.Services.AddScoped<ICtrlLists, CtrlLists>();
builder.Services.AddScoped<AppStatus>();
builder.Services.AddScoped<CtrlUsers>();

builder.Services.AddDbContextFactory<AppFactory>(
        options => options.UseSqlServer("name=ConnectionStrings:WebApiDatabase"));

//builder.Services.AddDbContext<ApplicationDbContext>(
//        options => options.UseSqlServer("name=ConnectionStrings:WebApiDatabase"));

builder.Services.AddRazorPages();

builder.Services.AddMudServices();

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
