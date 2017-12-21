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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace ExceptionsOnly
{
    public static class ExceptionLog
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ExceptionLog));

        public static void Dump(long exceptionId, Exception ex)
        {
            ExceptionLog.Logger.Info("***************************");
            ExceptionLog.Logger.Info(string.Format("     Exception Id : {0,-3}    ", exceptionId));
            ExceptionLog.Logger.Info("***************************");
            ExceptionLog.Logger.Error(string.Format("Exception ({0})", exceptionId), ex);
            ExceptionLog.Logger.Info(" ");
            ExceptionLog.Logger.Info(" ");
        }


        public static void Dump(string message)
        {
            ExceptionLog.Logger.Info("***************************");
            ExceptionLog.Logger.Info(message);
            ExceptionLog.Logger.Info("***************************");
            ExceptionLog.Logger.Info(" ");
        }
    }
}
