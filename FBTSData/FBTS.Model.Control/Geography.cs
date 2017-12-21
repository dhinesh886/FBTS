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
    public class Geography : Operations
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CurrencySymbol { get; set; }
        public Geographies ChildGeographies { get; set; }
       
    }
}
