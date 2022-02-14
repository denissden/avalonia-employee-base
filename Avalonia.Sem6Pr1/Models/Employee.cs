using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Avalonia.Sem6Pr1.Attributes;
using Avalonia.Sem6Pr1.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace Avalonia.Sem6Pr1.Models;

public class Employee : IOnPropertyChanged
{
    [Key] 
    [NotEditable]
    public int Id { get; set; }
    [Required] 
    [Name("First name")]
    public string FirstName { get; set; } = "";
    [Name("Middle name")]
    public string MiddleName { get; set; } = "";
    [Required]
    [Name("Last name")]
    public string LastName { get; set; } = "";
    [Phone]
    [Required]
    [Name("Phone number")]
    public string PhoneNumber { get; set; } = "";
    [EmailAddress]
    [Required]
    [Name("Email")]
    public string EmailAddress { get; set; } = "";

    public event PropertyChangedEventHandler? PropertyChanged;

    [NotifyPropertyChangedInvocator]
    public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}