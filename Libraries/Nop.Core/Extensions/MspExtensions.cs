using System;
using System.Text.RegularExpressions;
/**
* @subject : Extension methods
* @author  : William Ho
* @date    : 14 Jan, 2019
*/
namespace Nop.Core.Extensions
{
    /// <summary>
    /// Custom extensions
    /// </summary>
    public static class MspExtensions
    {
        /// <summary>
        /// Compares this String to another String, ignoring case considerations.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="anotherString"></param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string str, string anotherString)
        {
            return str.Equals(anotherString, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Returns true if, and only if, length() is 0.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string str)
        {
            return str.Length == 0 ? true : false;
        }

        /// <summary>
        /// Tells whether or not this string matches the given regular expression.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool Matches(this string str, string pattern)
        {
            return new Regex(pattern).IsMatch(str);
        }


    }
}
