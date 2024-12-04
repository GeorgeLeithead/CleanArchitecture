using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureMauiAspire;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var mauiAppBuilder = MauiApp.CreateBuilder();

        mauiAppBuilder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        #if DEBUG
        mauiAppBuilder.Configuration.AddInMemoryCollection(AspireAppSettings.Settings);
#endif
        
        mauiAppBuilder.AddAppDefaults();

        #if DEBUG
        mauiAppBuilder.Logging.AddDebug();
#endif
        
        var scheme = mauiAppBuilder.Configuration.IsDevelopment() ? "http" : "https";
        mauiAppBuilder.Services.AddHttpClient<WeatherApiClient>(client => client.BaseAddress = new($"{scheme}://apiservice"));
        mauiAppBuilder.Services.AddSingleton<MainPage>();

        return mauiAppBuilder.Build();
    }
}
