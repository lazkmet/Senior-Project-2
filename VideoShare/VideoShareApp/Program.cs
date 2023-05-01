using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using VideoShareData.Models;
using System.Configuration;
using Microsoft.AspNetCore.Hosting.Server;
using Syncfusion.Blazor;
using System.Drawing.Text;
using VideoShareData.Services;
using BlazorBootstrap;
using System.Security.Claims;
using VideoShareData.Enums;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Components.Authorization;
using VideoShareApp.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddBlazorBootstrap();
builder.Services.AddDbContextFactory<WebAppDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("VideoShare"));
});
builder.Services.AddScoped<ProtectedSessionStorage>();
builder.Services.AddScoped<AuthenticationStateProvider, UserAuthenticationProvider>();
builder.Services.AddScoped<IUserService, UserService>();
//Set up needed components for Authentication
SetupAuthentication(builder.Services);


//Register Syncfusion
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetValue<string>("Syncfusion"));

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

void SetupAuthentication(IServiceCollection services)
{
    services.AddAuthenticationCore();
    services.AddAuthorization(options => {
        options.AddPolicy("AdminOnly", policy => policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString()));
    });
}