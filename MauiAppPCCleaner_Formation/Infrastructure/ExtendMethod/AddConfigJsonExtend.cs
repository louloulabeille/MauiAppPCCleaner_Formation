using MauiAppPCCleaner_Formation.Infrastructure.System;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MauiAppPCCleaner_Formation.Infrastructure.ExtendMethod
{
    public static class AddConfigJsonExtend
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddConfigJson(IConfiguration configuration)
            {
                services.Configure<Config>(configuration.GetSection("Config"));
                return services;
            }
        }

        /// <summary>
        /// // -- méthode d'extension pour ajouter la configuration à partir du fichier config.json
        /// // -- ne marche pas pour le moment - impossible d'installer le package Microsoft.Extensions.Configuration.Json dans un projet MAUI
        /// </summary>
        /// <param name="builder"></param>
        extension(IConfigurationBuilder builder)
        {
            public IConfigurationBuilder AddAppsettingsConfiguration()
            {
                using var stream = FileSystem.OpenAppPackageFileAsync("config.json").Result;

                var config = new ConfigurationBuilder().AddJsonStream(stream).Build();

                builder.AddConfiguration(config);

                return builder;
            }
        }

    }
}
