using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinyCard.Core.Config;
using tinyCard.Core.Config.Extensions;
using tinyCard.Core.Data;

namespace tinyCard.Core.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAppServices(
            this IServiceCollection @this, IConfiguration configuration)
        {
            @this.AddSingleton<AppConfig>(
                configuration.ReadAppConfiguration());

            @this.AddDbContext<CardDbContext>(
                 (serviceProvider, optionsBuilder) => {
                     var appConfig = serviceProvider.GetRequiredService<AppConfig>();

                     optionsBuilder.UseSqlServer(appConfig.TinyCardConnectionString);
                 });

            @this.AddScoped<ICardService, CardService>();
        }
    }
}
