using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using TVTrackWeb.Components;
using TVTrackWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// ⬇️ Registrar servicios de sesión y Neo4j
builder.Services.AddSingleton<SesionService>();
builder.Services.AddSingleton<Neo4jService>();

// ⬇️ Agregar componentes interactivos de Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// ⬇️ Configuración para manejo de errores y archivos estáticos
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAntiforgery();

// ⬇️ Renderizar el componente raíz
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
