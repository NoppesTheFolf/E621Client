using System;
using System.IO;

namespace Noppes.E621.Extensions
{
    /// <summary>
    /// Provides extra functionality to strings.
    /// </summary>
    internal static class StringExtensions
    {
        public static string GetPathExtensionWithoutDot(this string value)
        {
            string? extensionWithDot = Path.GetExtension(value);

            if (extensionWithDot == null)
                throw new ArgumentException($"{nameof(value)} is not a path with an extension.", nameof(value));

            return extensionWithDot.Substring(1, extensionWithDot.Length - 1);
        }
    }
}
