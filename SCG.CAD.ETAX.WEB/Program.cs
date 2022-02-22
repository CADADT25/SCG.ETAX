global using ClosedXML.Excel;
global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using SCG.CAD.ETAX.MODEL;
global using SCG.CAD.ETAX.UTILITY;
global using System.Text;
global using SCG.CAD.ETAX.MODEL.etaxModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
