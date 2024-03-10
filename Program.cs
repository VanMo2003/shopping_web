using Microsoft.EntityFrameworkCore;
using website_shopping.Models.Contexts;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<ShopContext>(option =>
        {
            string? connectString = builder.Configuration.GetConnectionString("SqlServer");
            option.UseSqlServer(connectString);
        });
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();


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


        app.MapControllers();

        app.MapAreaControllerRoute(
               name: "MyAreaAdmin",
               areaName: "Admin",
               pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

        // app.MapControllerRoute(
        // name: "MyArea",
        // pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


        app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}