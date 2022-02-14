using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Avalonia.Sem6Pr1.Interfaces;
using JetBrains.Annotations;

namespace Avalonia.Sem6Pr1.Models;

public class Department : IOnPropertyChanged
{
    [Key]
    public int Id { get; set; }
    public string Address { get; set; }
    public List<Employee> Employees { get; set; }
    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}