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
    public enum JsonFormats
    {
        /// <summary>
        ///     Specifies no format is used.
        /// </summary>
        None,

        /// <summary>
        ///     Specifies Branches.
        /// </summary>
        Branches,

        /// <summary>
        ///     Specifies Officers.
        /// </summary>
        Officers,

        /// <summary>
        ///     Specifies Tasks.
        /// </summary>
        Tasks,

        /// <summary>
        ///     Specifies Others.
        /// </summary>
        Others
    }
}