﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PLSO2018.Entities.Services;
using System;
using System.IO;

namespace DataContext
{
    public abstract class DesignTimeDbContextFactoryBase<TContext> :
            IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        public TContext CreateDbContext(string[] args)
        {
            return Create(
                Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        protected abstract TContext CreateNewInstance(
            DbContextOptions<TContext> options);

        public TContext Create()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = AppContext.BaseDirectory;

            return Create(basePath, environmentName);
        }

        private TContext Create(string basePath, string environmentName)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();
            var connstr = config.GetConnectionString("default");

            if (String.IsNullOrWhiteSpace(connstr) == true) {
                throw new InvalidOperationException(
                    "Could not find a connection string named 'default'.");
            } else {
                return Create(connstr);
            }
        }

        private TContext Create(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException(
             $"{nameof(connectionString)} is null or empty.",
             nameof(connectionString));

            var optionsBuilder =
                 new DbContextOptionsBuilder<TContext>();

            Console.WriteLine(
                "MyDesignTimeDbContextFactory.Create(string): Connection string: {0}",
                connectionString);

            optionsBuilder.UseSqlServer(connectionString);

            DbContextOptions<TContext> options = optionsBuilder.Options;

            return CreateNewInstance(options);
        }
    }


    public class PLSODbContextDesignTimeDbContextFactory :
        DesignTimeDbContextFactoryBase<PLSODb>
    {

        private UserResolver userResolver = null;
        private ILoggerFactory loggerFactory = null;

        public PLSODbContextDesignTimeDbContextFactory(UserResolver userResolver, ILoggerFactory loggerFactory)
        {
            this.userResolver = userResolver;
            this.loggerFactory = loggerFactory;
        }

        protected override PLSODb CreateNewInstance(
            DbContextOptions<PLSODb> options)
        {
            return new PLSODb(options, userResolver, loggerFactory);
        }

    }
}
