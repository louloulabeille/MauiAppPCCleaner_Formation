using CommunityToolkit.Maui;
using MauiAppPCCleaner_Formation.Infrastructure.ExtendMethod;
using MauiAppPCCleaner_Formation.ViewModels;
using Microsoft.Extensions.Logging;

namespace MauiAppPCCleaner_Formation
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                // Initialize the .NET MAUI Community Toolkit by adding the below line of code
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // -- ajout de la configuration à partir du fichier config.json
            builder.Configuration.AddAppsettingsConfiguration();
            // -- ajout avec le design pattern options pour injecter la configuration dans les viewmodels
            builder.Services.AddConfigJson(builder.Configuration);

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // -- ajout par injection de dépendance des viewmodels et des pages
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<MainViewModel>();

            // -- ajout d'un system de sauvegarde 
            

            return builder.Build();
        }
    }
}
