using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Reflection;
using Microsoft.VisualBasic;

namespace FBTS.Library.DBEngine
{
    /// <summary>
    ///     DBWriteEngine Class will perform all the DML Operation in an efficient way
    /// </summary>
    /// <remarks></remarks>
     
    public class DbWriteEngine
    {

        #region "Write Multi-Tables"

        public DbWriteEngine()
        {
            DataTables = new List<DataTable>();
            Exceptions = new List<Exception>();
        }
        public List<DataTable> DataTables { get; set; } 
        public List<Exception> Exceptions { get; set; }

        public bool IsSuccess
        {
            get { return (Exceptions.Count == 0); }
        }
        public string ConnectionString { get; set; }

        public string CurrentTxnNumber { get; set; }
        /// <summary>
        ///     To Prepare the structure of the table and validates whether the given schema information is correct or Not.
        /// </summary>
        /// <param name="pcTablename">Name of the Database Table</param>
        /// <param name="pcFields">Columns/Fields required for the current operation</param>
        /// <returns>Data table in the required structure</returns>
        /// <remarks></remarks>
        public DataTable PrepareTableStructure(string pcTablename, string pcFields )
        {
            //Begin - Declarations of Local Variables and Objects

            SqlCommand cmd;
            DataTable dt;
            using (var objCon = ServerConnection.GetConnection(ConnectionString))
            {
                var ds = new DataSet();
                //End - Declarations of Local Variables and Objects

                //Begin - Initialization of Local Variables and Objects
                cmd = new SqlCommand();
                dt = new DataTable(); 

                //End - Initialization of Local Variables and Objects

                //Check For Fields Parameter to prepare the DataTable Structure
                //BLANK -  Prepare the Structure for all the Columns in the Table 
                //K     -  Prepare the Structure only for the Primary Key Columns in the Table  
                //Other wise with the Actual Given Columns
                SqlDataAdapter da;
                if ((pcFields == null) | string.IsNullOrEmpty(pcFields))
                {
                    pcFields = " * ";
                }
                else if (pcFields == "K")
                {
                    cmd.Connection = objCon;
                    cmd.CommandText = "ezyadmin.Sp_Key_Fields";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@v_table", pcTablename.Trim()));
                    cmd.Parameters.Add(new SqlParameter("@v_output", ParameterAttributes.Out));
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "KeyInfo");
                    var dtKey = ds.Tables[0];
                    pcFields = string.Empty;
                    if (dtKey.Rows.Count > 0)
                    {
                        foreach (DataRow dtRow in dtKey.Rows)
                        {
                            pcFields = string.Format("{0}{1},", pcFields, dtRow["column_name"]);
                        }
                        pcFields = Strings.Left(pcFields, Strings.Len(pcFields) - 1);
                    }
                    else
                    {
                      Exceptions.Add(new Exception("No Key Fields Defined for table = " + pcTablename)); 
                    }
                }
                if (Exceptions.Count==0)
                {
                    var strCommand = string.Format("Select {0} from {1} where 1=2 ", pcFields, pcTablename.Trim());
                    cmd.Connection = objCon;
                    cmd.CommandText = strCommand;
                    cmd.CommandType = CommandType.Text;
                    da = new SqlDataAdapter(cmd);
                    da.Fill(ds, "TABLESTRUCTURE");
                    dt = ds.Tables[0];
                }
            }
            cmd.Connection = null;

            return dt;
        }


