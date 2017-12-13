﻿using FoodReport.DAL.Data;
using FoodReport.DAL.Interfaces;
using FoodReport.DAL.Models;
using FoodReport.DAL.Repos;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using FoodReport.Models;
using FoodReport.BLL.Interfaces;
using FoodReport.BLL.Services;
using FoodReport.BLL.Models;

namespace FoodReport
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
            services.AddMvc();
            services.Configure<Settings>(options =>
            {
                options.ConnectionString
                    = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                options.Database
                    = Configuration.GetSection("MongoConnection:Database").Value;
            });
            services.AddTransient<IProductRepository, ProductRepo>();
            services.AddTransient<IReportRepository, ReportRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISearchProduct, SearchProductService>();
            services.AddTransient<ISearchReport, SearchReportService>();
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IStatusReportService, StatusReportService>();
            services.AddTransient<ISummaryReport<SummaryModel>, SummaryReportService>();

            services.AddTransient<IRoleRepo, RoleRepo>();
            services.AddSingleton<IPasswordHasher, PasswordHashService>();


            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
               .AddCookie(options => //CookieAuthenticationOptions
                {
                   options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
               });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<Settings> options, IPasswordHasher passwordHasher)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Report}/{action=Index}/{id?}");
            });
            var Db = new InitMongoDbService(options, passwordHasher);
            Db.Init();
        }
    }
}
