using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace FBTS.Library.Common
{
    public static class StringUtilities
    {
        public static string Left(string param, int length)
        {
            //we start at 0 since we want to get the characters starting from the
            //left and with the specified lenght and assign it to a variable
            string result = param.Substring(0, length);
            //return the result of the operation
            return result;
        }

        public static string Right(string param, int length)
        {
            //start at the index based on the lenght of the sting minus
            //the specified lenght and assign it a variable
            string result = param.Substring(param.Length - length, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex, int length)
        {
            //start at the specified index in the string ang get N number of
            //characters depending on the lenght and assign it to a variable
            string result = param.Substring(startIndex, length);
            //return the result of the operation
            return result;
        }

        public static string Mid(string param, int startIndex)
        {
            //start at the specified index and return all characters after it
            //and assign it to a variable
            string result = param.Substring(startIndex);
            //return the result of the operation
            return result;
        }

        public static string JoinMyReaderData(SqlDataReader dr, string argsSeparator)
        {
            string selectedValues = "";
            int found = 0;

            while (dr.Read())
            {
                if (found == 0)
                {
                    selectedValues = dr[0].ToString().Trim();
                }
                else
                {
                    selectedValues += argsSeparator + dr[0].ToString().Trim();
                }
                found += 1;
            }

            return (selectedValues);
        }

        public static string JoinMyReaderData(SqlDataReader dr, string argsSeparator, string argsColumnName)
        {
            string selectedValues = "";
            int found = 0;

            while (dr.Read())
            {
                if (found == 0)
                {
                    selectedValues = dr[argsColumnName].ToString().Trim();
                }
                else
                {
                    selectedValues += argsSeparator + dr[argsColumnName].ToString().Trim();
                }
                found += 1;
            }

            return (selectedValues);
        }


        public static bool ContainsAny(this string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length == 0)
            {
                foreach (string value in values)
                {
                    if (str.Contains(value))
                        return true;
                }
            }

            return false;
        }
        public static bool ToBool(this string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                    return false;

                if (str.Trim().ToLower() == "true")
                    return true;
                else if (str.Trim().ToLower() == "false")
                    return false;

                if (str.Trim() == "0")
                    return false;
                else if (str.Trim() == "1")
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }
        }

        
        public static Int32 ToInt(this string str)
        {
            if (string.IsNullOrEmpty(str.ToTrimString())) return 0;
            Int32 num;
            if (Int32.TryParse(str.ToTrimString(), out num))
                return num;
            else
                return 0;
        }
        public static decimal ToDecimal(this string str,int decimalPoint)
        {
            if (string.IsNullOrEmpty(str.ToTrimString())) return 0;
            decimal num;
            if (decimal.TryParse(str.ToTrimString(), out num))
                return Math.Round(num, decimalPoint, MidpointRounding.AwayFromZero);
            else
                return 0;
        }
        /// <summary>
        ///     Returns an enumerable collection of the specified type containing the substrings in this instance that are
        ///     delimited by elements of a specified Char array
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="separator">
        ///     An array of Unicode characters that delimit the substrings in this instance, an empty array containing no
        ///     delimiters, or null.
        /// </param>
        /// <typeparam name="T">
        ///     The type of the elemnt to return in the collection, this type must implement IConvertible.
        /// </typeparam>
        /// <returns>
        ///     An enumerable collection whose elements contain the substrings in this instance that are delimited by one or more
        ///     characters in separator.
        /// </returns>
        public static IEnumerable<T> SplitTo<T>(this string str, params char[] separator) where T : IConvertible
        {
            foreach (string s in str.Split(separator, StringSplitOptions.None))
                yield return (T) Convert.ChangeType(s, typeof (T));
        }

        public static IEnumerable<T> SplitTo<T>(this string str, params string[] separator) where T : IConvertible
        {
            foreach (string s in str.Split(separator, StringSplitOptions.None))
                yield return (T)Convert.ChangeType(s, typeof(T));
        }
        /// <summary>
        ///     Determines whether a string has multiple occurrences of a particular character. May be helpful when parsing file
        ///     names, or ensuring a particular string has already contains a given character. This may be extended to use strings,
        ///     rather than a char.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="charToFind"></param>
        /// <returns></returns>
        public static bool HasMultipleInstancesOf(this string input, char charToFind)
        {
            if ((string.IsNullOrEmpty(input)) || (input.Length == 0) || (input.IndexOf(charToFind) == 0))
                return false;

            if (input.IndexOf(charToFind) != input.LastIndexOf(charToFind))
                return true;

            return false;
        }

        public static bool In(this string Value, params string[] ValuesToCompare)
        {
            bool IsIn = false;
            foreach (string ValueToCompare in ValuesToCompare)
            {
                if (Value == ValueToCompare)
                {
                    IsIn = true;
                }
            }
            return IsIn;
        }

        public static DateTime? ToDateTime(this string s)
        {
            DateTime dtr;
            bool tryDtr = DateTime.TryParse(s, out dtr);
            return (tryDtr) ? dtr : new DateTime?();
        }

        // Used when we want to completely remove HTML code and not encode it with XML entities.
        public static string StripHtml(this string input)
        {
            // Will this simple expression replace all tags???
            var tagsExpression = new Regex(@"</?.+?>");
            return tagsExpression.Replace(input, " ");
        }

        /// <summary>
        ///     Convert a input string to a byte array and compute the hash.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <returns>Hexadecimal string.</returns>
        public static string ToMd5Hash(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                byte[] originalBytes = Encoding.Default.GetBytes(value);
                byte[] encodedBytes = md5.ComputeHash(originalBytes);
                return BitConverter.ToString(encodedBytes).Replace("-", string.Empty);
            }
        }

        /// <summary>
        ///     Count all words in a given string
        /// </summary>
        /// <param name="input">string to begin with</param>
        /// <returns>int</returns>
        public static int WordCount(this string input)
        {
            int count = 0;
            // Exclude whitespaces, Tabs and line breaks
            var re = new Regex(@"[^\s]+");
            MatchCollection matches = re.Matches(input);
            count = matches.Count;
            return count;
        }

        public static string FormatFileSize(this long fileSize)
        {
            string[] suffix = {"bytes", "KB", "MB", "GB"};
            long j = 0;

            while (fileSize > 1024 && j < 4)
            {
                fileSize = fileSize/1024;
                j++;
            }
            return (fileSize + " " + suffix[j]);
        }
        public static string ToTrimString(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            try
            {
                value = value.Trim();
            }
            catch
            {
                return string.Empty;
            }
            return value;
        }
    }
}