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
        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;
        
        public MainWindowViewModel()
        {
            GoNext = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute((TableEditViewModel)new TableEditViewModel<Employee>(this))
            );
        }
    }
}