using Web.Middlewares;
using Microsoft.AspNetCore.Authentication.Cookies;
using Data;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Web.Chat;

namespace Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
			ApplicationDbContext.ConnectionString = builder.Configuration.GetConnectionString("WebEducacionIt");
			// Add services to the container.
			builder.Services.AddControllersWithViews();

            builder.Services.AddSignalR();

            builder.Services.AddHttpClient("useApi", config =>
            {
                config.BaseAddress = new Uri(builder.Configuration["ServiceUrl:ApiUrl"]);
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme= CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, config =>
            {
                config.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.Redirect("https://localhost:7168");
                    return Task.CompletedTask;
                };
            }).AddGoogle(GoogleDefaults.AuthenticationScheme, option =>
            {
                option.ClientId = builder.Configuration["Authentication:Google:Client_id"];
                option.ClientSecret = builder.Configuration["Authentication:Google:Client_secret"];
                option.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
            });

            builder.Services.AddSession();

            builder.Services.AddAuthorization(option =>
            {
                option.AddPolicy("ADMINISTRADORES", policy =>
                {
                    policy.RequireRole("Administrador");
                });
                option.AddPolicy("USUARIOS", policy =>
                {
                    policy.RequireRole("Usuario");
                });
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

            app.UseSession();

            app.UseMiddleware<ExceptionMiddleware>();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=Login}/{id?}");

            app.MapHub<ChatHub>("/Chat");
            
            app.Run();
        }
    }
}