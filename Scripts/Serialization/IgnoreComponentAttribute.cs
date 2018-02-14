using System;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
class IgnoreComponentAttribute : Attribute
{
}
