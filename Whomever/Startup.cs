using Whomever.Data;
using Whomever.Areas.ContactUsService;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

//using Microsoft.AspNetCore.Authentication.AzureAD.UI;

namespace Whomever
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
            //services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
            //    .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            //services.AddControllers();
            services.AddDbContext<ApplicationDbContext>(options =>
       options.UseSqlServer(
           Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ApplicationSeeder>();
            services.AddTransient<IContactUsService, NullContactUsService>();
            //reused thru scope
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            //for testing
            //services.AddScoped<IApplicationRepository, MockApplicationRepository>();
            services.AddControllersWithViews()
              .AddRazorRuntimeCompilation()
              .AddNewtonsoftJson(cfg => cfg.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
            services.AddRazorPages();
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
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseEndpoints(cfg =>
            {
                cfg.MapControllerRoute("Fallback",
            "{controller}/{action}/{id?}",
            new { controller = "Home", action = "Index" });
                cfg.MapRazorPages();
            });
        }
    }
}