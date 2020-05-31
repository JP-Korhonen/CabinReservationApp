using CommonModels;
using DinkToPdf;
using DinkToPdf.Contracts;
using FrontEnd.Areas.Identity.Data;
using FrontEnd.Data;
using FrontEnd.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FrontEnd
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var restApiConfigSection = Configuration.GetSection("RestApiConfig");
            RestApiConfig config = restApiConfigSection.Get<RestApiConfig>();
            services.AddSingleton(config);

            var jwtConfigSection = Configuration.GetSection("JwtSettings");
            JWTConfig jWTConfig = jwtConfigSection.Get<JWTConfig>();
            jWTConfig.CreateSecurityKey();
            services.AddSingleton(jWTConfig);

            // https://code-maze.com/create-pdf-dotnetcore/
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-3.1#basic-usage
            services.AddHttpClient();
            services.AddScoped<ServiceRepository>();

            services.AddDbContext<FrontEndContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString")));

            services.AddDefaultIdentity<FrontEndUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<FrontEndContext>();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<FrontEndUser> userManager, RoleManager<IdentityRole> roleManager)
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

            // Adding roles and admin if not exists
            IdentityDataInitializer.SeedData(userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
