using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Avalonia.Sem6Pr1.Attributes;
using Avalonia.Sem6Pr1.Helpers;
using Avalonia.Sem6Pr1.Interfaces;
using JetBrains.Annotations;

namespace Avalonia.Sem6Pr1.Models;

public class Department : OnPropertyChangedBase
{
    [Key]
    [NotEditable]
    public int Id { get; set; }
    public string Address { get; set; }
}