﻿using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using ToDoApp.Services;

namespace ToDoApp
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
            builder.Services.AddMudServices();

            string directory = AppDomain.CurrentDomain.BaseDirectory;

            string filePath = Path.Combine(directory, "data.db");

            builder.Services.AddSingleton<IToDoService>(new ToDoService(filePath));


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
