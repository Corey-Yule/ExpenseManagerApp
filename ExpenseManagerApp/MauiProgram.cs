using Microsoft.Extensions.Logging;
using ExpenseManagerApp.Database;
using System.IO;

namespace ExpenseManagerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp(sp => new App(sp)) // ✅ FIXED: pass IServiceProvider
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            // ✅ Register AppDatabase here
            builder.Services.AddSingleton<AppDatabase>(s =>
            {
                string dbPath = Path.Combine(FileSystem.AppDataDirectory, "expenses.db3");
                return new AppDatabase(dbPath);
            });

            return builder.Build();
        }
    }
}
