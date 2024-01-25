using Final_Exam.DAL;
using Final_Exam.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(opt=>opt.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;

    opt.User.RequireUniqueEmail = true;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


var app = builder.Build();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();


app.MapControllerRoute(

    "default",
    "{area:exists}/{controller=Home}/{action=Index}/{id?}"

    );
app.MapControllerRoute(
    
    "default",
    "{controller=Home}/{action=Index}/{id?}"

    );


app.Run();
