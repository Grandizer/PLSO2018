﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PLSO2018.Website.Services;
using DataContext;
using PLSO2018.Entities;
using PLSO2018.Entities.Common;
using Microsoft.Extensions.Logging;
using DataContext.Support;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PLSO2018.Entities.Services;
using DataContext.Repositories;
using PLSO2018.Controllers;

namespace PLSO2018.Website {

	public class Startup {

		private ILogger logger;
		private string SendGridKey = "";

		public Startup(IConfiguration configuration, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			Configuration = configuration;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();

			logger = loggerFactory.CreateLogger<Startup>();

			if (env.IsEnvironment(Constants.Applicaton.LocalEnvironment)) {
				builder.AddUserSecrets<Startup>();
			}
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services) {
			services.AddDbContext<PLSODb>(options =>
				options.UseSqlServer(
					Configuration.GetConnectionString("PLSOData"),
					sql => {
						sql.EnableRetryOnFailure(); // https://stackoverflow.com/questions/40985683/ef-core-and-sqlazureexecutionstrategy
					}
				));

			// TODO: Add Repositories and Factories here
			//services.AddScoped(typeof(ACalendarFactory));
			//services.AddScoped(typeof(BillingRatesRepo));
			//services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.AddScoped(typeof(ExcelController));

			services.AddIdentity<ApplicationUser, ApplicationRole>(config => {
				config.SignIn.RequireConfirmedEmail = true;
				})
				.AddEntityFrameworkStores<PLSODb>()
				.AddDefaultTokenProviders(); // Necessary for generating tokens for "Forgot Password"

			services.AddScoped<SignInManager<ApplicationUser>, PLSOSignInManager<ApplicationUser>>();
			services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

			services.Configure<IdentityOptions>(options => {
				// Password settings
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 8;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = true;
				options.Password.RequireLowercase = false;
				options.Password.RequiredUniqueChars = 6;

				// Lockout settings
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
				options.Lockout.MaxFailedAccessAttempts = 10;
				options.Lockout.AllowedForNewUsers = true;

				// User settings
				options.User.RequireUniqueEmail = true;
			});


			services.ConfigureApplicationCookie(options => {
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
				// If the LoginPath isn't set, ASP.NET Core defaults 
				// the path to /Account/Login.
				options.LoginPath = "/Account/Login";
				// If the AccessDeniedPath isn't set, ASP.NET Core defaults 
				// the path to /Account/AccessDenied.
				options.AccessDeniedPath = "/Account/AccessDenied";
				options.SlidingExpiration = true;
			});

			// Add application services.
			services.AddSingleton<IEmailSender, EmailSender>();
			//services.Configure<AuthMessageSenderOptions>(Configuration);
			//services.Configure<AuthMessageSenderOptions>(Configuration.GetSection("SendGrid"));
				
				
			// Add Repositories here
			services.AddScoped(typeof(ExcelTemplateRepo));

			services.AddMvc();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory) {
			loggerFactory.AddConsole(Configuration.GetSection("Logging")); // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging
			loggerFactory.AddDebug();
			loggerFactory.AddContext(LogLevel.Warning, Configuration.GetConnectionString("PLSOData")); // Should just save Warning, Error and Critical messages to the Database

			if (env.IsDevelopment()) {
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			} else {
				app.UseExceptionHandler("/Home/Error");
			}

			app.UseStaticFiles();
			app.UseAuthentication();

			app.UseMvc(routes => {
				routes.MapRoute(
						name: "default",
						template: "{controller=Home}/{action=Index}/{id?}");
			});

			// So we can create instances of DI items when you can not inject them naturally
			ServiceInstantiator.Instance = app.ApplicationServices;
		}

	}
}
