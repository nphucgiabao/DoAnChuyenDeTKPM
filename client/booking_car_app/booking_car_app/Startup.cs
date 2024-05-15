using booking_car_app.ApiServices.Booking;
using booking_car_app.ApiServices.User;
using booking_car_app.HttpHandlers;
using booking_car_app.Middlewares;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace booking_car_app
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IBookingService, BookingService>();
            
            services.AddTransient<AuthenticationDelegatingHandler>();            

            services.AddHttpClient("BookingCarAPIClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44308/"); // API GATEWAY URL
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");            
            }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();

            services.AddHttpClient("IDPClient", client =>
            {
                client.BaseAddress = new Uri("https://localhost:44342/");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
            }).AddHttpMessageHandler<AuthenticationDelegatingHandler>();
            services.AddHttpContextAccessor();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
    .AddCookie("Cookies")
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:44342";

        options.ClientId = "appBookingCarMVC";
        options.ClientSecret = "secret";
        options.ResponseType = "code";

        //options.GetClaimsFromUserInfoEndpoint = true;
        options.Scope.Add("openid");
        options.Scope.Add("profile");
        options.Scope.Add("booking_car_api");
        options.SaveTokens = true;
        options.GetClaimsFromUserInfoEndpoint = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = JwtClaimTypes.GivenName,
            RoleClaimType = JwtClaimTypes.Role
        };
    });
            services.AddAutoMapper(typeof(Startup));
            
            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            //})
            //    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            //    {
            //        options.Authority = "https://localhost:44342";

            //        options.ClientId = "appBookingCarMVC";
            //        options.ClientSecret = "secret";
            //        options.ResponseType = "code";

            //        options.Scope.Add("openid");
            //        options.Scope.Add("profile");
            //        //options.Scope.Add("address");
            //        //options.Scope.Add("email");
            //        //options.Scope.Add("roles");

            //        //options.ClaimActions.DeleteClaim("sid");
            //        //options.ClaimActions.DeleteClaim("idp");
            //        //options.ClaimActions.DeleteClaim("s_hash");
            //        //options.ClaimActions.DeleteClaim("auth_time");
            //        //options.ClaimActions.MapUniqueJsonKey("role", "role");

            //        options.Scope.Add("booking_car_api");

            //        options.SaveTokens = true;
            //        options.GetClaimsFromUserInfoEndpoint = true;

            //        options.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            NameClaimType = JwtClaimTypes.GivenName,
            //            RoleClaimType = JwtClaimTypes.Role
            //        };
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
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

            app.UseMiddleware<UserMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
         
        }
    }
}
