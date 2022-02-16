using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Avalonia.Sem6Pr1.Attributes;
using Avalonia.Sem6Pr1.Helpers;
using Avalonia.Sem6Pr1.Interfaces;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace Avalonia.Sem6Pr1.Models;

public class Client : OnPropertyChangedBase
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
}