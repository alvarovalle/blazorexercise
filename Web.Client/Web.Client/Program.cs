using Microsoft.EntityFrameworkCore;
//using MongoDB.Driver.Core.Configuration;
using RepositorySQL;
using Services;
using Web.Client.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//var LocalConnection = builder.Configuration.GetConnectionString("MongoConnection");
//builder.Services.AddDbContext<Context>(options => options.UseMongoDB(LocalConnection!, "local"));

var LocalConnection = builder.Configuration.GetConnectionString("SQLConnection");

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(LocalConnection));

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
