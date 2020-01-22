using System;
using System.Reflection;

namespace Noppes.E621.Extensions
{
    /// <summary>
    /// Adds extra functionality to the enum values.
    /// </summary>
    internal static class EnumExtensions
    {
        public static string ToApiParameter(this Enum value)
        {
            var enumType = value.GetType();
            var enumName = Enum.GetName(enumType, value);
            var enumMembers = enumType.GetMember(enumName);

            var attribute = enumMembers[0].GetCustomAttribute<ParameterAttribute>();

            return attribute == null ? enumName : attribute.Value;
        }
    }
}
