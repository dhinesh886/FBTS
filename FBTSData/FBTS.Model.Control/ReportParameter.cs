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
    public class EzyReportParameter : Operations
    {
        public string ParaCode { get; set; }
        public string ParaDescription  { get; set; }
        public string ParaValue { get; set; }
        public bool IsMandatory { get; set; } 
    }
}
