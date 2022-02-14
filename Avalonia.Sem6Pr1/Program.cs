using System;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Avalonia.Sem6Pr1.Database;
using Avalonia.Sem6Pr1.Helpers;
using Avalonia.Sem6Pr1.Models;
using Avalonia.Sem6Pr1.ViewModels;
using Avalonia.Sem6Pr1.Views;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;
using AppContext = Avalonia.Sem6Pr1.Database.AppContext;

namespace Avalonia.Sem6Pr1
{
    class Program
    {
        // Initialization code. Don't use any Avalonia, third-party APIs or any
        // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
        // yet and stuff might break.
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        // Avalonia configuration, don't remove; also used by visual designer.
        public static AppBuilder BuildAvaloniaApp()
        {
            // views
            Locator.CurrentMutable.Register(() => new TableEditView(), typeof(IViewFor<TableEditViewModel<Employee>>));
            
            // database service
            string conn = "Host=localhost;Database=avalonia_sem6pr1;Username=user;Password=password";
            Locator.CurrentMutable.RegisterLazySingleton(() => new AppContext(conn), typeof(DbContext));
            Locator.CurrentMutable.RegisterLazySingleton(() => new TableService(), typeof(TableService));
            
            // validation
            var emailRegex = new Regex("^.+@.+[.].+");
            var phoneRegex = new Regex("^[0-9]*$");

            var modelsConfiguration = new ModelsConfiguration(
                ModelConfiguration.Create<Employee>(c =>
                c
                    .ForMember(e => e.EmailAddress, o => o.Validate(addr => emailRegex.IsMatch(addr)))
                    .ForMember(e => e.PhoneNumber, o => o.Validate(p => phoneRegex.IsMatch(p)))
                    .ForMember(e => e.FirstName, o => o.ConvertToString(s => s + "1"))
                )
                );
            Locator.CurrentMutable.RegisterConstant(modelsConfiguration, typeof(ModelsConfiguration));

            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
        }
    }
}