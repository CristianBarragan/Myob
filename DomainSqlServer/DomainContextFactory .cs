using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DomainSqlServer
{
    public class DomainContextFactory : IDbContextFactory<DomainModelContext>
    {

        public DomainModelContext Create(DbContextFactoryOptions options)
        {

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfigurationRoot Configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<DomainModelContext>();
            var sqlConnectionString = Configuration["DbStringLocalizer:ConnectionString"];
            optionsBuilder.UseSqlServer("Data Source=ASUS1\\ADMIN;initial catalog=MyObDatabase;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework", b => b.MigrationsAssembly(typeof(DomainModelContext).GetTypeInfo().FullName));
            return new DomainModelContext(optionsBuilder.Options);
        }
    }
}
