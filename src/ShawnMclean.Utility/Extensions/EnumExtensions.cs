using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShawnMclean.Utility.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Get a list of enumerant of the enum
        /// </summary>
        /// <typeparam name="TEnum"></typeparam>
        /// <param name="enumObj"></param>
        /// <returns></returns>
        public static IList<TEnum> ToEnumList<TEnum>(this TEnum enumObj) where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum) throw new ArgumentException("An Enumeration type is required.", "enumObj");

            return (TEnum[])Enum.GetValues(typeof(TEnum));
        }
    }
}
