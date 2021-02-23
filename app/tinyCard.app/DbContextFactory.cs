using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Data;
using tinyCard.Core.Config.Extensions;

namespace tinyCard.app
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CardDbContext>
    {
        public CardDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{AppDomain.CurrentDomain.BaseDirectory}")
                .AddJsonFile("appsettings.json", false)
                .Build();

            var config = configuration.ReadAppConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<CardDbContext>();

            optionsBuilder.UseSqlServer(
                config.TinyCardConnectionString,
                options => {
                    options.MigrationsAssembly("tinyCard.app");
                });

            return new CardDbContext(optionsBuilder.Options);
        }
    }
}
