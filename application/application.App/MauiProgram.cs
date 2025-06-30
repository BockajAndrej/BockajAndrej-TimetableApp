using application.App.Pages.View;
using application.App.Pages.ViewModel;
using application.BL;
using application.BL.Facades;
using application.DAL;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace application.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            //Views
            builder.Services.AddTransient<MainPageView>();

            //ViewModels
            builder.Services.AddTransient<MainPageViewModel>();

            //Installers
            builder.Services
                .AddDalServices()
                .AddBlServices();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            var app = builder.Build();

            return app;
        }
    }
}
