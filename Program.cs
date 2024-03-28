using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using website_shopping.Models;
using website_shopping.Models.Contexts;
using website_shopping.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //! Add services to the container.
        builder.Services.AddDbContext<ShopContext>(option =>
        {
            string? connectString = builder.Configuration.GetConnectionString("SqlServer");
            option.UseSqlServer(connectString);
        });
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        //! đăng ký identity
        builder.Services.AddDefaultIdentity<UserModel>()
                        .AddEntityFrameworkStores<ShopContext>()
                        .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            //! Thiết lập về Password
            options.Password.RequireDigit = false;              //* không bắt buộc phải có s
            options.Password.RequireLowercase = false;          //* chữ thường
            options.Password.RequireNonAlphanumeric = false;    //* ký tự đặc biệt
            options.Password.RequireUppercase = false;          //* chữ in hoa
            options.Password.RequiredLength = 3;                //* số ký tự tối thiểu của password
            options.Password.RequiredUniqueChars = 1;           //* số ký tự đặc biệt

            //! Cấu hình Lockout - khóa user
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);   //* Khóa 5p
            options.Lockout.MaxFailedAccessAttempts = 5;                        //* thất bại tối đa 5 lần
            options.Lockout.AllowedForNewUsers = true;

            //! Cấu hình về User
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; //* các ký tự đặt tên user
            options.User.RequireUniqueEmail = true; //* Email là duy nhất

            //! Cấu hình đăng nhập
            options.SignIn.RequireConfirmedEmail = false;       //* Cấu hình xác thực địa chỉ email (phải xác thực email mới đăng nhập lại được) 
            options.SignIn.RequireConfirmedPhoneNumber = false; //* Xác thực số điện thoại
        });

        builder.Services.AddOptions();
        var mailSettings = builder.Configuration.GetSection("MailSettings");
        builder.Services.Configure<MailSettings>(mailSettings);
        builder.Services.AddSingleton<SendMailService>();

        // builder.Services.ConfigureApplicationCookie(options =>
        // {
        //     options.LoginPath = "/login/";
        //     options.LogoutPath = "/logout/";
        //     options.AccessDeniedPath = "/khongduoctruycap.html";
        // });

        // builder.Services.AddAuthentication()
        //                 .AddGoogle(options =>
        //                 {
        //                     var gConfig = builder.Configuration.GetSection("Authentication:Google");
        //                     options.ClientId = gConfig["ClientId"];
        //                     options.ClientSecret = gConfig["ClientSecret"];
        //                     options.CallbackPath = "/dang-nhap-google";
        //                 })
        //                 .AddFacebook(options =>
        //                 {
        //                     var fConfig = builder.Configuration.GetSection("Authentication:Facebook");
        //                     options.AppId = fConfig["AppId"];
        //                     options.AppSecret = fConfig["AppSecret"];
        //                     options.CallbackPath = "/dang-nhap-facebook";
        //                 });


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

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        // app.MapAreaControllerRoute(
        //        name: "MyAreaAdmin",
        //        areaName: "Admin",
        //        pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
        name: "MyArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}