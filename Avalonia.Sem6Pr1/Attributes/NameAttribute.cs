using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Avalonia.Sem6Pr1.Attributes;

[System.AttributeUsage(System.AttributeTargets.Property)]
public class NameAttribute : Attribute
{
    public string Name { get; set; }

    public NameAttribute(string name) => Name = name;
}