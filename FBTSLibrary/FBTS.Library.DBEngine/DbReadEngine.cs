using System.Data;
using System.Data.SqlClient;

namespace FBTS.Library.DBEngine
{
    /// <summary>
    ///     Class to Perform all Data read operations using direct queries or stored procedures
    /// </summary>
    /// <remarks></remarks>
    
    public class DbReadEngine
    {
        public string ConnectionString { get; set; }

        #region "Execute Query, Stored Procedures And Returns DataSet/DatTable"


        /// <summary>
        ///     Executes the given stored procedure and returns the dataset
        /// </summary>
        /// <param name="spName">Name of the stored procedure which needs to be executed</param>
        /// <param name="sqlparams">Parameter Array of the stored procedure</param>
        /// <returns>DataSet</returns>
        /// <remarks></remarks>
        public DataSet ExecuteSprocDs(string spName, params SqlParameter[] sqlparams)
        {
            var ds = new DataSet();
            var con = ServerConnection.GetConnection(ConnectionString);
            var cmd = new SqlCommand(spName, con) {CommandType = CommandType.StoredProcedure};
            foreach (var item in sqlparams)
            {
                cmd.Parameters.Add(item);
            }

            var da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            finally { con.Close(); }
           
            return ds;
        }


        /// <summary>
        ///     Builds and executes the query using the given attributes and returns the dataset
        /// </summary>
        /// <param name="tableName">Table Name from which the data will be retrieved</param>
        /// <param name="selectList">List of columns as comma deli metered string</param>
        /// <param name="condnList">Condition to filter the data</param>
        /// <param name="sortingby">Sort by column</param>
        /// <returns>DataSet</returns>
        /// <remarks></remarks>
        public DataSet ExecuteSqlDs(string tableName, string selectList, string condnList, string sortingby)
        {
            var ds = new DataSet();
            var strsql = string.Format("select {0} from {1}", selectList, tableName);
            if (!string.IsNullOrEmpty(condnList))
            {
                strsql += " where " + condnList;
            }
            if (!string.IsNullOrEmpty(sortingby))
            {
                strsql += " Order By " + sortingby;
            }
            var con = ServerConnection.GetConnection(ConnectionString);
            var cmd = new SqlCommand(strsql, con) {CommandType = CommandType.Text};

            var da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
            }
            finally { con.Close(); }
            return ds;
        }


        /// <summary>
        ///     Builds and executes the query using the given attributes and returns the DataTable
        /// </summary>
        /// <param name="tableName">Table Name from which the data will be retrieved</param>
        /// <param name="selectList">List of columns as comma deli metered string</param>
        /// <param name="condnList">Condition to filter the data</param>
        /// <param name="sortingby">Sort by column</param>
        /// <returns>DataTable</returns>
        /// <remarks></remarks>
        public DataTable ExecuteSqldt(string tableName, string selectList, string condnList, string sortingby)
        {
            DataTable dt;
            var ds = new DataSet();
            var strsql = string.Format("select {0} from {1}", selectList, tableName);
            if (!string.IsNullOrEmpty(condnList))
            {
                strsql += " where " + condnList;
            }
            if (!string.IsNullOrEmpty(sortingby))
            {
                strsql += " Order By " + sortingby;
            }
            var con = ServerConnection.GetConnection(ConnectionString);
            var cmd = new SqlCommand(strsql, con) {CommandType = CommandType.Text};

            var da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            finally { con.Close(); }
           
            return dt;
        }


        /// <summary>
        ///     Executes the given stored procedure and returns the DataTable
        /// </summary>
        /// <param name="spName">Name of the stored procedure which needs to be executed</param>
        /// <param name="sqlparams">Parameter Array of the stored procedure</param>
        /// <returns>DataTable</returns>
        /// <remarks></remarks>
        public DataTable ExecuteSprocDt(string spName, params SqlParameter[] sqlparams)
        {
            var ds = new DataSet();
            DataTable dt = null;
            var con = ServerConnection.GetConnection(ConnectionString);
            var cmd = new SqlCommand(spName, con) {CommandType = CommandType.StoredProcedure};
            foreach (var item in sqlparams)
            {
                cmd.Parameters.Add(item);
            }
            var da = new SqlDataAdapter(cmd);
            try
            {               
                
                
                da.Fill(ds);
                if (ds !=null && ds.Tables.Count>0)
                {               
                    dt = ds.Tables[0];
                }
            }
            finally { con.Close(); }
            return dt;
        }

        #endregion


        #region "Execute Query, Stored Procedures And Returns DataReader"


        /// <summary>
        ///     Executes the given stored procedure and returns the SqlDataReader
        /// </summary>
        /// <param name="connection">COnnection object to execute the SP using this </param>
        /// <param name="spName">Name of the stored procedure which needs to be executed</param>
        /// <param name="sqlparams">Parameter Array of the stored procedure</param>
        /// <returns>SqlDataReader</returns>
        /// <remarks></remarks>
        public SqlDataReader ExecuteSprocDr(SqlConnection connection, string spName, params SqlParameter[] sqlparams)
        {
            var cmd = new SqlCommand(spName, connection) {CommandType = CommandType.StoredProcedure};
            foreach (var item in sqlparams)
            {
                cmd.Parameters.Add(item);
            }
            var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }


        /// <summary>
        ///     Builds and executes the query using the given attributes and returns the SqlDataReader
        /// </summary>
        /// <param name="connection">COnnection object to execute the Query using this </param>
        /// <param name="tableName">Table Name from which the data will be retrieved</param>
        /// <param name="selectList">List of columns as comma deli metered string</param>
        /// <param name="condnList">Condition to filter the data</param>
        /// <param name="sortingby">Sort by column</param>
        /// <returns>SqlDataReader</returns>
        /// <remarks></remarks>
        public SqlDataReader ExecuteSqlDr(SqlConnection connection, string tableName, string selectList, string condnList, string sortingby)
        {
            var strsql = string.Format("select {0} from {1}", selectList, tableName);
            if (!string.IsNullOrEmpty(condnList))
            {
                strsql += string.Format(" where {0}", condnList);
            }
            if (!string.IsNullOrEmpty(sortingby))
            {
                strsql += string.Format(" Order By {0}", sortingby);
            }
            var cmd = new SqlCommand(strsql, connection) {CommandType = CommandType.Text};
            var dr = cmd.ExecuteReader();
            return dr;
        }

        #endregion


        /// <summary>
        ///     Builds and executes the query using the given attributes and returns the string value
        /// </summary>
        /// <param name="argsTableName">Table Name from which the data will be retrieved</param>
        /// <param name="argsColumn">List of columns as comma deli metered string</param>
        /// <param name="argsCondition">Condition to filter the data</param>
        /// <returns>Required column value as String </returns>
        /// <remarks></remarks>
        public string GetValue(string argsTableName, string argsColumn, string argsCondition)
        {
            var ds = new DataSet();
            var lsDesp = string.Empty;
            var strsql = string.Format("select {0} from {1}", argsColumn, argsTableName);
            if (!string.IsNullOrEmpty(argsCondition))
            {
                strsql += string.Format(" where {0}", argsCondition);
            }
            var con = ServerConnection.GetConnection(ConnectionString);
            var cmd = new SqlCommand(strsql, con) {CommandType = CommandType.Text};

            var da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lsDesp = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            finally { con.Close(); }
            return lsDesp;
        }
    }
}
