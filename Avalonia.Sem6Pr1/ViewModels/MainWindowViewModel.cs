using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;
using Avalonia.Sem6Pr1.Models;
using ReactiveUI;

namespace Avalonia.Sem6Pr1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase, IScreen
    {
        public string Greeting => "Welcome to Avalonia!";
        public RoutingState Router { get; } = new RoutingState();
        public ReactiveCommand<Unit, IRoutableViewModel> GoEmployees { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoTasks { get; }
        public ReactiveCommand<Unit, IRoutableViewModel> GoDepartments { get; }
        
        public ReactiveCommand<Unit, IRoutableViewModel> GoClients { get; }
        
        public ReactiveCommand<Unit, IRoutableViewModel> GoApplications { get; }

        public MainWindowViewModel()
        {
            GoEmployees = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new TableEditViewModel<Employee>(this))
            );
            GoTasks = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new TableEditViewModel<Models.Task>(this))
            );
            GoDepartments = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new TableEditViewModel<Department>(this))
            );
            GoClients = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new TableEditViewModel<Client>(this))
            );
            GoApplications = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new TableEditViewModel<Models.Application>(this))
            );
        }
    }
}