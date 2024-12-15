using Blazored.LocalStorage;
using ContatosApp.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace ContatosApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            var dbPath = Path.Combine(
                FileSystem.AppDataDirectory,
                "contatos.db3"
            );
            builder.Services.AddSingleton(
                new DatabaseService(dbPath)
            );

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddMudServices();
            builder.Services.AddSweetAlert2();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}