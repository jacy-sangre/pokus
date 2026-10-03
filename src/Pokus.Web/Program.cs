using Pokus.Web.Components;
using Pokus.Web.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// In-memory services for the prototype (one shared user, reset on restart). A database replaces these later.
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddSingleton<ISettingsService, InMemorySettingsService>();
builder.Services.AddSingleton<ITechniqueService, InMemoryTechniqueService>();
builder.Services.AddSingleton<IFocusService, InMemoryFocusService>();
builder.Services.AddSingleton<IDeckService, InMemoryDeckService>();
builder.Services.AddSingleton<IMessageService, InMemoryMessageService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
