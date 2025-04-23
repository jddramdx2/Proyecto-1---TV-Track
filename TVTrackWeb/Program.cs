using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TVTrackWeb.Components;
using TVTrackWeb.Services;
using static System.Net.Mime.MediaTypeNames;
using System.Threading.Tasks;
using System;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<SesionService>();
builder.Services.AddSingleton<Neo4jService>();

var neo = new Neo4jService();
await neo.ImportarPeliculasDesdeCSV("listado_100_peliculas.csv");


// App
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();

app.MapBlazorHub(); // << ESTA LÍNEA ES CLAVE
app.MapFallbackToPage("/_Host"); // Asegurate de tener una página _Host.cshtml

app.Run();
