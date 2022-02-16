using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Avalonia.Sem6Pr1.Attributes;
using Avalonia.Sem6Pr1.Helpers;

namespace Avalonia.Sem6Pr1.Models;

public class Application : OnPropertyChangedBase
{
    [Key]
    [NotEditable]
    public int Id { get; set; }
    public string Description { get; set; }
    
    [ForeignKey(nameof(Client))]
    [Name("Created by client (id)")]
    public int ClientId { get; set; }
    [ForeignKey(nameof(Models.Task))]
    [Name("Assigned task (id)")]
    public int TaskId { get; set; }
}