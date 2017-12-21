#region File Header
// ----------------------------------------------------------------------------
// File Name    : ExceptionLog.cs
// Namespace    : FBTS.Library.EventLogger.Logger
// Class Name   : ReportLog
// Description  : 
//                
// Author       : DhineshKumar (dhinesh886@gmail.com)
// Created Date : Monday, December 28, 2009
// Updated By   : -
// Updated Date : -
// Company      : Copyright (c) 2014 Ezy Solutions Pvt Ltd.
//                
// Comments     : 
// ----------------------------------------------------------------------------
#endregion File Header

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace ReportsOnly
{
    public static class ReportLog
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ReportLog));

        public static void Dump(string message)
        {
            ReportLog.Logger.Info(message);
            
        }
    }
}