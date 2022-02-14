using System.ComponentModel;
using Avalonia.Sem6Pr1.Interfaces;

namespace Avalonia.Sem6Pr1.Models;

public class Dummy : IOnPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged(string? propertyName = null)
    {
        throw new System.NotImplementedException();
    }
}