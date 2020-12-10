namespace SKAutoNew
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity.UI.Services;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using SKAutoNew.Common;
    using SKAutoNew.Data;
    using SKAutoNew.Data.Models;
    using SKAutoNew.Data.Repositories;
    using SKAutoNew.Services.Data;
    using SKAutoNew.Services.Data.Contractcs;
    using SKAutoNew.Services.Messaging;
    using System;
    using System.Linq;

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
            services.AddDbContext<SKAutoDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<SKAutoDbContext>();

            services
                .AddMvc()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                });

            services
                .ConfigureApplicationCookie(options =>
                {
                    options.LoginPath = "/Identity/Account/Login";
                    options.LogoutPath = "/Identity/Account/Logout";
                    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                });

            services
                .Configure<CookiePolicyOptions>(options =>
                {
                    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                    options.CheckConsentNeeded = context => true;
                    options.MinimumSameSitePolicy = SameSiteMode.Lax;
                    options.ConsentCookie.Name = ".AspNetCore.ConsentCookie";
                });

            services.AddControllersWithViews(
                options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });
            services.AddRazorPages();

            // cart
            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            // Data repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            // Application services
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IBrandService, BrandService>();
            services.AddTransient<IModelService, ModelService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IPartService, PartService>();
            services.AddTransient<ITownService, TownService>();
            services.AddTransient<IUseFullCategoryService, UseFullCategoryService>();
            services.AddTransient<ICompanyService, CompanySrvice>();
            services.AddTransient<IRecipientService, RecipientService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IManufactoryService, ManufactoryService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IOrderStatusService, OrderStatusService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<SKAutoDbContext>())
                {
                    if (env.IsDevelopment())
                    {
                        dbContext.Database.Migrate();
                    }

                    if (!dbContext.OrderStatuses.Any())
                    {
                        dbContext.OrderStatuses.Add(new OrderStatus { Name = GlobalConstants.PendingBGStatus });
                        dbContext.OrderStatuses.Add(new OrderStatus { Name = GlobalConstants.ShippedBGStatus });
                        dbContext.OrderStatuses.Add(new OrderStatus { Name = GlobalConstants.DeliverBGStatus });
                        dbContext.OrderStatuses.Add(new OrderStatus { Name = GlobalConstants.AcquiredBGStatus });
                    }

                    if (!dbContext.Roles.Any())
                    {
                        dbContext.Roles.Add(new ApplicationRole 
                        {
                            Name = GlobalConstants.AdministratorRoleName, 
                            NormalizedName = GlobalConstants.AdministratorRoleName.ToUpper()
                        });
                        dbContext.Roles.Add(new ApplicationRole 
                        { 
                            Name = GlobalConstants.UserRoleName,
                            NormalizedName = GlobalConstants.UserRoleName.ToUpper()
                        });
                    }

                    dbContext.SaveChanges();
                }
            }


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
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // cart
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
