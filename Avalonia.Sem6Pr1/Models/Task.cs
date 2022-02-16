using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using Avalonia.Sem6Pr1.Attributes;
using Avalonia.Sem6Pr1.Helpers;
using Avalonia.Sem6Pr1.Interfaces;
using JetBrains.Annotations;

namespace Avalonia.Sem6Pr1.Models;

public class Task : OnPropertyChangedBase
{
    [Key]
    [NotEditable]
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string Description { get; set; }
    [ForeignKey(nameof(Employee))]
    public int Responsible { get; set; }
}