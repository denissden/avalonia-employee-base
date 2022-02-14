using System;

namespace Avalonia.Sem6Pr1.Attributes;

[System.AttributeUsage(System.AttributeTargets.Property)]
public class NotEditableAttribute : Attribute
{
    public NotEditableAttribute() {}
}