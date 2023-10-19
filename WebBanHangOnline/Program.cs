using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Policy;
using WebBanHangOnline.Models;
using WebBanHangOnline.Repository;

        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var connectionString = builder.Configuration.GetConnectionString("QlbanHangContext");
        builder.Services.AddDbContext<QlbanHangContext>(x => x.UseSqlServer(connectionString));

        builder.Services.AddScoped<ILoaiSpRepository, LoaiSpRepository>();
        builder.Services.AddSession();

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
        app.UseSession();


        app.MapControllerRoute(
           name: "ShopSanPham",
           pattern: "{controller=Product}/{action=shop}/{id?}");


//app.MapControllerRoute(
//            name: "default",
//            pattern: "{controller=Account}/{action=DangNhap}/{id?}"); //dang nhap truoc moi vao dc trang admin

//app.Run();


app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Access}/{action=Login}/{id?}"); //dang nhap truoc moi vao dc trang ban hang

        app.Run();