        /// <summary>
        ///     This will generate the required queries to perform the DML Operations
        /// </summary>
        /// <param name="paramTables">Array of table names and their respective actions</param>
        /// <returns>Newly generated transaction number/Error collection</returns>
        /// <remarks>Also generates new transaction number automatically. It has also got a very strong transaction rollback mechanism.</remarks>
        public string WriteMultipleTables(ref string[,] paramTables)
        {
            //Begin - Declarations of Local Variables and Objects
            DataTable dt;
            DataTable dtKey = null;
            string msNewNumber;
            string msCommandText = string.Empty;
            Exceptions = new List<Exception>();
            using (var objCon = ServerConnection.GetConnection(ConnectionString))
            {
                var ds = new DataSet();  
                msNewNumber = string.Empty; 
                //Start a Transaction with  the Active Connection
                var objTransaction = objCon.BeginTransaction();
                //Here,Actual manipulation of Data Starts
                try
                {  
                    for (var miTableIndex = 0; miTableIndex <= Information.UBound(paramTables, 2); miTableIndex++)
                    {
                        if (!string.IsNullOrEmpty(paramTables[0, miTableIndex]))
                        {
                            //Based on Array index Assign the Actual Data table to a Local DataTable for performing the Action
                            dt = DataTables[miTableIndex];
                            if (dt == null) continue;
                            //Get the Current Table and Current Action from the Parameter Array
                            string msCurerentTable = paramTables[0, miTableIndex].Trim();
                            string msCurrentAction = paramTables[1, miTableIndex].Trim();

                            //Get Primary Key Fields if the Action is Update or Delete using the ezyadmin.Sp_Key_Fields Stored Procedure
                            SqlCommand cmd;
                            if ((msCurrentAction == "U" | msCurrentAction == "D"))
                            {
                                cmd = new SqlCommand
                                {
                                    Connection = objCon,
                                    Transaction = objTransaction,
                                    CommandText = "ezyadmin.Sp_Key_Fields",
                                    CommandType = CommandType.StoredProcedure
                                };
                                cmd.Parameters.Add(new SqlParameter("@v_table", msCurerentTable));
                                cmd.Parameters.Add(new SqlParameter("@v_output", ParameterAttributes.Out));
                                var da = new SqlDataAdapter(cmd);
                                da.Fill(ds, "KeyInfo");
                                dtKey = ds.Tables[0];
                            }
                            //To Build Query for every row a row wise loop starts here
                            for (int miRowIndex = 0; miRowIndex <= dt.Rows.Count - 1; miRowIndex++)
                            {
                                //Check whether the Current Action
                                int miColumnIndex;
                                string msColumnName;
                                string msValue;
                                string msValues;
                                msCommandText = string.Empty;
                                if ((msCurrentAction ==  "I"))
                                {
                                    string msColumnNames = string.Empty;
                                    msValues = string.Empty;
                                    //Actual Query building Starts Here
                                    //To Add the Column name and Column Value to the Current row Query a column wise loop starts here 
                                    for (miColumnIndex = 0; miColumnIndex <= dt.Columns.Count - 1; miColumnIndex++)
                                    {
                                        //To get Current Column Name And Value
                                        msColumnName = Strings.UCase(dt.Columns[miColumnIndex].Caption);
                                        msValue = Strings.UCase(dt.Rows[miRowIndex][miColumnIndex].ToString());
                                        //Add the Column Name to ColumnList to build the Query
                                        msColumnNames = msColumnNames + msColumnName + ",";

                                        //If it is a Boolean column assign Login Values Instead of TRUE or FALSE
                                        if (dt.Columns[miColumnIndex].DataType == Type.GetType("System.Boolean"))
                                        {
                                            msValue = Convert.ToInt32(Convert.ToBoolean(dt.Rows[miRowIndex][miColumnIndex])).ToString(CultureInfo.InvariantCulture);
                                        }
                                        else
                                        {
                                            //Generate New Number From M_numBers if the Column Value COntains the Word AUTO_NUMBER Otherwise go with Direct Assignation
                                            if (msValue.Contains("AUTO_NUMBER"))
                                            {
                                                var numberKey = msValue.Split(new[]{"||"}, StringSplitOptions.None);
                                                if (numberKey.Length > 0 && numberKey[1] == "ISEQUENCE" && string.IsNullOrEmpty(msNewNumber))
                                                {
                                                    msNewNumber = GetNewMasterNumber(numberKey[3], numberKey[2]);
                                                } 
                                                dt.Rows[miRowIndex][miColumnIndex] = msNewNumber;
                                            }
                                            msValue = dt.Rows[miRowIndex][miColumnIndex].ToString();
                                        }
                                        //Add the value to ValueList to build the Query
                                        msValues = msValues + string.Format("'{0}',", msValue);
                                    }

                                    //Remove the Last Comma from  Column Name List and Column value List
                                    msColumnNames = Strings.Mid(msColumnNames, 1, Strings.Len(msColumnNames) - 1);
                                    msValues = Strings.Mid(msValues, 1, Strings.Len(msValues) - 1);

                                    //Build the Query Using Table Name, Column Name List and Column value List
                                    msCommandText = string.Format("Insert into {0} ({1}) values ({2})", msCurerentTable,
                                        msColumnNames, msValues);

                                    //Execute the Query 
                                    cmd = new SqlCommand
                                    {
                                        Connection = objCon,
                                        Transaction = objTransaction,
                                        CommandText = msCommandText,
                                        CommandType = CommandType.Text
                                    };
                                    cmd.ExecuteNonQuery();
                                }
                                else if ((msCurrentAction == "U" | msCurrentAction == "D"))
                                {
                                    var msCondition = " Where ";
                                    msValues = string.Empty;

                                    //To Add the Column name and Column Value to the Current row Query a column wise loop starts here 
                                    for (miColumnIndex = 0; miColumnIndex <= dt.Columns.Count - 1; miColumnIndex++)
                                    {
                                        var msKeyColumnName = string.Empty;
                                        //To get Current Column Name 
                                        msColumnName = Strings.UCase(dt.Columns[miColumnIndex].Caption);
                                        //Check whether the Current column is a Primary Key Column or Not?
                                        //if it is a Primary Key Add it to the  where condition of the Query otherwise add it to updatation List
                                        if (dtKey != null && dtKey.Rows.Count > 0)
                                        {
                                            var dtKeyRows =
                                                dtKey.Select(string.Format("column_name = '{0}'", msColumnName));
                                            if ((dtKeyRows.Length > 0))
                                            {
                                                DataRow dtKeyRow = dtKeyRows[0];
                                                msKeyColumnName = dtKeyRow["column_name"].ToString();
                                            }
                                        }
                                        //Check whether the Column is a Boolean type?
                                        //If not, take the column Value AS it is
                                        //If it is a Boolean column assign Login Values Instead of TRUE or FALSE
                                        if (dt.Columns[miColumnIndex].DataType == Type.GetType("System.Boolean"))
                                        {
                                            msValue = Convert.ToBoolean(dt.Rows[miRowIndex][miColumnIndex].ToString())
                                                ? 1.ToString(CultureInfo.InvariantCulture)
                                                : 0.ToString(CultureInfo.InvariantCulture);
                                        }
                                        else
                                        {
                                            msValue = string.Format("'{0}'",
                                                dt.Rows[miRowIndex][miColumnIndex].ToString().Trim());
                                        }
                                        //If the Action is U and table M_NUMBERS directly take the values of NUMBERS and BNO column from New number variable
                                        if (msCurrentAction == "U" & msCurerentTable == "M_NUMBERS" &
                                            msValue.Contains("AUTO_NUMBER")  & msColumnName == "N_NUMBER")
                                        {
                                            msNewNumber = GetNewTrnNumber(
                                                dt.Rows[miRowIndex]["N_TYPES"].ToString(),
                                                dt.Rows[miRowIndex]["N_TD"].ToString(),
                                                dt.Rows[miRowIndex]["N_BNO"].ToString(),
                                                dt.Rows[miRowIndex]["N_BRANCH"].ToString(),
                                                dt.Rows[miRowIndex]["N_BU"].ToString());
                                            dt.Rows[miRowIndex][msColumnName] = Strings.Right(msNewNumber.Trim(), 5);
                                            msValue = string.Format("'{0}'", Strings.Right(msNewNumber.Trim(), 5));
                                        }
                                        //if it is a Primary Key Add it to the  where condition of the Query otherwise add it to updatation List
                                        if (string.IsNullOrEmpty(msKeyColumnName))
                                        {
                                            if (
                                                !(Information.IsDBNull(dt.Rows[miRowIndex][miColumnIndex]) &
                                                  (dt.Rows[miRowIndex][miColumnIndex] == null)))
                                            {
                                                msValues = msValues + msColumnName +  string.Format(" = {0},", msValue);
                                            }
                                        }
                                        else
                                        {
                                            msCondition = msCondition + msKeyColumnName + " = " + msValue + " and ";
                                        }
                                    }
                                    //Remove the Last and KeyWord from ConditionList 
                                    //Build the Query Using Table Name, Column Name List, Column value List and Condition List
                                    msCondition = Strings.Mid(msCondition, 1, Strings.Len(msCondition) - 5);
                                    if (msCurrentAction == "U")
                                    {
                                        //Remove the Last Comma from Column value List
                                        msValues = Strings.Mid(msValues, 1, Strings.Len(msValues) - 1);
                                        msCommandText = "update " + msCurerentTable + " set " + msValues + msCondition;
                                    }
                                    else
                                    {
                                        msCommandText = "delete " + msCurerentTable + msCondition;
                                    }
                                    //Execute the Query 
                                    cmd = new SqlCommand
                                    {
                                        Transaction = objTransaction,
                                        Connection = objCon,
                                        CommandText = msCommandText,
                                        CommandType = CommandType.Text
                                    };
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                { 
                    Exceptions.Add(ex);
                    Exceptions.Add(new Exception(msCommandText));
                }
                try
                {
                    if (IsSuccess)
                    {
                        objTransaction.Commit();
                        CurrentTxnNumber = msNewNumber;
                    }
                    else
                    {
                        objTransaction.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    Exceptions.Add(ex);
                }
            } 
            //Return the Function with  NewNumber
            return msNewNumber;
        }


        /// <summary>
        ///     This will not be called directly from the page. and will act like a support function for Write multi Tables to generate new master number
        /// </summary>
        /// <param name="argsCode"></param>
        /// <param name="argsType">Type value in M_NUMBERS</param> 
        /// <returns>New Transaction Number</returns>
        /// <remarks></remarks>
        public string GetNewMasterNumber(string argsCode, string argsType)
        {
            var objReadData = new DbReadEngine {ConnectionString = ConnectionString};
            var ds = objReadData.ExecuteSprocDs("ezyadmin.Usp_Ezy_Common_GenNewMasterNumber", 
                                            new SqlParameter("@Pvc_Code", argsCode), 
                                            new SqlParameter("@Pvc_Type", argsType));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString().Trim(); 
            }
            return string.Empty;
        }

        /// <summary>
        ///     This will not be called directly from the page. and will act like a support function for Write multi Tables to generate new transaction number
        /// </summary>
        /// <param name="argsType">Type value in M_NUMBERS</param>
        /// <param name="argsBno">BNo Value in M_NUMBERS</param>
        /// <param name="argsBranch">Branch value in M_NUMBERS</param>
        /// <param name="argsBu">Bu value in M_NUMBERS</param>
        /// <param name="argsTd"></param>
        /// <param name="argsJb"></param> 
        /// <returns>New Transaction Number</returns>
        /// <remarks></remarks>
        public string GetNewTrnNumber(string argsType, string argsTd, string argsBno, string argsBranch, string argsBu)
        {
            var objReadData = new DbReadEngine { ConnectionString = ConnectionString };
            var ds = objReadData.ExecuteSprocDs("ezyadmin.Usp_Ezy_Common_GenNewTransNumber",
                                            new SqlParameter("@Pvc_Type", argsType),
                                            new SqlParameter("@Pvc_Td", argsTd) ,
                                            new SqlParameter("@Pvc_Bno", argsBno),
                                            new SqlParameter("@Pvc_Branch", argsBranch),
                                            new SqlParameter("@Pvc_Bu", argsBu));
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0][0].ToString().Trim();
            }
            return string.Empty;
        }  
        #endregion


        /// <summary>
        ///     Function to Execute Delete Query
        /// </summary>
        /// <param name="tableName">Name of the table from which the data will be deleted</param>
        /// <param name="condnList">Where condition for the current deletion operation</param>
        /// <returns>The operation status</returns>
        /// <remarks></remarks>
        public bool ExecuteDelSql(string tableName, string condnList)
        {
            var strsql = string.Format("delete from {0}", tableName);
            if (!string.IsNullOrEmpty(condnList))
            {
                strsql += string.Format(" where {0}", condnList);
            }
            var con = ServerConnection.GetConnection(ConnectionString);
            try
            {
                var cmd = new SqlCommand(strsql, con) { CommandType = CommandType.Text };
                con.Open();
                cmd.ExecuteNonQuery();
           
            }
            finally { con.Close(); }
            
            return true;
        }
        //


        /// <summary>
        ///     Function to Execute Drop Query
        /// </summary>
        /// <param name="argsTableName">Name of the table from which the data will be deleted</param>
        /// <returns>The operation status</returns>
        /// <remarks></remarks>
        public bool DropTable(ref string argsTableName)
        {
            var strsql = string.Format("drop table {0}", argsTableName);
            var con = ServerConnection.GetConnection(ConnectionString);
            var cmd = new SqlCommand(strsql, con) {CommandType = CommandType.Text};
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                
            }  
            finally { con.Close(); }
            return true;
        }
    }
}
