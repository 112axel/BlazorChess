using BlazorChess.Hubs;
using BlazorChess.Server.Data;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

string enviorment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

Console.WriteLine(enviorment);

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"appsettings.{enviorment}.json", optional: true)
    .Build();
string conectionString = "";
if(enviorment == "Development")
{
    conectionString = configuration.GetConnectionString("MyDbContext");
}
else
{
    conectionString = "host=postgres;port=5432;database=ChessDb;username=test;password=1234;Pooling=true;";
}

builder.Services.AddDbContext<ChessContext>(options =>
{
    options.UseNpgsql(conectionString);
});

builder.Services.AddSignalR();


var app = builder.Build();

app.MapHub<GameHub>("/GameHub");
app.MapHub<BoardHub>("/BoardHub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ChessContext>();
    if (dataContext.Database.GetPendingMigrations().Any())
    {
        dataContext.Database.Migrate();
    }
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
