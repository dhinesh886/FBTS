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
    public class Designation : Operations
    {
        public Designation()
        {
            CreatedDate = DateTime.Now;
        }
        public string Id { get; set; }
        public string Description { get; set; } 
        public bool IsSuspended { get; set; }
        public string Level { get; set; }
        public string SlNo { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get { return DateTime.Now; } }
    }
}