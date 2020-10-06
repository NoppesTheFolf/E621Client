using Noppes.E621.Extensions;
using System;

namespace Noppes.E621
{
    /// <summary>
    /// Allows you to define a custom parameter value on an enum value that can be retrieved using a
    /// call to <see cref="EnumExtensions.ToApiParameter"/> as a way of setting custom parameter
    /// values for enum types.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    internal class ParameterAttribute : Attribute
    {
        public string Value { get; set; }

        public ParameterAttribute(string value)
        {
            Value = value;
        }
    }
}
