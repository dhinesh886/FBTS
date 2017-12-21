#region File Header
// ----------------------------------------------------------------------------
// File Name    : ConnectionStrings.cs
// Namespace    : FBTS.EntityFramework.Control.Helpers
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
using FBTS.Library.Common;

namespace FBTS.EntityFramework.Control.Helpers
{
    public static class ConnectionStrings
    {
        public static string GetControlConnectionString()
        {
            var originalConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ControlEntities"].ConnectionString;
            var controlDbInfo = ConfigurationReader<string>.GetAppConfigurationValue("controlDbInfo").Split('|');

            var ecsBuilder = new EntityConnectionStringBuilder(originalConnectionString);
            var sqlCsBuilder = new SqlConnectionStringBuilder(ecsBuilder.ProviderConnectionString)
            {
                InitialCatalog = controlDbInfo[0],
                DataSource = controlDbInfo[1],
                UserID = controlDbInfo[2],
                Password = controlDbInfo[3]
            };
            var providerConnectionString = sqlCsBuilder.ToString();
            ecsBuilder.ProviderConnectionString = providerConnectionString;
            return ecsBuilder.ToString();
        } 
    }
}
