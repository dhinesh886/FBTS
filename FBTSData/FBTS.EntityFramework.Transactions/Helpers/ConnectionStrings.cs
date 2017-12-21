#region File Header
// ----------------------------------------------------------------------------
// File Name    : ConnectionStrings.cs
// Namespace    : FBTS.EntityFramework.Transactions.Helpers
// Class Name   : ConnectionStrings
// Description  : 
//                
// Author       : DhineshKumar (dhinesh886@gmail.com)
// Created Date : Monday, December 07, 2014
// Updated By   : -
// Updated Date : -
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : 
// ----------------------------------------------------------------------------
#endregion File Header
using System.Data.Entity.Core.EntityClient;
using System.Data.SqlClient; 
using FBTS.Model.Common;

namespace FBTS.EntityFramework.Transactions.Helpers
{
    public static class ConnectionStrings
    { 
        public static string GetTransactionConnectionString(DataBaseInfo dbInfo)
        {
            var originalConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["TransactionEntities"].ConnectionString;
            var ecsBuilder = new EntityConnectionStringBuilder(originalConnectionString);
            var sqlCsBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString)
            {
                DataSource = dbInfo.DbServer,
                InitialCatalog = dbInfo.DbName,
                UserID = dbInfo.DbUserid,
                Password = dbInfo.DbPassword
            };
            var providerConnectionString = sqlCsBuilder.ToString();
            ecsBuilder.ProviderConnectionString = providerConnectionString;
            return ecsBuilder.ToString();
        }
    }
}
