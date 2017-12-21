using System;
using System.Globalization;
using System.Text;

namespace FBTS.Library.Common
{
    public class Currency
    {
        /// <summary>
        ///     Function to format the currency value to Indian currency  format with comma
        /// </summary>
        /// <param name="argsAmount">Amount value to convert to Indian money format</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static string FormatIndian(decimal argsAmount)
        {
            string charMoney = argsAmount.ToString(CultureInfo.InvariantCulture);
            string[] splitVal = SplitDecimal(charMoney);
            string quotChar = splitVal[0];
            string remainingChar = splitVal[1];
            int lenStr = quotChar.Length;
            int index = 3;
            while (lenStr > index)
            {
                quotChar = quotChar.Insert((lenStr - index), ",");
                index = index + 2;
            }
            return quotChar + "." + remainingChar;
        }

        public static string[] SplitDecimal(string argsValue)
        {
            var splitted = new string[2];
            if (string.IsNullOrEmpty(argsValue))
            {
                argsValue = "0";
            }
            if (Convert.ToDouble(argsValue) - Convert.ToInt32(argsValue) != 0)
            {
                splitted = argsValue.Split('.');
            }
            else
            {
                splitted[0] = Convert.ToInt32(argsValue).ToString(CultureInfo.InvariantCulture);
                splitted[1] = "00";
            }
            if (!string.IsNullOrEmpty(splitted[0]))
            {
                if (!string.IsNullOrEmpty(splitted[1]))
                {
                }
                else
                {
                    splitted[1] = "00";
                }
            }
            else
            {
                splitted[0] = "0";
            }
            return splitted;
        }

        public static string RupeesToWords(decimal argsRupees)
        {
            string[] values = SplitDecimal(argsRupees.ToString(CultureInfo.InvariantCulture));

            string outputContent = ToWords(Convert.ToInt32(values[0]));

            if (Convert.ToInt32(values[1]) > 0)
            {
                outputContent += " and " + ToWords(Convert.ToInt32(values[1]));
            }

            return outputContent;
        }

        public static string ToWords(int inputNumber)
        {
            int inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            var numbers = new int[4];
            int first = 0;
            int u, h, t;
            var sb = new StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 =
            {
                "", "One ", "Two ", "Three ", "Four ",
                "Five ", "Six ", "Seven ", "Eight ", "Nine "
            };
            string[] words1 =
            {
                "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
                "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen "
            };
            string[] words2 =
            {
                "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
                "Seventy ", "Eighty ", "Ninety "
            };
            string[] words3 = {"Thousand ", "Lakh ", "Crore "};

            numbers[0] = inputNo%1000; // units
            numbers[1] = inputNo/1000;
            numbers[2] = inputNo/100000;
            numbers[1] = numbers[1] - 100*numbers[2]; // thousands
            numbers[3] = inputNo/10000000; // crores
            numbers[2] = numbers[2] - 100*numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i]%10; // ones
                t = numbers[i]/10;
                h = numbers[i]/100; // hundreds
                t = t - 10*h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }
    }
}