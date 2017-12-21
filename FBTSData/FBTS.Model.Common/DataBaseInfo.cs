#region File Header
// ----------------------------------------------------------------------------
// File Name    : ExceptionLog.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : ExceptionLog
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
namespace FBTS.Model.Common
{
    public class DataBaseInfo
    {
        public string ControlDbServer { get; set; }
        public string DbServer { get; set; }
        public string DbName { get; set; }
        public string DbUserid { get; set; }
        public string DbPassword { get; set; }

        public string GetDbEngineConnectionString()
        {
            return string.Format("{0}|{1}|{2}|{3}", DbServer, DbName, DbUserid,
                    DbPassword);
            
        } 
    }
}