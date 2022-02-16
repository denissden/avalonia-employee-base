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
using Task = System.Threading.Tasks.Task;

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
            AddTableEdit<Employee>();
            AddTableEdit<Models.Task>();
            AddTableEdit<Department>();
            AddTableEdit<Client>();
            AddTableEdit<Models.Application>();
            
            // database service
            string conn = "Host=localhost;Database=avalonia_sem6pr1;Username=user;Password=password";
            Locator.CurrentMutable.RegisterLazySingleton(() => new AppContext(conn), typeof(DbContext));
            Locator.CurrentMutable.RegisterLazySingleton(() => new TableService(), typeof(TableService));
            
            // validation
            InitValidation();

            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI();
        }

        public static void InitValidation()
        {
            var emailRegex = new Regex("^.+@.+[.].+");
            var phoneRegex = new Regex("^[0-9]{10}$");
            var numberRegex = new Regex("^[0-9]*$");

            var modelsConfiguration = new ModelsConfiguration(
                ModelConfiguration.Create<Employee>(c =>
                    c
                        .ForMember(e => e.FirstName, o => o.Validate(s => !string.IsNullOrWhiteSpace(s)))
                        .ForMember(e => e.LastName, o => o.Validate(s => !string.IsNullOrWhiteSpace(s)))
                        .ForMember(e => e.EmailAddress, o => o.Validate(addr => emailRegex.IsMatch(addr ?? "")))
                        .ForMember(e => e.PhoneNumber, o => o.Validate(p => phoneRegex.IsMatch(p ?? "")))
                        .ForMember(e => e.DepartmentId, o => o.ConvertFromString(Convert.ToInt32).ValidateNotNull())
                ),
                ModelConfiguration.Create<Models.Task>(c =>
                    c
                        .ForMember(e => e.Responsible, o => o.ConvertFromString(Convert.ToInt32).ValidateNotNull())
                ),
                ModelConfiguration.Create<Department>(c =>
                    c
                        .ForMember(e => e.Address, o => o.Validate(s => !string.IsNullOrWhiteSpace(s)))
                ),
                ModelConfiguration.Create<Client>(c =>
                    c
                        .ForMember(e => e.FirstName, o => o.Validate(s => !string.IsNullOrWhiteSpace(s)))
                        .ForMember(e => e.LastName, o => o.Validate(s => !string.IsNullOrWhiteSpace(s)))
                        .ForMember(e => e.EmailAddress, o => o.Validate(addr => emailRegex.IsMatch(addr ?? "")))
                        .ForMember(e => e.PhoneNumber, o => o.Validate(p => phoneRegex.IsMatch(p ?? "")))
                ),
                ModelConfiguration.Create<Models.Application>(c =>
                    c
                        .ForMember(e => e.Description, o => o.Validate(s => !string.IsNullOrWhiteSpace(s)))
                        .ForMember(e => e.ClientId, o => o.ConvertFromString(Convert.ToInt32).ValidateNotNull())
                        .ForMember(e => e.TaskId, o => o.ConvertFromString(Convert.ToInt32).ValidateNotNull())
                )
            );
            Locator.CurrentMutable.RegisterConstant(modelsConfiguration, typeof(ModelsConfiguration));
        }
        
        private static void AddTableEdit<TModel>() where TModel : class, new()
        {
            Locator.CurrentMutable.Register(() => new TableEditView(), typeof(IViewFor<TableEditViewModel<TModel>>));
        }
    }
}