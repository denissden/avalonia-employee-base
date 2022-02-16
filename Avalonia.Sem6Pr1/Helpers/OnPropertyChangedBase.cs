using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Sem6Pr1.Interfaces;
using JetBrains.Annotations;

namespace Avalonia.Sem6Pr1.Helpers;

public class OnPropertyChangedBase : IOnPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}