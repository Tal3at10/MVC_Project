using Demo.BLL.Services;
using Demo.BLL.Services.Departments;
using Demo.BLL.Services.Employees;
using Demo.DAL.Presistance.Reposateries.Departments;
using Demo.DAL.Presistance.Reposateries.Employees;
using Demo.DAL.Presistence.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NoteKeeperPro.Application.Common.Services.EmailSettings;
using NoteKeeperPro.Infrastructure.Identity;

namespace Demo.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>((options)=>
            {
                options.UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
            });
            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddScoped<IEmailSettings, EmailSettings>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Options =>
            {
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireDigit = true;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequiredLength = 5;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); // PasswrdSignInAsync depends on AddDefaultTokenProviders

            // UserManager , RoleManager, SigningManager

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie
                (Options =>
                {
                    Options.LoginPath = "/Account/Login";
                    Options.AccessDeniedPath = "/Home/Error";
                    Options.LogoutPath = "/Account/Login";
                }

                );


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
            app.UseAuthentication(); // Order Matters Authentication before Authorization
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Register}/{id?}");

            app.Run();
        }
    }
}

