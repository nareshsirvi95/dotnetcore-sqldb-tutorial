using DotNetCoreSqlDb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddAzureWebAppDiagnostics();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<MyDatabaseContext>(options =>
                    options.UsesqlServer(builder.configuration.getconnectionstring("AZURE_POSTGRESQL_CONNECTIONSTRING")));

builder.Services.addstackexchangerediscache(options=>
{
    options.configuration=builder.configuration["AZURE_REDIS_CONNECTIONSTRING"];
    options.instancename="SampleInstance";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todos}/{action=Index}/{id?}");

app.Run();
