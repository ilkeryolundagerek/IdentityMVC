using IdentityMVC.Data;
using IdentityMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<UserContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("identity")));

builder.Services
    .AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<UserContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailSender, EMailSender>(o => new EMailSender(
    builder.Configuration["EmailOptions:From"],
    builder.Configuration["EmailOptions:Username"],
    builder.Configuration["EmailOptions:Password"],
    builder.Configuration.GetValue<int>("EmailOptions:Port"),
    builder.Configuration["EmailOptions:Host"],
    builder.Configuration.GetValue<bool>("EmailOptions:SSL")
    ));

builder.Services.ConfigureApplicationCookie(o =>
{
    o.LoginPath=new PathString("/identity/account/login");
    o.LogoutPath=new PathString("/identity/account/logout");
    o.AccessDeniedPath=new PathString("/identity/account/accessdenied");
});

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

app.MapRazorPages();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
