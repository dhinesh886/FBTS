using System.Data;
using System.Data.SqlClient;

namespace FBTS.Library.DBEngine
{
    public static class ServiceDbEngine
    {
        public static string ConnectionString { get; set; } 
        public static DataSet ExecuteSprocDs(string spName, params SqlParameter[] sqlparams)
        {
            var ds = new DataSet();
            try
            {
                var con = ServerConnection.GetConnection(ConnectionString);
                SqlCommand cmd = null;

                cmd = new SqlCommand(spName, con) {CommandType = CommandType.StoredProcedure};
                foreach (var item in sqlparams)
                {
                    cmd.Parameters.Add(item);
                }
                var da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            catch
            {
                throw;
            }

            return ds;
        }
 
    }
}