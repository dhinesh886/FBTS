using System;
using System.Collections.Generic;
using System.Linq;

namespace FBTS.Library.Common
{
    public static class Dates
    {
        public static string ToFriendlyDateString(this DateTime date)
        {
            string formattedDate;
            if (date.Date == DateTime.Today)
            {
                formattedDate = "Today";
            }
            else if (date.Date == DateTime.Today.AddDays(-1))
            {
                formattedDate = "Yesterday";
            }
            else if (date.Date > DateTime.Today.AddDays(-6))
            {
                // *** Show the Day of the week
                formattedDate = date.ToString("dddd");
            }
            else
            {
                formattedDate = date.ToString("MMMM dd, yyyy");
            }

            //append the time portion to the output
            formattedDate += " @ " + date.ToString("t").ToLower();
            return formattedDate;
        }

        public static IEnumerable<DateTime> GetDateRangeTo(this DateTime self, DateTime toDate)
        {
            IEnumerable<int> range = Enumerable.Range(0, new TimeSpan(toDate.Ticks - self.Ticks).Days);

            return from p in range
                select self.Date.AddDays(p);
        }

        /// <summary>
        ///     Converts a System.DateTime object to Unix timestamp
        /// </summary>
        /// <returns>The Unix timestamp</returns>
        public static long ToUnixTimestamp(this DateTime date)
        {
            var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            TimeSpan unixTimeSpan = date - unixEpoch;

            return (long) unixTimeSpan.TotalSeconds;
        }

        public static bool Between<T>(this T value, T from, T to) where T : IComparable<T>
        {
            return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
        }

        public static bool IsInRange(this DateTime currentDate, DateTime beginDate, DateTime endDate)
        {
            return (currentDate >= beginDate && currentDate <= endDate);
        }

        public static bool IsLeapYear(this DateTime value)
        {
            return (DateTime.DaysInMonth(value.Year, 2) == 29);
        }

        public static DateTime GetLastDayInMonth(DateTime argsDate)
        {
            return new DateTime(argsDate.Year, argsDate.Month, DateTime.DaysInMonth(argsDate.Year, argsDate.Month));
        }

        public static int GetQuarterName(DateTime argsDate)
        {
            return (int) Math.Ceiling(argsDate.Month/3.0);
        }

        public static DateTime GetQuarterStartDate(DateTime argsDate)
        {
            return new DateTime(argsDate.Year, 3*GetQuarterName(argsDate) - 2, 1);
        }

        public static DateTime GetQuarterEndDate(DateTime argsDate)
        {
            return new DateTime(argsDate.Year, 3*GetQuarterName(argsDate) + 1, 1).AddDays(-1);
        }

        public static string FormatDate(DateTime argsDate, string outputFormat)
        {
            if (argsDate == DateTime.MinValue) argsDate = new DateTime(1900, 01, 01);
            return argsDate.ToString(outputFormat);
        }

