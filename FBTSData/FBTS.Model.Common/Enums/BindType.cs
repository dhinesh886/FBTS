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
    public enum BindType
    {
        /// <summary>
        ///     Specifies no criteria is used.
        /// </summary>
        None,

        /// <summary>
        ///     Specifies primary key is used.
        /// </summary>
        List,

        /// <summary>
        ///     Specifies wildcard is used.
        /// </summary>
        Form ,
        Header,
        Details,
        SubDetail,
        SubList,
        BillDetail,
        Account,
        CompanyProfile,
        MaterialDetail,
        Export
    }
}