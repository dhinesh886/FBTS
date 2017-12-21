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
using System;

namespace FBTS.Model.Control
{
    [Serializable]
    public class AnAttribute : Operations
    {
        public AnAttribute()
        {
            SubAnalysis = new AnAttributes();
        }
        public string Id { get; set; }
        public string Slno { get; set; }
        public string Name { get; set; }
        public string Module { get; set; } 
        public bool IsDefault { get; set; }
        public bool IsSuspended { get; set; }
        public AnAttributes SubAnalysis { get; set; }
       
    }
}