        public static string FormatDate(string argsDate, DateFormat argsOutputDateFormat)
        {
            string inputDate = argsDate.Trim();
            string outputFormat;
            DateTime date;
            try
            {
                if (!string.IsNullOrEmpty(inputDate))
                {
                    switch (argsOutputDateFormat)
                    {
                        case DateFormat.Format_01:
                        {
                            outputFormat = "yyyy-MM-dd";
                            //Input Format[DD/MM/YYYY]  
                            //Output Format[YYYY-MM-DD]                       
                            int[] dtarry = inputDate.Split('/').Select(n => Convert.ToInt32(n)).ToArray();
                            date = new DateTime(dtarry[2], dtarry[1], dtarry[0]);
                            break;
                        }
                        case DateFormat.Format_02:
                        {
                            outputFormat = "dd/MM/yyyy";
                            //Input Format[YYYY-MM-DD] 
                            //Output Format[DD/MM/YYYY] 
                            int[] dtarry = inputDate.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
                            date = new DateTime(dtarry[0], dtarry[1], dtarry[2]);
                            break;
                        }
                        case DateFormat.Format_03:
                        {
                            outputFormat = "dd/MM/yyyy";
                            //Input Format[MM/DD/YYYY 12:00:00 AM]
                            //Output Format[DD/MM/YYYY] 				       
                            date = Convert.ToDateTime(inputDate);
                            break;
                        }
                        case DateFormat.Format_04:
                        {
                            outputFormat = "dd-MM-yyyy";
                            //Input Format[MM/DD/YYYY 12:00:00 AM]
                            //Output Format[DD-MM-YYYY] 
                            date = Convert.ToDateTime(inputDate);
                            break;
                        }
                        case DateFormat.Format_05:
                        {
                            outputFormat = "yyyy-MM-dd";
                            //Input Format[MM/DD/YYYY 12:00:00 AM]
                            //Output Format[YYYY-MM-DD] 
                            date = Convert.ToDateTime(inputDate);
                            break;
                        }
                        case DateFormat.Format_06:
                        {
                            outputFormat = "MM/dd/yyyy";
                            //Input Format[DD/MM/YYYY]
                            //Output Format[MM/DD/YYYY]
                            int[] dtarry = inputDate.Split('/').Select(n => Convert.ToInt32(n)).ToArray();
                            date = new DateTime(dtarry[2], dtarry[1], dtarry[0]);
                            break;
                        }
                        case DateFormat.Format_07:
                        {
                            outputFormat = "MM/dd/yyyy";
                            //Input Format[YYYY-MM-DD]
                            //Output Format[MM/DD/YYYY] 
                            int[] dtarry = inputDate.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
                            date = new DateTime(dtarry[0], dtarry[1], dtarry[2]);
                            break;
                        }
                        default:
                        {
                            outputFormat = "yyyy-MM-dd";
                            //Output Format[YYYY-MM-DD]                        
                            date = new DateTime(1900, 01, 01);
                            break;
                        }
                    }
                }
                else
                {
                    outputFormat = "yyyy-MM-dd";
                    //Output Format[YYYY-MM-DD]                        
                    date = new DateTime(1900, 01, 01);
                }
            }
            catch
            {
                outputFormat = "yyyy-MM-dd";
                //Output Format[YYYY-MM-DD]                        
                date = new DateTime(1900, 01, 01);
            }
            return date.ToString(outputFormat);
        }
        public static DateTime ToDateTime(string argsDate, DateFormat argsInputDateFormat)
        {
            if (!string.IsNullOrEmpty(argsDate))
            {
                string inputDate = argsDate.Trim();
                DateTime date;
                try
                {
                    if (!string.IsNullOrEmpty(inputDate))
                    {
                        switch (argsInputDateFormat)
                        {
                            case DateFormat.Format_01:
                            {
                                //Input Format[DD/MM/YYYY]  
                                //Output Format[YYYY-MM-DD]                       
                                int[] dtarry = inputDate.Split('/').Select(n => Convert.ToInt32(n)).ToArray();
                                date = new DateTime(dtarry[2], dtarry[1], dtarry[0]);
                                break;
                            }
                            case DateFormat.Format_02:
                            {
                                //Input Format[YYYY-MM-DD] 
                                //Output Format[DD/MM/YYYY] 
                                int[] dtarry = inputDate.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
                                date = new DateTime(dtarry[0], dtarry[1], dtarry[2]);
                                break;
                            }
                            case DateFormat.Format_03:
                            {
                                //Input Format[MM/DD/YYYY 12:00:00 AM]
                                //Output Format[DD/MM/YYYY] 				       
                                date = Convert.ToDateTime(inputDate);
                                break;
                            }
                            case DateFormat.Format_04:
                            {
                                //Input Format[MM/DD/YYYY 12:00:00 AM]
                                //Output Format[DD-MM-YYYY] 
                                date = Convert.ToDateTime(inputDate);
                                break;
                            }
                            case DateFormat.Format_05:
                            {
                                //Input Format[MM/DD/YYYY 12:00:00 AM]
                                //Output Format[YYYY-MM-DD] 
                                date = Convert.ToDateTime(inputDate);
                                break;
                            }
                            case DateFormat.Format_06:
                            {
                                //Input Format[DD/MM/YYYY]
                                //Output Format[MM/DD/YYYY]
                                int[] dtarry = inputDate.Split('/').Select(n => Convert.ToInt32(n)).ToArray();
                                date = new DateTime(dtarry[2], dtarry[1], dtarry[0]);
                                break;
                            }
                            case DateFormat.Format_07:
                            {
                                //Input Format[YYYY-MM-DD]
                                //Output Format[MM/DD/YYYY] 
                                int[] dtarry = inputDate.Split('-').Select(n => Convert.ToInt32(n)).ToArray();
                                date = new DateTime(dtarry[0], dtarry[1], dtarry[2]);
                                break;
                            }
                            default:
                            {
                                //Output Format[YYYY-MM-DD]                        
                                date = new DateTime(1900, 01, 01);
                                break;
                            }
                        }
                    }
                    else
                    {
                        //Output Format[YYYY-MM-DD]                        
                        date = new DateTime(1900, 01, 01);
                    }
                }
                catch
                {
                    //Output Format[YYYY-MM-DD]                        
                    date = new DateTime(1900, 01, 01);
                }
                return date;
            }
            else
            {
                //return DateTime.Now;
                return new DateTime(1900, 01, 01);
            }
        }
        public static int GetDateDifference(DateTime argsFromDate, DateTime argsToDate)
        {
            TimeSpan ts = argsFromDate.Subtract(argsToDate);
            return ts.Days;
        }
    }
}