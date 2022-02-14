using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Avalonia.Sem6Pr1.Interfaces;

public interface IOnPropertyChanged : INotifyPropertyChanged
{
    public abstract void OnPropertyChanged([CallerMemberName] string? propertyName = null);
}