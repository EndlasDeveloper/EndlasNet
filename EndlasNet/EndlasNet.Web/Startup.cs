using EndlasNet.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json.Serialization;

namespace EndlasNet.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            // Add the application context for database managment using ef core
            services.AddDbContext<EndlasNetDbContext>(options => options
            .UseSqlServer(Configuration.GetConnectionString("endlas_local"))); // db ref in appsettings.json
            services.AddRazorPages();
            services.AddSession();
            services.AddMvcCore(); 
            services.AddCors();
            services.AddControllers().AddJsonOptions(options => {
                // Use the default property (Pascal) casing.
                options.JsonSerializerOptions.PropertyNamingPolicy =
                JsonNamingPolicy.CamelCase;
            });
            services.AddScoped<UserRepo>();
            services.AddScoped<IEmployeeRepo, EmployeeRepo>();
            services.AddScoped<IAdminRepo, AdminRepo>();
            services.AddScoped<IVendorRepo, VendorRepo>();
            services.AddScoped<ICustomerRepo, CustomerRepo>();
            services.AddScoped<IJobRepo, JobRepo>();
            services.AddScoped<IWorkRepo, WorkRepo>();
            services.AddScoped<IWorkOrderRepo, WorkOrderRepo>();
            services.AddScoped<IStaticPartInfoRepo, StaticPartInfoRepo>();
            services.AddScoped<IWorkRepo, WorkRepo>();
            services.AddScoped<IWorkItemRepo, WorkItemRepo>();
            services.AddScoped<IPartForJobRepo, PartForJobRepo>();
            services.AddScoped<IPartForWorkRepo, PartForWorkRepo>();
            services.AddScoped<IPartForWorkOrderRepo, PartForWorkOrderRepo>();
            services.AddScoped<IQuoteRepo, QuoteRepo>();
            services.AddScoped<IPowderOrderRepo, PowderOrderRepo>();
            services.AddScoped<ILineItemRepo, LineItemRepo>();
            services.AddScoped<IPowderBottleRepo, PowderBottleRepo>();
            services.AddScoped<IMachiningToolRepo, MachiningToolRepo>();
            services.AddScoped<IStaticPowderInfoRepo, StaticPowderInfoRepo>();
            services.AddScoped<IMachiningToolForWorkRepo, MachiningToolForWorkRepo>();
            services.AddScoped<IPowderForPartRepo, PowderForPartRepo>();

            // session time to 2 hours
            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromHours(2);
                o.SlidingExpiration = true;
            });
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
            app.UseSession();
            app.UseAuthorization();
            app.UseCors(options =>
                options.WithOrigins("http://localhost:3000")
                .AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}