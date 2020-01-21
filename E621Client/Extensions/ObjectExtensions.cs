using System.Collections.Generic;
using System.Linq;

namespace Noppes.E621.Extensions
{
    /// <summary>
    /// Adds extra functionality to the <see cref="object"/> class.
    /// </summary>
    internal static class ObjectExtensions
    {
        /// <summary>
        /// Gets all the public properties from the given object, retrieves their
        /// name and value and puts them into a dictionary.
        /// </summary>
        public static IDictionary<string, object> ToDictionary(this object target)
        {
            return target.GetType()
                .GetProperties()
                .ToDictionary(p => p.Name, p => p.GetValue(target));
        }
    }
}
