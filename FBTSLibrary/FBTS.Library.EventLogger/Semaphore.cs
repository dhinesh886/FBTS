using System;
using System.Data;
using System.Data.SqlClient; 

namespace FBTS.Library.EventLogger
{
    public class Semaphore
    {
        public enum SemTypes
        {
            Login,
            IntializeLogger,
            CurrentFormLogger

        }
        #region "Local Variable Initialization"

        public string ConnectionString { get; set; }

        public object SemError { get; set; }

        public object CUser { get; set; }

        public string SessionId { get; set; }

        public object CForm { get; set; }

        public string UserNode { get; set; }

        public object SemOption { get; set; }

        public object SemRefNum { get; set; }

        public string SemRefname { get; set; }

        public SemTypes SemType { get; set; }

        #endregion


        public void SemaphoreWrite()
        {
            //DataTable dt;
            ////Get an Active Connection
            //var objCon = ServerConnection.GetConnection(ConnectionString);
            ////Start a Transaction with  the Active Connection
            //var objTransaction = objCon.BeginTransaction();
            //short nWrite = 0;
            //SemError = string.Empty;
            //if (string.IsNullOrEmpty(SessionId))
            //    SessionId = UserNode;
            //if (SemType == SemTypes.Login)
            //{
            //    //-- To Check Whether the user has been already logged in?-----
            //    string msSqlQuery = string.Format("select u_node from c_user_activity where u_code = '{0}'", CUser);
            //    dt = ExecuteSqldt(msSqlQuery, objCon);
                //if (dt.Rows.Count > 0)
                //{
                //    if (dt.Rows[0][0].ToString().Trim() != UserNode.Trim())
                //    {
                //        SemError = "You have already logged in Terminal# : " + UserNode.Trim();
                //        objTransaction.Rollback();
                //        objCon.Close();
                //        return;
                //    }
                //}
            //}
            //else if (SemType == SemTypes.IntializeLogger)
            //{
            //    //-- Track login info initially from the Login page
            //    string msSqlQuery = string.Format("select session_id from c_user_activity where session_id = '{0}' and u_code = '{1}'", SessionId, CUser);
            //    dt = ExecuteSqldt(msSqlQuery, objCon);
            //    if (dt.Rows.Count > 0)
            //    {
            //        msSqlQuery = "Update c_user_activity set session_id = '" + SessionId + "',trn_date =  getdate() " + "where u_node = '" + UserNode + "' and u_code = '" + CUser + "'";
            //    }
            //    else
            //    {
            //        msSqlQuery = "Insert into c_user_activity (session_id, u_code, u_node, u_refno,trn_date) " + "values ( '" + SessionId + "','" + CUser + "','" + UserNode + "','', getdate())";
            //    }
            //    ExecuteSql(msSqlQuery, objCon);
            //    msSqlQuery = "Insert into t_user_activity (u_refname,session_id, u_code, u_node, u_refno,trn_date) " + "values ('LOGGEDIN', '" + SessionId + "','" + CUser + "','" + UserNode + "',getdate(), getdate())";
            //    ExecuteSql(msSqlQuery, objCon);
            //    nWrite = 1;


            //}
            //else if (SemType == SemTypes.CurrentFormLogger & !string.IsNullOrEmpty(SemRefname))
            //{
            //    //-- Write the Menu info from Master Page on Click of the menu
            //    string msSqlQuery = string.Format("Update c_user_activity set u_form = '{0}' " + " where  session_id = '{1}' and u_code = '{2}'", CForm, SessionId, CUser);
            //    ExecuteSql(msSqlQuery, objCon);
            //    nWrite = 1;
            //}
            //else if (SemType == "OPT")
            //{
            //    nWrite = 1;
            //}
            //else if (SemType == "REFNO" & !string.IsNullOrEmpty(Strings.Trim(SemRefNum)))
            //{
            //    try
            //    {
            //        //-- To identify if other users are accessing the Same account info --
            //        string msSqlQuery = "Select u_code from c_user_activity " + " where u_code <> '" + CUser + "' and  u_refno = '" + SemRefNum + "'";
            //        dt = ExecuteSqldt(msSqlQuery, objCon);

            //        if (dt.Rows.Count > 0)
            //        {
            //            SemError = "Details are being in used By another user " + dt.Rows[0][0].ToString().Trim() + Strings.Chr(13) + " Please try after some time.";
            //        }
            //        else
            //        {
            //            msSqlQuery = "Update c_user_activity set u_refno = '" + SemRefNum + "', u_option = '" + SemOption + "',trn_date =  getdate() " + " where session_id = '" + SessionId + "' and u_code = '" + CUser + "'";
            //            ExecuteSql(msSqlQuery, objCon);
            //            nWrite = 1;

            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
            //else if (SemType == "REFNAME" & !string.IsNullOrEmpty(Strings.Trim(SemRefname)))
            //{
            //    try
            //    {
            //        //-- To identify if other users are accessing the Same Transaction--
            //        string msSqlQuery = "Select u_code from c_user_activity " + " where u_code <> '" + CUser + "' and  u_refname = '" + SemRefname + "'";
            //        dt = ExecuteSqldt(msSqlQuery, objCon);

            //        if (dt.Rows.Count > 0)
            //        {
            //            SemError = "Details are being in used By another user " + dt.Rows[0][0].ToString().Trim() + Strings.Chr(13) + " Please try after some time.";
            //        }
            //        else
            //        {
            //            msSqlQuery = "Update c_user_activity set  u_refno = '" + SemRefNum + "',u_refname = '" + SemRefname + "',u_option= '" + SemOption + "' ,trn_date =  getdate() " + " where  session_id = '" + SessionId + "' and u_code = '" + CUser + "'";
            //            ExecuteSql(msSqlQuery, objCon);
            //            nWrite = 1;
            //        }

            //    }
            //    catch (Exception ex)
            //    {
            //    }
            //}
            //else if (SemType == "CLOSE")
            //{
            //    //-- Clear the current transaction log
            //    string msSqlQuery = "Insert into t_user_activity  SELECT * FROM c_user_activity" + " where  session_id = '" + SessionId + "' and u_code = '" + CUser + "'";
            //    ExecuteSql(msSqlQuery, objCon);
            //    msSqlQuery = "Update c_user_activity set  u_refno = '',u_refname = '' ,u_form='',u_option='' " + " where  session_id = '" + SessionId + "' and u_code = '" + CUser + "'";
            //    ExecuteSql(msSqlQuery, objCon);

            //}
            //else if (SemType == "CLOG")
            //{
            //    //-- Clear the current user log
            //    string msSqlQuery = "Delete from c_user_activity where session_id = '" + SessionId + "' and u_code = '" + CUser + "'";
            //    ExecuteSql(msSqlQuery, objCon);
            //    msSqlQuery = "Insert into t_user_activity (u_refname,session_id, u_code, u_node, u_refno,trn_date) " + "values ('LOGGEDOUT', '" + SessionId + "','" + CUser + "','" + UserNode + "',getdate(), getdate())";
            //    ExecuteSql(msSqlQuery, objCon);
            //}
            //else if ((SemType == "ALL" | SemType == "EDIT" | SemType == "DEL") & (!string.IsNullOrEmpty(Strings.Trim(SemRefNum)) & !string.IsNullOrEmpty(Strings.Trim(SemRefname))))
            //{
            //    string msSqlQuery = "Select u_code from c_user_activity  " + " where u_code <> '" + CUser + "' and u_refname = '" + SemRefname + "' and  u_refno   = '" + SemRefNum + "'";
            //    dt = ExecuteSqldt(msSqlQuery, objCon);

            //    if (dt.Rows.Count > 0)
            //    {
            //        SemError = "Details are being in use by another user: " + dt.Rows[0][0].ToString().Trim() + Strings.Chr(10) + " Please try after some time.";
            //    }
            //    else
            //    {
            //        nWrite = 2;
            //    }

            //}
            //else if (SemType == "CREF" | SemType == "COPT")
            //{
            //    SemRefname = " ";
            //    SemRefNum = " ";
            //    SemOption = (SemType == "COPT" ? " " : SemOption);
            //    CForm = (SemType == "COPT" ? " " : CForm);
            //    nWrite = 2;
            //}
            //else if (SemType == "SAV" | SemType == "CAN")
            //{
            //    string msSqlQuery = "Insert into c_user_activity (session_id,u_refno,u_refname,u_code ,u_form, u_option, u_node,trn_date,trn_status) values ( '" + SessionId + "','" + SemRefNum + "','" + SemRefname + "','" + CUser + "','" + CForm + "','" + SemOption + "','" + UserNode + "', GetDate() , " + (SemType == "SAV" ? 1 : 0) + " ) ";
            //    ExecuteSql(msSqlQuery, objCon);
            //    SemRefname = " ";
            //    SemRefNum = " ";
            //    SemOption = " ";
            //}
            //try
            //{
            //    string msSqlQuery;
            //    if (nWrite == 1)
            //    {
            //        msSqlQuery = "Update c_user_activity set u_refname = '" + SemRefname + "', " + "u_refno = '" + SemRefNum + "', " + "u_node = '" + UserNode + "', " + "u_option = '" + SemOption + "', " + "u_form = '" + CForm + "'" + " where u_code = '" + CUser + "' and session_id = '" + SessionId + "'";
            //        ExecuteSql(msSqlQuery, objCon);
            //    }
            //    else if (nWrite == 2)
            //    {
            //        msSqlQuery = "Insert into c_user_activity (session_id,u_refno,u_refname,u_code ,u_form, u_option, u_node,trn_date) values ( '" + SessionId + "','" + SemRefNum + "','" + SemRefname + "','" + CUser + "','" + CForm + "','" + SemOption + "','" + UserNode + "', GetDate())";
            //        ExecuteSql(msSqlQuery, objCon);
            //    }
            //    objTransaction.Commit();
            //}
            //catch (Exception ex)
            //{
            //    SemError = ex.Message;
            //    objTransaction.Rollback();
            //}
            //objCon.Close();
        }

        public DataTable ExecuteSqldt(string paramSqlQuery, SqlConnection paramCon)
        {
            SqlDataAdapter da;
            DataTable dt;
            var ds = new DataSet();
            var cmd = new SqlCommand(paramSqlQuery, paramCon) {CommandType = CommandType.Text};
            da = new SqlDataAdapter(cmd);
            try
            {
                da.Fill(ds);
                dt = ds.Tables[0];
            }
            catch  
            {
                dt = null;
            }
            return dt;
        }

        public int ExecuteSql(string paramSqlQuery, SqlConnection paramCon)
        {
            var cmd = new SqlCommand(paramSqlQuery, paramCon) {CommandType = CommandType.Text};
            int miRowsAffected = cmd.ExecuteNonQuery();
            return miRowsAffected;
        }
    }
}
