using System;

namespace Avalonia.Sem6Pr1.Helpers;

public class MemberConfiguration
{
    //public delegate bool? ValidatorDelegate(object o);
    public Func<object?, bool?>? Validator { get; set; }
    public Func<object?, string?>? ToStringConverter { get; set; }
    public Func<string?, object?>? FromStringConverter { get; set; }
    public string? Name { get; set; }
}