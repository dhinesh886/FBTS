namespace FBTS.Library.Common
{
    public enum DateFormat
    {
        None = 0,

        /// <summary>
        ///     Input Format[DD/MM/YYYY] Output Format[YYYY-MM-DD]
        /// </summary>
        Format_01 = 1,

        /// <summary>
        ///     Input Format[YYYY-MM-DD] Output Format[DD/MM/YYYY]
        /// </summary>
        Format_02 = 2,

        /// <summary>
        ///     Input Format[MM/DD/YYYY 12:00:00 AM] Output Format[DD/MM/YYYY]
        /// </summary>
        Format_03 = 3,

        /// <summary>
        ///     Input Format[MM/DD/YYYY 12:00:00 AM] Output Format[DD-MM-YYYY]
        /// </summary>
        Format_04 = 4,

        /// <summary>
        ///     Input Format[MM/DD/YYYY 12:00:00 AM] Output Format[YYYY-MM-DD]
        /// </summary>
        Format_05 = 5,

        /// <summary>
        ///     Input Format[DD/MM/YYYY] Output Format[MM/DD/YYYY 12:00:00 AM]
        /// </summary>
        Format_06 = 6,

        /// <summary>
        ///     Input Format[YYYY-MM-DD] Output Format[MM/DD/YYYY 12:00:00 AM]
        /// </summary>
        Format_07 = 7
    }
}