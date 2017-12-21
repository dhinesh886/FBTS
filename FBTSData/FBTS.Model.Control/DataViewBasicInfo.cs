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

using FBTS.Model.Common;

namespace FBTS.Model.Control
{
    public class DataViewBasicInfo : Operations
    { 
        public string Type { get; set; }
        public string Id { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public string StoredProcedure { get; set; }
        public string Module { get; set; }
        public bool IsSuspended { get; set; }
        public string Off { get; set; }
       
    }
}
