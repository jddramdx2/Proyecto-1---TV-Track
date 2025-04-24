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
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// App
var app = builder.Build();
app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

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

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
