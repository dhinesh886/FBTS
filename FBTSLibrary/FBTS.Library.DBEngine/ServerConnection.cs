using System.Data.SqlClient;
using Microsoft.VisualBasic;

namespace FBTS.Library.DBEngine
{
    public static class ServerConnection
    {
        /// <summary>
        ///     This will create a connection object and establish a connection to the Database
        /// </summary>
        /// <param name="sConnectionSting">Connection string to establish connection</param>
        /// <returns>connection object</returns>
        /// <remarks></remarks>
        public static SqlConnection GetConnection(string sConnectionSting)
        {
            var objConn = new SqlConnection();
            if (!string.IsNullOrEmpty(sConnectionSting))
            {
                var aConnectionString = Strings.Split(sConnectionSting, "|");
                var cString = string.Format("Data Source={0}; Initial catalog={1};User Id={2};Password={3};", aConnectionString[0], aConnectionString[1], aConnectionString[2], aConnectionString[3]);
                objConn.ConnectionString = cString;
                objConn.Open();
            }
            else
            {
                objConn = null;
            }
            return objConn;
        }
    }
}
