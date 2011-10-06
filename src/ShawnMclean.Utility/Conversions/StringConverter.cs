using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ShawnMclean.Utility.Conversions
{
    public static class StringConverter
    {

        /// <summary>
        /// Uses reflection to convert object property to a string.
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static string PropertyToString(Expression selector)
        {
            switch (selector.NodeType)
            {
                case ExpressionType.MemberAccess:
                    return ((selector as MemberExpression).Member as PropertyInfo).Name;
                    break;

                case ExpressionType.Convert:
                    return (((selector as UnaryExpression).Operand as MemberExpression).Member as PropertyInfo).Name;

                    break;
            }
            throw new InvalidOperationException();
        }
    }
}
