
using FinalWorkDentistry.Abstract;
using FinalWorkDentistry.AutoMapping;

using FinalWorkDentistry.DataAccessLayer;
using FinalWorkDentistry.Domains;
using FinalWorkDentistry.Models;

using FinalWorkDentistry.UsersRoles;
using FinalWorkDentistry.ServerHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Smart.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;



namespace FinalWorkDentistry
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
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = false;
                options.Cookie.IsEssential = false; //---------
            });

            // services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();

            // Добавляем DbContext с EnableRetryOnFailure
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    sqlOptions => sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5, // Максимальное количество повторных попыток
                        maxRetryDelay: TimeSpan.FromSeconds(10), // Максимальная задержка между попытками
                        errorNumbersToAdd: null // Дополнительные коды ошибок для повторения
                    )
                )
            );

            services
                .AddDefaultIdentity<ApplicationUser>(o => {
                    o.Password.RequiredLength = 3;
                    o.Password.RequireNonAlphanumeric = false;
                    o.Password.RequiredUniqueChars = 0;
                    o.Password.RequireDigit = false;
                    o.Password.RequireLowercase = false;
                    o.Password.RequireUppercase = false;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddMvc();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" });
            });

            services.AddTransient<IRepository<Service>, ServiceSqlRepository>();

            // services.AddTransient<DoctorModel>();
            // services.AddTransient<CategoryDoctor>();

            // services.AddTransient<DoctorBriefModel>();

            services.AddTransient<IRepository<Doctor>, DoctorSqlRepository>();
            services.AddTransient<IRepository<Reviews>, ReviewsSqlRepository>();
            services.AddTransient<IRepository<Request>, RequestSqlRepository>();
            services.AddTransient<IRepository<CategoryService>, CategoryUslugiSqlRepository>();
            services.AddTransient<IRepository<CategoryDoctor>, CategoryMedicSqlRepository>();

            services.AddSignalR();

            services.AddSmart();

            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            // services.AddTransient<BlazorCart>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.





        public void Configure(
            IApplicationBuilder app,
            IWebHostEnvironment env,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                    app.UseDatabaseErrorPage();
                }
                else
                {
                    app.UseExceptionHandler("/Home/Error");
                    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                    app.UseHsts();
                }
                app.UseHttpsRedirection();
                app.UseStaticFiles();
                app.UseSession();
            app.Use(async delegate (HttpContext Context, Func<Task> Next)
            {
                //this throwaway session variable will "prime" the SetString() method
                //to allow it to be called after the response has started
                var TempKey = Guid.NewGuid().ToString(); //create a random key
                Context.Session.Set(TempKey, System.Array.Empty<byte>()); //set the throwaway session variable
                Context.Session.Remove(TempKey); //remove the throwaway session variable
                await Next(); //continue on with the request
            });
            app.UseRouting();

                app.UseAuthentication();
                app.UseAuthorization();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Blazor}/{action=IndexMain}/{id?}");

                    endpoints.MapRazorPages();
                });

                ModelsMapping.InitializeMappingService();
                ModelsMapping.InitializeMappingDoctor();

                using (var scope = app.ApplicationServices.CreateScope())
                {
                    DataSeeder.SeedServices(scope.ServiceProvider);
                    DataSeeder.SeedRoles(roleManager);
                    DataSeeder.SeedUsers(userManager);
                }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapHub<ChatHub>("/chathub");
            });
        }
        }
    } 

